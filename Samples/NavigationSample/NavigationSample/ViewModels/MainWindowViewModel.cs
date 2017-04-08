namespace NavigationSample.ViewModels
{
    using Microsoft.Practices.Prism.Commands;
    using NavigationSample.Services;

    public class MainWindowViewModel : ShellViewModelBase
    {
        public MainWindowViewModel(IDataStore dataStore) : base(dataStore)
        {
        }
    }
}