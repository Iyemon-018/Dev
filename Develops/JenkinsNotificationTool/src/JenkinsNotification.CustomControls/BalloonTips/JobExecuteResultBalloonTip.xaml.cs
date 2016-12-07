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

namespace JenkinsNotification.CustomControls.BalloonTips
{
    using JenkinsNotification.Core.ViewModels.Api;

    /// <summary>
    /// JobExecuteResultBalloonTip.xaml の相互作用ロジック
    /// </summary>
    public partial class JobExecuteResultBalloonTip : UserControl
    {
        private readonly IJobExecuteResult _vm;

        public JobExecuteResultBalloonTip(IJobExecuteResult executeResult)
        {
            InitializeComponent();

            _vm = executeResult;
            DataContext = _vm;
        }
    }
}
