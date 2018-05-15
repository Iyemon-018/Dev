using System;
using GCP.VisionAPI.Sample.Views;
using System.Windows;
using GCP.VisionAPI.Sample.Services;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity;

namespace GCP.VisionAPI.Sample
{
    class Bootstrapper : UnityBootstrapper
    {

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog) ModuleCatalog;
            //moduleCatalog.AddModule(typeof(YOUR_MODULE));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<ISelectionDialogService, SelectionDialogService>();

            //ISelectionDialogService selectionDialogService = new SelectionDialogService();
            //ViewModelLocationProvider.SetDefaultViewModelFactory(viewModelType =>
            //    Activator.CreateInstance(viewModelType, new object[] {selectionDialogService}));

            //ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            //{

            //});
        }
    }
}