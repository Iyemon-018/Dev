using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;

namespace DataGridColumnBinding
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            IsVisibleDate = true;
            IsVisibleTime = true;

            Times = new ObservableCollection<DateTime>();

            foreach (var value in Enumerable.Range(0, 20))
            {
                Times.Add(DateTime.Now.AddHours(value));
            }
        }

        /// <summary>
        /// 日付を表示するかどうか
        /// </summary>
        private bool _isVisibleDate;

        /// <summary>
        /// 日付を表示するかどうか
        /// </summary>
        public bool IsVisibleDate
        {
            get { return _isVisibleDate; }
            set { SetProperty(ref _isVisibleDate, value); }
        }

        /// <summary>
        /// 時刻を表示するかどうか
        /// </summary>
        private bool _isVisibleTime;

        /// <summary>
        /// 時刻を表示するかどうか
        /// </summary>
        public bool IsVisibleTime
        {
            get { return _isVisibleTime; }
            set { SetProperty(ref _isVisibleTime, value); }
        }

        public ObservableCollection<DateTime> Times { get; private set; }
    }
}