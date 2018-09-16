using Prism.Mvvm;

namespace MaterialDesign.Dialog.Example.Dialogs.ViewModels
{
    public class ProgressDialogViewModel : BindableBase
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private int _count;

        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        private int _progress;

        public int Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

    }
}