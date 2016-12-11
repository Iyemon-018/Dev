namespace JenkinsNotification.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// メッセージを表示するためのダイアログ ウィンドウ コンポーネントです。
    /// </summary>
    public partial class MessageDialog : Window
    {
        #region Const

        /// <summary>
        /// 依存関係プロパティ <see cref="Caption"/> を識別します。
        /// </summary>
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption"
                                      , typeof(string)
                                      , typeof(MessageDialog)
                                      , new FrameworkPropertyMetadata("Message Caption"));

        /// <summary>
        /// 依存関係プロパティ <see cref="Message"/> を識別します。
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message"
                                      , typeof(string)
                                      , typeof(MessageDialog)
                                      , new FrameworkPropertyMetadata("Message Text."));

        /// <summary>
        /// 依存関係プロパティ <see cref="ButtonType"/> を識別します。
        /// </summary>
        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register("ButtonType"
                                      , typeof(MessageBoxButton)
                                      , typeof(MessageDialog)
                                      , new FrameworkPropertyMetadata(MessageBoxButton.OK, ButtonTypePropertyChanged));

        /// <summary>
        /// 依存関係プロパティ <see cref="IconType"/> を識別します。
        /// </summary>
        public static readonly DependencyProperty IconTypeProperty =
            DependencyProperty.Register("IconType"
                                      , typeof(MessageBoxImage)
                                      , typeof(MessageDialog)
                                      , new FrameworkPropertyMetadata(MessageBoxImage.Information));

        /// <summary>
        /// 依存関係プロパティ <see cref="Result"/> を識別します。
        /// </summary>
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register("Result"
                                      , typeof(MessageBoxResult)
                                      , typeof(MessageDialog)
                                      , new FrameworkPropertyMetadata(MessageBoxResult.None));

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MessageDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 表示メッセージを設定、または取得します。
        /// </summary>
        [Category("Custom"),
         Description("表示メッセージを設定、または取得します。")]
        internal string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// メッセージのタイトルを設定、または取得します。
        /// </summary>
        [Category("Custom"),
         Description("メッセージのタイトルを設定、または取得します。")]
        internal string Caption
        {
            get { return (string) GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        /// <summary>
        /// 表示するボタン種別を設定、または取得します。
        /// </summary>
        [Category("Custom"),
         Description("表示するボタン種別をを設定、または取得します。")]
        internal MessageBoxButton ButtonType
        {
            get { return (MessageBoxButton) GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }

        /// <summary>
        /// 表示するメッセージアイコンを設定、または取得します。
        /// </summary>
        [Category("Custom"),
         Description("表示するメッセージアイコンを設定、または取得します。")]
        internal MessageBoxImage IconType
        {
            get { return (MessageBoxImage) GetValue(IconTypeProperty); }
            set { SetValue(IconTypeProperty, value); }
        }

        /// <summary>
        /// メッセージの表示結果を設定、または取得します。
        /// </summary>
        [Category("Custom"),
         Description("メッセージの表示結果を設定、または取得します。")]
        internal MessageBoxResult Result
        {
            get { return (MessageBoxResult) GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// メッセージを表示します。
        /// </summary>
        /// <param name="message">表示メッセージ</param>
        /// <param name="caption">メッセージタイトル</param>
        /// <param name="button">ボタン種別</param>
        /// <param name="icon">アイコン種別</param>
        /// <returns>メッセージ表示結果</returns>
        public static MessageBoxResult Show(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(null, message, caption, button, icon);
        }

        /// <summary>
        /// 親ウィンドウの全面にメッセージを表示します。
        /// </summary>
        /// <param name="owner">親ウィンドウ</param>
        /// <param name="message">表示メッセージ</param>
        /// <param name="caption">メッセージタイトル</param>
        /// <param name="button">ボタン種別</param>
        /// <param name="icon">アイコン種別</param>
        /// <returns>メッセージ表示結果</returns>
        public static MessageBoxResult Show(Window owner, string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            var messageDialog = new MessageDialog
                                {
                                    Message    = message,
                                    Caption    = caption,
                                    ButtonType = button,
                                    IconType   = icon
                                };

            if (owner == null)
            {
                messageDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                messageDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                messageDialog.Owner = owner;
            }

            messageDialog.ShowDialog();
            return messageDialog.Result;
        }

        /// <summary>
        /// <see cref="ButtonTypeProperty"/> の値が変更されたときに呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="d">イベント送信元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private static void ButtonTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MessageDialog)?.OnButtonTypeChanged((MessageBoxButton)e.NewValue);
        }

        /// <summary>
        /// <see cref="CancelButton"/>がクリックされた際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close(MessageBoxResult.Cancel);
        }

        /// <summary>
        /// <see cref="CaptionAreaDockPanel"/>がマウスの左クリックされた際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void CaptionAreaDockPanel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// この画面を閉じます。
        /// </summary>
        /// <param name="result">メッセージ結果</param>
        private void Close(MessageBoxResult result)
        {
            Result = result;
            SystemCommands.CloseWindow(this);
        }

        /// <summary>
        /// <see cref="NoButton"/>がクリックされた際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void NoButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close(MessageBoxResult.No);
        }

        /// <summary>
        /// <see cref="OkButton"/>がクリックされた際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close(MessageBoxResult.OK);
        }

        /// <summary>
        /// <see cref="ButtonType"/> プロパティの値が更新されました。
        /// </summary>
        /// <param name="newValue">更新後の値</param>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="newValue"/> の値が使用できない場合にスローします。</exception>
        private void OnButtonTypeChanged(MessageBoxButton newValue)
        {
            switch (newValue)
            {
                case MessageBoxButton.OK:
                    // OKボタンのみ表示します。
                    OkButton.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.OKCancel:
                    // OK/Cancelボタンを表示します。
                    OkButton.Visibility     = Visibility.Visible;
                    CancelButton.Visibility = Visibility.Visible;
                    OkButton.IsDefault      = true;
                    CancelButton.IsCancel   = true;
                    break;
                case MessageBoxButton.YesNo:
                    // Yes/Noボタンを表示します。
                    YesButton.Visibility = Visibility.Visible;
                    NoButton.Visibility  = Visibility.Visible;
                    YesButton.IsDefault  = true;
                    NoButton.IsCancel    = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newValue), newValue, null);
            }
        }

        /// <summary>
        /// <see cref="YesButton"/>がクリックされた際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close(MessageBoxResult.Yes);
        }

        #endregion
    }
}
