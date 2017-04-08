namespace NavigationSample.Services
{
    using Prism.Mvvm;
    public class DataStore : BindableBase, IDataStore
    {
        /// <summary>
        /// 現在のページ情報
        /// </summary>
        private IPageViewData _currentViewData;

        /// <summary>
        /// 現在のページ情報を設定、または取得します。
        /// </summary>
        public IPageViewData CurrentViewData
        {
            get { return _currentViewData; }
            private set { SetProperty(ref _currentViewData, value); }
        }

        public DataStore()
        {
            _currentViewData = new PageViewData();
        }
    }
}