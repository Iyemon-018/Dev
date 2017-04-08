namespace NavigationSample.ViewModels
{
    using NavigationSample.Services;

    public abstract class ShellViewModelBase : ViewModelBase
    {
        private readonly IDataStore _dataStore;

        protected ShellViewModelBase(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IDataStore DataStore => _dataStore;
    }
}