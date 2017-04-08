namespace NavigationSample.ViewModels
{
    using NavigationSample.Services;

    public class MapImageViewModel : PageViewModelBase
    {
        public MapImageViewModel(ITransitionService transitionService, IDataStore dataStore) : base(transitionService, dataStore)
        {
        }
        protected override TransitionPageView NextView => TransitionPageView.Music;

        protected override TransitionPageView PrevVew => TransitionPageView.AirController;
    }
}