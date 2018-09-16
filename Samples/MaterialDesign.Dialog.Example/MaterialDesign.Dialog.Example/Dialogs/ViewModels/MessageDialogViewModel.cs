using Prism.Mvvm;

namespace MaterialDesign.Dialog.Example.Dialogs.ViewModels
{
    public class MessageDialogViewModel : BindableBase
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

    }
}