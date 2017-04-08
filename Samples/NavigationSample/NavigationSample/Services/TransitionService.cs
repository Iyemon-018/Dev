namespace NavigationSample.Services
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Navigation;

    public class TransitionService : ITransitionService
    {
        private NavigationService _navigation;

        private static readonly IDictionary<TransitionPageView, string> PageViewMap
            = new Dictionary<TransitionPageView, string>
              {
                {TransitionPageView.Map, "MapImageView"},
                {TransitionPageView.Music, "MusicView"},
                {TransitionPageView.Drawing, "DrawingView"},
                {TransitionPageView.AirController, "AirControllerView"},
              };

        private static readonly IDictionary<TransitionPageView, string> PageViewTitleMap
            = new Dictionary<TransitionPageView, string>
              {
                  {TransitionPageView.Map, "Map"},
                  {TransitionPageView.Music, "Music"},
                  {TransitionPageView.Drawing, "Drawing"},
                  {TransitionPageView.AirController, "Air Controller"},
              };

        private IDataStore _dataStore;

        public void Navigate(TransitionPageView pageView, object parameter)
        {
            var typeName = PageViewMap[pageView];
            var uri = new Uri($"Views/{typeName}.xaml", UriKind.Relative);
            _navigation.Navigate(uri, parameter);
            _dataStore.CurrentViewData.Title = PageViewTitleMap[pageView];
        }
        
        public void ShowDialog(TransitionDialogView dialogView)
        {
            throw new System.NotImplementedException();
        }

        public void SetService(object navigationService)
        {
            _navigation = navigationService as NavigationService;
        }

        public void SetDataStore(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
    }
}