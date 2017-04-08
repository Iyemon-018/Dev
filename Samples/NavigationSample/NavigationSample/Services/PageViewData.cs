namespace NavigationSample.Services
{
    using Prism.Mvvm;
    public class PageViewData : BindableBase, IPageViewData
    {
        /// <summary>
        /// タイトル
        /// </summary>
        private string _title;

        /// <summary>
        /// タイトルを設定、または取得します。
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}