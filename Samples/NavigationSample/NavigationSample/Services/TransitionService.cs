namespace NavigationSample.Services
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    public class TransitionService : ITransitionService
    {
        private readonly NavigationService _navigation;

        private static readonly IDictionary<TransitionPageView, string> PageViewMap
            = new Dictionary<TransitionPageView, string>
              {
                {TransitionPageView.Map, "MapImageView"},
                {TransitionPageView.Music, "MusicView"},
                {TransitionPageView.Drawing, "DrawingView"},
                {TransitionPageView.AirController, "AirControllerView"},
              };

        public TransitionService(NavigationService navigation)
        {
            if (navigation == null) throw new ArgumentNullException(nameof(navigation));
            _navigation = navigation;
        }

        public void Navigate(TransitionPageView pageView, object parameter)
        {
            var typeName = PageViewMap[pageView];
            var uri = new Uri($"Views/{typeName}.xaml", UriKind.Relative);
            _navigation.LoadCompleted += NavigationOnLoadCompleted;
            _navigation.Navigate(uri, parameter);
        }

        private void NavigationOnLoadCompleted(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;
            var vmType = page.GetType().GetTypeInfo().GetCustomAttribute<ViewModelResolveAttribute>().ViewModelType;
            var vm = Activator.CreateInstance(vmType, new object[] {this});
            page.DataContext = vm;
        }

        public void ShowDialog(TransitionDialogView dialogView)
        {
            throw new System.NotImplementedException();
        }
    }
}