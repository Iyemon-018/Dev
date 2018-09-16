using System;
using System.Threading.Tasks;

namespace MaterialDesign.Dialog.Example.Services
{
    public interface IProgressDialogService
    {
        Task Begin(int count, Action<ProgressStatus> task);
    }
}