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

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            //ApplicationConfiguration.SaveCurrent();
            ApplicationConfiguration.LoadCurrent();
        }
    }
}
