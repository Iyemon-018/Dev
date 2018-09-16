using MaterialDesign.Dialog.Example.ViewModels;
using Prism.Mvvm;

namespace MaterialDesign.Dialog.Example.Dialogs.ViewModels
{
    public class UserDataAddViewModel : BindableBase
    {
        private UserDataViewModel _userData;
    
        public UserDataViewModel UserData
        {
            get => _userData;
            private set => SetProperty(ref _userData, value);
        }

        public UserDataAddViewModel()
        {
            _userData = new UserDataViewModel();
        }
    }
}