using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace ChainOfResponsibilitySample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private CallbackManager _callbackManager = new CallbackManager();

        public MainWindow()
        {
            InitializeComponent();

            const int maximum = 100000;

            // コールバック登録
            for (int i = 0; i < maximum; i++)
            {
                var cmd = new GeneralRecivedCommand<object>(i, new Func<object, byte[]>(buffer => { return null; }));
                _callbackManager.AddCallback(cmd);
            }

            // イベント番号指定
            var r = new Random();
            int eventNo = r.Next(maximum - 1);

            // 時間を計測
            var sw = new Stopwatch();
            sw.Start();

            // コマンドを実行する。
            _callbackManager.ExecuteCallback(eventNo, null, null);

            sw.Stop();

            Console.WriteLine(string.Format("経過時間:{0}ミリ秒", sw.ElapsedMilliseconds));
        }
    }
}
