using System;
using System.Threading.Tasks;
using MaterialDesign.Dialog.Example.Dialogs;
using MaterialDesign.Dialog.Example.Dialogs.ViewModels;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign.Dialog.Example.Services
{
    public class ProgressDialogService : IProgressDialogService
    {
        private readonly string _identifier;

        private Action<ProgressStatus> _action;

        private ProgressDialogViewModel _viewModel;

        public ProgressDialogService(string identifier)
        {
            _identifier = identifier;
        }

        public async Task Begin(int count, Action<ProgressStatus> task)
        {
            _action = task;
            _viewModel = new ProgressDialogViewModel
                         {
                             Count = count, Progress = 0
                         };

            ProgressDialog dialog = new ProgressDialog
                                    {
                                        DataContext = _viewModel
                                    };
            await DialogHost.Show(dialog, _identifier, OpenedEventHandler);
        }

        private void OpenedEventHandler(object sender, DialogOpenedEventArgs e)
        {
            _action?.Invoke(new ProgressStatus(_viewModel));
            _viewModel.CompletedProgress += (o, args) => { e.Session.Close(null); };
        }
    }
}