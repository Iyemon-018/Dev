using System.Threading.Tasks;

namespace MaterialDesign.Dialog.Example.Services
{
    public interface IDialogService
    {
        Task<bool> Question(string message);
    }
}