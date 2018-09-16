using System.Windows;
using MaterialDesign.Dialog.Example.ViewModels;
using MaterialDesign.Dialog.Example.Views;
using Prism.Unity;

namespace MaterialDesign.Dialog.Example
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var shell = Shell as Shell;
            App.Current.MainWindow = shell;
            shell.DataContext = new ShellViewModel();
            shell.Show();
        }
    }
}