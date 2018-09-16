using MaterialDesign.Dialog.Example.Dialogs.ViewModels;

namespace MaterialDesign.Dialog.Example.Services
{
    public class ProgressStatus
    {
        private readonly ProgressDialogViewModel _progressDialogView;

        public ProgressStatus(ProgressDialogViewModel progressDialogView)
        {
            _progressDialogView = progressDialogView;
        }

        public void SetMessage(string message) => _progressDialogView.Message = message;

        public void UpdateProgress(int progress) => _progressDialogView.Progress = progress;

        public void Complete() => _progressDialogView.Completed = true;
    }
}