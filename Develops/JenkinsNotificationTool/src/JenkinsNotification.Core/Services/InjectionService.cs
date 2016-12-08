namespace JenkinsNotification.Core.Services
{
    public class InjectionService : IInjectionService
    {
        private readonly IDialogService _dialogService;
        
        private readonly IViewService _viewService;

        public IDialogService DialogService => _dialogService;
        
        public IViewService ViewService => _viewService;

        public InjectionService()
        {
            _dialogService = new DialogService();
            _viewService = new ViewService();
        }
    }
}