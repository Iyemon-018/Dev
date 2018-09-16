using System.Threading.Tasks;
using MaterialDesign.Dialog.Example.ViewModels;

namespace MaterialDesign.Dialog.Example.Services
{
    public interface IUserDataAddDialogService
    {
        Task<UserDataViewModel> Show();
    }
}