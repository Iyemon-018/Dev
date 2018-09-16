using System.Threading.Tasks;
using MaterialDesign.Dialog.Example.Dialogs;
using MaterialDesign.Dialog.Example.Dialogs.ViewModels;
using MaterialDesign.Dialog.Example.ViewModels;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign.Dialog.Example.Services
{
    public class UserDataAddDialogService : IUserDataAddDialogService
    {
        private readonly string _identifier;

        public UserDataAddDialogService(string identifier)
        {
            _identifier = identifier;
        }

        public async Task<UserDataViewModel> Show()
        {
            UserDataAddDialog dialog = new UserDataAddDialog
                                       {
                                           DataContext = new UserDataAddViewModel()
                                       };
            return await DialogHost.Show(dialog, _identifier) as UserDataViewModel;
        }
    }
}