namespace NavigationSample.ViewModels
{
    using NavigationSample.Services;

    public class AirControllerViewModel : PageViewModelBase
    {
        public AirControllerViewModel(ITransitionService transitionService) : base(transitionService)
        {
        }

        protected override TransitionPageView NextView => TransitionPageView.Map;

        protected override TransitionPageView PrevVew => TransitionPageView.Drawing;
    }
}