namespace JenkinsNotification.Core
{
    using System;
    using System.Reflection;
    using System.Windows;
    using Configurations;
    using JenkinsNotification.Core.ComponentModels;
    using JenkinsNotification.Core.Services;
    using Microsoft.Practices.Prism.Mvvm;

    /// <summary>
    /// このアプリケーションの機能管理クラスです。
    /// </summary>
    public sealed class ApplicationManager
    {
        private readonly IBalloonTipService _balloonTipService;

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="balloonTipService"></param>
        private ApplicationManager(IBalloonTipService balloonTipService)
        {
            _balloonTipService = balloonTipService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 唯一のインスタンスを取得します。
        /// </summary>
        public static ApplicationManager Instance { get; private set; }

        /// <summary>
        /// アプリケーション構成情報を取得します。
        /// </summary>
        public ApplicationConfiguration ApplicationConfiguration => ApplicationConfiguration.Current;

        public IBalloonTipService BalloonTipService => _balloonTipService;
        
        #endregion

        public static void Initialize(IBalloonTipService balloonTipService)
        {
            Instance = new ApplicationManager(balloonTipService);

            ApplicationConfiguration.LoadCurrent();
        }

        public static void SetDefaultViewModelLocater(IInjectionService injectionService)
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                viewModelType => Activator.CreateInstance(viewModelType, injectionService));

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(
                viewType =>
                {
                    var vmType = viewType.GetTypeInfo().GetCustomAttribute<ViewModelAttribute>();
                    return vmType?.ViewModelType;
                });
        }
        
        public void Shutdown()
        {
            Application.Current.Shutdown();
        }
    }
}