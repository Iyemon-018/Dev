using System;
using System.Threading.Tasks;
using MaterialDesign.Dialog.Example.Dialogs;
using MaterialDesign.Dialog.Example.Dialogs.ViewModels;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign.Dialog.Example.Services
{
    public class DialogService : IDialogService
    {
        private readonly string _identifier;

        public DialogService(string identifier)
        {
            _identifier = identifier;
        }

        public async Task<bool> Question(string message)
        {
            MessageDialog dialog = new MessageDialog
                                   {
                                       DataContext = new MessageDialogViewModel
                                                     {
                                                         Message = message
                                                     }
                                   };
            object result = await DialogHost.Show(dialog, _identifier);
            return (result is bool selectedResult) && selectedResult;
        }
    }
}