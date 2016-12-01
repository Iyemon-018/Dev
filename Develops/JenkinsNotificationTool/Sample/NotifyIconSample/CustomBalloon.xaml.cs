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

namespace NotifyIconSample
{
    using System.ComponentModel;
    using Hardcodet.Wpf.TaskbarNotification;
    using NotifyIconSample.Annotations;

    /// <summary>
    /// CustomBalloon.xaml の相互作用ロジック
    /// </summary>
    public partial class CustomBalloon : UserControl
    {
        /// <summary>
        /// 依存関係プロパティ <see cref="Title"/> を識別します。
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title"
                                      , typeof(string)
                                      , typeof(CustomBalloon)
                                      , new FrameworkPropertyMetadata("Title"));

        /// <summary>
        /// 表示するタイトルの文言を設定、または取得します。
        /// </summary>
        [Category("Custom"),
         Description("表示するタイトルの文言を設定、または取得します。")]
        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        /// <summary>
        /// 依存関係プロパティ <see cref="Message"/> を識別します。
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message"
                                      , typeof(string)
                                      , typeof(CustomBalloon)
                                      , new FrameworkPropertyMetadata("Message"));

        /// <summary>
        /// 表示メッセージを設定、または取得します。
        /// </summary>
        [Category("Custom"),
         Description("表示メッセージを設定、または取得します。")]
        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        private readonly TaskbarIcon _taskbarIcon;

        public CustomBalloon([NotNull] TaskbarIcon taskbarIcon)
        {
            if (taskbarIcon == null) throw new ArgumentNullException(nameof(taskbarIcon));
            InitializeComponent();
            
            _taskbarIcon = taskbarIcon;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            _taskbarIcon.CloseBalloon();
        }
    }
}
