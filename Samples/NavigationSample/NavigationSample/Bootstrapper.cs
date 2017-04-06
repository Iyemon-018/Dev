namespace NavigationSample
{
    using System.Reflection;
    using System.Windows;
    using Microsoft.Practices.Unity;
    using NavigationSample.Services;
    using Prism.Mvvm;
    using Prism.Unity;

    public class Bootstrapper : UnityBootstrapper
    {
        #region Methods

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override void ConfigureViewModelLocator()
        {
            //base.ConfigureViewModelLocator();

            //
            // View に設定したViewModel 属性の型によってView とViewModel を紐付けます。
            //
            ViewModelLocationProvider
                .SetDefaultViewTypeToViewModelTypeResolver(
                    viewType =>
                    {
                        var vmType = viewType.GetTypeInfo().GetCustomAttribute<ViewModelResolveAttribute>();
                        return vmType?.ViewModelType;
                    });

            //
            // ViewModel を生成する場合、コンストラクタの引数にインジェクション サービスを設定する。
            //
            ViewModelLocationProvider.SetDefaultViewModelFactory(viewModelType => Container.Resolve(viewModelType));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            var app = Application.Current;
            var mainView = Shell as MainWindow;
            app.MainWindow = mainView;

            var transitionService = new TransitionService(mainView.frame.NavigationService);
            Container.RegisterInstance(transitionService);
            mainView.Show();
            transitionService.Navigate(TransitionPageView.Map, null);
        }

        #endregion
    }
}