namespace AppCenter.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows;
    using System.Windows.Threading;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private static readonly string AppToken = "00289a3c-e780-405d-b78c-4fecdef2269c";

        public App()
        {
            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Crash!!", MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        /// <summary>
        /// <see cref="E:System.Windows.Application.Startup" /> イベントを発生させます。
        /// </summary>
        /// <param name="e">
        /// イベント データを格納している <see cref="T:System.Windows.StartupEventArgs" />。
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            Analytics.SetEnabledAsync(true);
#else
            Analytics.SetEnabledAsync(true);
#endif

            // アプリ固有の CustomException がスローされた場合、ShouldReportProcessed = true (= 処理済み) であればレポートを出力しない。
            // ShouldReportProcessed = false (= 未処理) もしくは CustomException 以外の例外はレポートに通知する。
            Crashes.ShouldProcessErrorReport = (ErrorReport report)
                                                   => !(report.Exception is CustomException customException)
                                                      || !customException.ShouldReportProcessed;

            Crashes.ShouldAwaitUserConfirmation = () =>
                                                  {
                                                      MessageBoxResult result = MessageBox.Show("クラッシュレポートを送信します。よろしいでしょうか？"
                                                                                              , "Question"
                                                                                              , MessageBoxButton.YesNo
                                                                                              , MessageBoxImage.Question);

                                                      if (result == MessageBoxResult.Yes)
                                                      {
                                                          Crashes.NotifyUserConfirmation(UserConfirmation.Send);

                                                          return true;
                                                      }
                                                      else
                                                      {
                                                          Crashes.NotifyUserConfirmation(UserConfirmation.DontSend);

                                                          return false;
                                                      }
                                                  };

            Crashes.SendingErrorReport += (object sender, SendingErrorReportEventArgs sere) =>
                                          {
                                              // Your code, e.g. to present a custom UI.
                                              //MessageBox.Show("クラッシュ レポートを送っています。"
                                              //              , "Sending Crash Report"
                                              //              , MessageBoxButton.OK
                                              //              , MessageBoxImage.Information);
                                          };

            Crashes.SentErrorReport += (object sender, SentErrorReportEventArgs sere) =>
                                       {
                                           // Your code, e.g. to hide a custom UI.
                                           MessageBox.Show("クラッシュ レポートの送信を完了しました。"
                                                         , "Send Crash Report Completed"
                                                         , MessageBoxButton.OK
                                                         , MessageBoxImage.Information);
                                       };

            Crashes.FailedToSendErrorReport += (object sender, FailedToSendErrorReportEventArgs ftsere) =>
                                               {
                                                   // Your code goes here.
                                                   //MessageBox.Show("クラッシュ レポートの送信に失敗しました。"
                                                   //              , "Send Crash Report Failed"
                                                   //              , MessageBoxButton.OK
                                                   //              , MessageBoxImage.Information);
                                               };

            //Crashes.GetErrorAttachments = (ErrorReport report) =>
            //                              {
                                              //ErrorAttachmentLog textLog =
                                              //    ErrorAttachmentLog.AttachmentWithText("This is a text attachment.", "text.txt");

                                              //byte[] imageBuffer;
                                              //using (var bitmap = new Bitmap("test-image.png"))
                                              //    using (var ms = new MemoryStream())
                                              //{
                                              //    bitmap.Save(ms, ImageFormat.Png);
                                              //    imageBuffer = ms.GetBuffer();
                                              //}

                                              //ErrorAttachmentLog binaryLog =
                                              //    ErrorAttachmentLog.AttachmentWithBinary(imageBuffer, "test-image.png", "image/jpeg");

                                              //MessageBox.Show("Get Error Attachments");

                                              //return new List<ErrorAttachmentLog> {textLog, binaryLog};
                                              //return new List<ErrorAttachmentLog> { textLog };
                                          //};

            AppCenter.Start(AppToken, typeof(Analytics), typeof(Crashes));
            AppCenterAnalytics.Initialize();
            AppCenterAnalytics.SetCountryCode();
        }
    }
}