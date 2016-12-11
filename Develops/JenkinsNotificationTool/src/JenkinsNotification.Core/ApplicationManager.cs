namespace JenkinsNotification.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using JenkinsNotification.Core.ComponentModels;
    using JenkinsNotification.Core.Configurations;
    using JenkinsNotification.Core.Properties;
    using JenkinsNotification.Core.Services;
    using Microsoft.Practices.Prism.Mvvm;

    /// <summary>
    /// このアプリケーションの機能管理クラスです。
    /// </summary>
    /// <remarks>
    /// <see cref="IBalloonTipService"/> はTaskbarIcon がなければその機能を使用することができません。<para/>
    /// TaskbarIcon を参照するにはView をインスタンス化する必要があります。<para/>
    /// View をインスタンス化するとViewModelLocator の機能によりViewModel が自動生成されますが、
    /// ViewModel には<see cref="IBalloonTipService"/> が必要です。<para/>
    /// 循環参照のような連鎖を回避するため、まず<see cref="SetDefaultViewModelLocater"/> メソッドで
    /// インジェクションサービスを設定しておき、<see cref="Initialize"/> メソッドで当クラスのインスタンス化を行います。<para/>
    /// よって、<see cref="IBalloonTipService"/> は当クラスだけインスタンスを持っています。<para/>
    /// <see cref="IServicesProvider"/> に<see cref="IBalloonTipService"/> が存在しないのはこのためです。<para/>
    /// </remarks>
    public sealed class ApplicationManager
    {
        #region Fields

        /// <summary>
        /// バルーン表示サービス
        /// </summary>
        private readonly IBalloonTipService _balloonTipService;

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="balloonTipService">バルーン表示サービス</param>
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

        /// <summary>
        /// バルーン表示サービスを取得します。
        /// </summary>
        public IBalloonTipService BalloonTipService => _balloonTipService;
        
        #endregion

        #region Methods

        /// <summary>
        /// アプリケーション機能の初期化処理を実行します。
        /// </summary>
        /// <param name="balloonTipService">バルーン表示サービス</param>
        public static void Initialize(IBalloonTipService balloonTipService)
        {
            //
            // 構成ファイルを読み込む。
            //
            ApplicationConfiguration.LoadCurrent();

            //
            // このインスタンスを生成する。
            //
            Instance = new ApplicationManager(balloonTipService);
        }

        /// <summary>
        /// ViewModel の生成ルールを設定します。<para/>
        /// このメソッドは最初のView を生成する前に実行してください。
        /// </summary>
        /// <param name="servicesProvider">インジェクション サービス</param>
        public static void SetDefaultViewModelLocater(IServicesProvider servicesProvider)
        {
            //
            // ViewModel を生成する場合、コンストラクタの引数にインジェクション サービスを設定する。
            //
            ViewModelLocationProvider.SetDefaultViewModelFactory(
                viewModelType => Activator.CreateInstance(viewModelType, servicesProvider));

            //
            // View に設定したViewModel 属性の型によってView とViewModel を紐付けます。
            //
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(
                viewType =>
                {
                    var vmType = viewType.GetTypeInfo().GetCustomAttribute<ViewModelAttribute>();
                    return vmType?.ViewModelType;
                });
        }

        /// <summary>
        /// このアプリケーションを終了します。
        /// </summary>
        public void Shutdown()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}