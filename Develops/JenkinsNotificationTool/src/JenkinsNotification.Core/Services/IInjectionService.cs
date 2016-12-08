namespace JenkinsNotification.Core.Services
{
    public interface IInjectionService
    {
        IDialogService DialogService { get; }

        IViewService ViewService { get; }
    }
}