namespace NavigationSample.ViewModels
{
    using NavigationSample.Services;

    public class DrawingViewModel : PageViewModelBase
    {
        public DrawingViewModel(ITransitionService transitionService) : base(transitionService)
        {
        }
        protected override TransitionPageView NextView => TransitionPageView.AirController;

        protected override TransitionPageView PrevVew => TransitionPageView.Music;
    }
}