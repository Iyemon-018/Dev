namespace NavigationSample.ViewModels
{
    using System;
    using NavigationSample.Services;
    using Prism.Commands;

    public abstract class PageViewModelBase : ViewModelBase
    {
        private readonly ITransitionService _transitionService;

        protected PageViewModelBase(ITransitionService transitionService)
        {
            if (transitionService == null) throw new ArgumentNullException(nameof(transitionService));
            _transitionService = transitionService;

            NextCommand = new DelegateCommand(ExecuteNextCommand);
            PrevCommand = new DelegateCommand(ExecutePrevCommand);
        }

        private void ExecutePrevCommand()
        {
            TransitionService.Navigate(PrevVew, null);
        }

        private void ExecuteNextCommand()
        {
            TransitionService.Navigate(NextView, null);
        }

        protected ITransitionService TransitionService => _transitionService;

        public DelegateCommand NextCommand { get; private set; }

        public DelegateCommand PrevCommand { get; private set; }

        protected abstract TransitionPageView NextView { get; }

        protected abstract TransitionPageView PrevVew { get; }
    }
}