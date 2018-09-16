using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace MaterialDesign.Dialog.Example.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel()
        {
            ShutdownCommand = new DelegateCommand(() => App.Current.MainWindow.Close());
        }
        public ICommand ShutdownCommand { get; private set; }
    }
}