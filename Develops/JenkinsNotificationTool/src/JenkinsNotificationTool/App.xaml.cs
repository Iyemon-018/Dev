using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JenkinsNotificationTool
{
    using JenkinsNotification.Core;
    using JenkinsNotification.Core.Configurations;
    using JenkinsNotification.Core.Services;
    using JenkinsNotification.CustomControls.Services;
    using JenkinsNotificationTool.Views;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationManager.SetDefaultViewModelLocater(new InjectionService());

            base.OnStartup(e);

            var view = new MainView();
            ApplicationManager.Initialize(new BalloonTipService(view.TaskbarIcon));
        }
    }
}
