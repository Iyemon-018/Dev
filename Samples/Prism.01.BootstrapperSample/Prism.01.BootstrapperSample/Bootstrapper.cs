namespace Prism._01.BootstrapperSample
{
    using System;
    using System.Reflection;
    using System.Windows;
    using Microsoft.Practices.ServiceLocation;
    using Prism.Mvvm;
    using Prism.Unity;
    using Prism._01.BootstrapperSample.Attributes;

    /// <summary>
    /// このアプリケーションのBootstrapperクラスです。
    /// </summary>
    /// <seealso cref="Prism.Unity.UnityBootstrapper" />
    public class Bootstrapper : UnityBootstrapper
    {
        #region Methods

        /// <summary>
        /// Prismの<see cref="ViewModelLocator"/> を構成します。
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            Console.WriteLine($"> {DateTime.Now:hh:mm:ss.fff} [Info] Called ConfigureViewModelLocator.");
            //base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(
                                                               x =>
                                                               {
                                                                   // Viewに定義したViewModelAttribute属性の型によってViewModelを決定する。
                                                                   var viewModelType = x.GetCustomAttribute<ViewModelAttribute>();
                                                                   return viewModelType?.Type;
                                                               });
            ViewModelLocationProvider.SetDefaultViewModelFactory(Activator.CreateInstance);
        }

        /// <summary>
        /// アプリケーションのShellインスタンスを生成します。
        /// </summary>
        /// <returns>Shellインスタンス</returns>
        /// <remarks>
        /// Prismでは、アプリケーション開始時の依存関係オブジェクトを"Shell"と呼んでいる。
        /// このメソッドはBootstrapper開始時に"Shell"を作成するためのもの。
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            //return base.CreateShell();
            Console.WriteLine($"> {DateTime.Now:hh:mm:ss.fff} [Info] Called CreateShell.");
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        /// <summary>
        /// "Shell"の初期化を実施します。
        /// </summary>
        /// <remarks>
        /// WPFでは、メインウィンドウはApplication.Current.MainWindow なので、そこにShellを設定する。
        /// Shellが設定できればあとは表示するだけ。
        /// </remarks>
        protected override void InitializeShell()
        {
            // 基本クラスのメソッドはなにもしないのでコメントアウトしておいたほうがいい。
            //base.InitializeShell();

            Console.WriteLine($"> {DateTime.Now:hh:mm:ss.fff} [Info] Called InitializeShell.");
            Application.Current.MainWindow = Shell as Window;
            Application.Current.MainWindow.Show();
        }

        #endregion
    }
}
