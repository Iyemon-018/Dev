namespace NavigationSample.Services
{
    public enum TransitionPageView
    {
        Map,
        Music,
        Drawing,
        AirController,
    }

    public enum TransitionDialogView
    {
        Main,
        Configuration,
    }

    public interface ITransitionService
    {
        void Navigate(TransitionPageView pageView, object parameter);

        void ShowDialog(TransitionDialogView dialogView);

        void SetService(object navigationService);

        void SetDataStore(IDataStore dataStore);
    }
}