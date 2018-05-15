using System.Windows;

namespace WebSocket4Net_Sample
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Windows.Data;
    using WebSocket4Net;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebSocket _socket;

        private readonly object _notiferResultsLock = new object();

        private readonly ObservableCollection<JenkinsNotifierJson> _notiferResults = new ObservableCollection<JenkinsNotifierJson>();

        public MainWindow()
        {
            InitializeComponent();

            BindingOperations.EnableCollectionSynchronization(_notiferResults, _notiferResultsLock);

            NotifyResultsDataGrid.ItemsSource = _notiferResults;
        }

        private void Connect()
        {
            Action<string> AddText = x =>
                                     {
                                         Dispatcher.Invoke(() =>
                                                           {
                                                               AccessResultTextBox.Text += x + Environment.NewLine;
                                                           });
                                     };
            if (_socket == null)
            {
                AddText("サーバーへの接続を開始します。");
            }
            _socket = new WebSocket(ServerAddressTextBox.Text);

            // 文字列受信
            _socket.MessageReceived += (sender, e) =>
                                       {
                                           AddText("[MessageReceived]");
                                           AddText(e.Message);

                                           File.WriteAllText($"Jenkins_{DateTime.Now:yyyy-MM-dd_HHmmss}.json", e.Message);

                                           var serializer = new DataContractJsonSerializer(typeof(JenkinsNotifierJson));
                                           using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Message)))
                                           {
                                               var data = serializer.ReadObject(ms) as JenkinsNotifierJson;
                                               if (data != null)
                                               {
                                                   _notiferResults.Add(data);
                                               }
                                           }
                                       };

            // データ受信
            _socket.DataReceived += (sender, e) =>
                                    {
                                        AddText("[DataReceived]");
                                        AddText(Encoding.UTF8.GetString(e.Data));
                                    };

            // サーバー接続完了
            _socket.Opened += (sender, e) =>
                              {
                                  AddText("[Opened]");
                                  AddText("サーバーへの接続が完了した。");
                              };

            // サーバーの切断
            _socket.Closed += (sender, e) =>
                              {
                                  AddText("[Closed]");
                                  AddText("サーバーとの接続を切断した。");
                              };

            // エラー発生
            _socket.Error += (sender, e) =>
                             {
                                 AddText("[Error]");
                                 AddText($"{e.Exception.GetType()}\r\n{e.Exception.ToString()}");
                             };

            //
            // これを実行すると以下の様なメッセージが出力される。
            //
            //[Opened]
            //サーバーへの接続が完了した。
            //[MessageReceived]
            //{"project":"MC14-MC4.EcoDataPremium-develop.AutoBuildAndTest","number":213,"status":"START"}
            //[MessageReceived]
            //{"project":"MC14-MC4.EcoDataPremium-develop.AutoBuildAndTest","number":213,"status":"SUCCESS","result":"SUCCESS"}
            //
            _socket.Open();
        }

        private void ConnectionButton_OnClick(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        private void DisconnectButton_OnClick(object sender, RoutedEventArgs e)
        {
            _socket?.Close();
        }
    }


    [DataContract]
    public class JenkinsNotifierJson
    {
        [DataMember]
        public string project { get; set; }

        [DataMember]
        public int number { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string result { get; set; }
    }


    //
    //
    //

    #region Jenkins API jobs で取得できるjsonデータ形式

    /*
     * こんなデータが取得できる。
       {  
        "_class":"hudson.model.Hudson",
        "assignedLabels":[  
          {  

          }
        ],
        "mode":"NORMAL",
        "nodeDescription":"ノード",
        "nodeName":"",
        "numExecutors":2,
        "description":null,
        "jobs":[  
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"LicenseDB_Backup",
             "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/",
             "color":"blue"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC11-MC1.App-develop-FxCop.ForInternal",
             "url":"http://mc-tfserver/jenkins/job/MC11-MC1.App-develop-FxCop.ForInternal/",
             "color":"blue"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC11-MC1.App-develop-R.ForInternal",
             "url":"http://mc-tfserver/jenkins/job/MC11-MC1.App-develop-R.ForInternal/",
             "color":"blue"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC11-MC1.App-develop.ForInternal",
             "url":"http://mc-tfserver/jenkins/job/MC11-MC1.App-develop.ForInternal/",
             "color":"blue"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC11-MC1.App-master.ForInternal",
             "url":"http://mc-tfserver/jenkins/job/MC11-MC1.App-master.ForInternal/",
             "color":"blue"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC14-MC4.BeOnePremium.AutoBuildAndTest",
             "url":"http://mc-tfserver/jenkins/job/MC14-MC4.BeOnePremium.AutoBuildAndTest/",
             "color":"blue"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC14-MC4.EcoDataPremium-develop.FxCop",
             "url":"http://mc-tfserver/jenkins/job/MC14-MC4.EcoDataPremium-develop.FxCop/",
             "color":"notbuilt"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC14-MC4.EcoDataPremiumRegist.AutoBuildAndTest",
             "url":"http://mc-tfserver/jenkins/job/MC14-MC4.EcoDataPremiumRegist.AutoBuildAndTest/",
             "color":"blue"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC14-MC4.Premium-d.Build.Test",
             "url":"http://mc-tfserver/jenkins/job/MC14-MC4.Premium-d.Build.Test/",
             "color":"blue"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"MC14-MC4.Premium.Setup",
             "url":"http://mc-tfserver/jenkins/job/MC14-MC4.Premium.Setup/",
             "color":"red"
          },
          {  
             "_class":"hudson.model.FreeStyleProject",
             "name":"Test.SetupBuild",
             "url":"http://mc-tfserver/jenkins/job/Test.SetupBuild/",
             "color":"blue"
          }
        ],
        "overallLoad":{  

        },
        "primaryView":{  
          "_class":"hudson.model.AllView",
          "name":"すべて",
          "url":"http://mc-tfserver/jenkins/"
        },
        "quietingDown":false,
        "slaveAgentPort":9000,
        "unlabeledLoad":{  
          "_class":"jenkins.model.UnlabeledLoadStatistics"
        },
        "useCrumbs":false,
        "useSecurity":true,
        "views":[  
          {  
             "_class":"hudson.model.ListView",
             "name":"EcoData",
             "url":"http://mc-tfserver/jenkins/view/EcoData/"
          },
          {  
             "_class":"hudson.model.AllView",
             "name":"すべて",
             "url":"http://mc-tfserver/jenkins/"
          },
          {  
             "_class":"hudson.model.ListView",
             "name":"定期実行ジョブ",
             "url":"http://mc-tfserver/jenkins/view/%E5%AE%9A%E6%9C%9F%E5%AE%9F%E8%A1%8C%E3%82%B8%E3%83%A7%E3%83%96/"
          },
          {  
             "_class":"hudson.model.ListView",
             "name":"訪問者管理システム",
             "url":"http://mc-tfserver/jenkins/view/%E8%A8%AA%E5%95%8F%E8%80%85%E7%AE%A1%E7%90%86%E3%82%B7%E3%82%B9%E3%83%86%E3%83%A0/"
          }
        ]
        }
     */

    public class Rootobject
    {
        public string _class { get; set; }
        public Assignedlabel[] assignedLabels { get; set; }
        public string mode { get; set; }
        public string nodeDescription { get; set; }
        public string nodeName { get; set; }
        public int numExecutors { get; set; }
        public object description { get; set; }

        /// <summary>
        /// ここでジョブの情報が取得できる。
        /// </summary>
        public Job[] jobs { get; set; }
        public Overallload overallLoad { get; set; }
        public Primaryview primaryView { get; set; }
        public bool quietingDown { get; set; }
        public int slaveAgentPort { get; set; }
        public Unlabeledload unlabeledLoad { get; set; }
        public bool useCrumbs { get; set; }
        public bool useSecurity { get; set; }
        public View[] views { get; set; }
    }

    public class Overallload
    {
    }

    public class Primaryview
    {
        public string _class { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Unlabeledload
    {
        public string _class { get; set; }
    }

    public class Assignedlabel
    {
    }

    public class Job
    {
        public string _class { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string color { get; set; }
    }

    public class View
    {
        public string _class { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }


    #endregion

}
