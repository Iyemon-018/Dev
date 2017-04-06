namespace NavigationSample.Services
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Controls;
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
        
        public void Navigate(TransitionPageView pageView, object parameter)
        {
            var typeName = PageViewMap[pageView];
            var uri = new Uri($"Views/{typeName}.xaml", UriKind.Relative);
            _navigation.Navigate(uri, parameter);
        }
        
        public void ShowDialog(TransitionDialogView dialogView)
        {
            throw new System.NotImplementedException();
        }

        public void SetService(object navigationService)
        {
            _navigation = navigationService as NavigationService;
        }
    }
}