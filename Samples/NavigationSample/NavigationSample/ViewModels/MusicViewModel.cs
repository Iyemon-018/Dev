namespace NavigationSample.ViewModels
{
    using NavigationSample.Services;

    public class MusicViewModel : PageViewModelBase
    {
        public MusicViewModel(ITransitionService transitionService, IDataStore dataStore) : base(transitionService, dataStore)
        {
        }
        protected override TransitionPageView NextView => TransitionPageView.Drawing;

        protected override TransitionPageView PrevVew => TransitionPageView.Map;
    }
}