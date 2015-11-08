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

namespace DateTimeCompareSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DateTimeTextBox.Text = DateTime.Now.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var now = DateTime.Now;
            var inputTime = DateTime.Parse(DateTimeTextBox.Text);

            if (!inputTime.CompareYear(now))
            {
                ConsoleLog.Write("年が異なる。");
            }
            if (!inputTime.CompareMonth(now))
            {
                ConsoleLog.Write("月が異なる。");
            }
            if (!inputTime.CompareDay(now))
            {
                ConsoleLog.Write("日が異なる。");
            }
            if (!inputTime.CompareHour(now))
            {
                ConsoleLog.Write("時間が異なる。");
            }
            if (!inputTime.CompareMinute(now))
            {
                ConsoleLog.Write("分が異なる。");
            }
            if (!inputTime.CompareSecond(now))
            {
                ConsoleLog.Write("秒が異なる。");
            }

        }
    }
}
