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
                                 AddText(string.Format("{0}\r\n{1}", e.Exception.GetType(), e.Exception.ToString()));
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

}
