namespace NavigationSample.ViewModels
{
    using NavigationSample.Services;

    public class AirControllerViewModel : PageViewModelBase
    {
        public AirControllerViewModel(ITransitionService transitionService, IDataStore dataStore) : base(transitionService, dataStore)
        {
        }

        protected override TransitionPageView NextView => TransitionPageView.Map;

        protected override TransitionPageView PrevVew => TransitionPageView.Drawing;
    }
}