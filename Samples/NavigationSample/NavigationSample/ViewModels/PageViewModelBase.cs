namespace NavigationSample.ViewModels
{
    using System;
    using NavigationSample.Services;
    using Prism.Commands;

    public abstract class PageViewModelBase : ViewModelBase
    {
        private readonly ITransitionService _transitionService;

        private readonly IDataStore _dataStore;

        protected PageViewModelBase(ITransitionService transitionService, IDataStore dataStore)
        {
            if (transitionService == null) throw new ArgumentNullException(nameof(transitionService));
            _transitionService = transitionService;
            _dataStore = dataStore;

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

        protected IDataStore DataStore => _dataStore;

        public DelegateCommand NextCommand { get; private set; }

        public DelegateCommand PrevCommand { get; private set; }

        protected abstract TransitionPageView NextView { get; }

        protected abstract TransitionPageView PrevVew { get; }
    }
}