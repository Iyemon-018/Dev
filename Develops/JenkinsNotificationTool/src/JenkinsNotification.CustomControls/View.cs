namespace JenkinsNotification.CustomControls
{
    using System.Windows;
    using Core.Utility;
    using Microsoft.Practices.Prism.Mvvm;

    /// <summary>
    /// このアプリケーション専用のView コンポーネントクラスです。
    /// </summary>
    /// <seealso cref="IView" />
    /// <seealso cref="Window" />
    public class View : Window, IView
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static View()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(View), new FrameworkPropertyMetadata(typeof(View)));
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected View()
        {
            //
            // ViewModel インジェクション機能を有効化する。
            //
            ViewUtility.InjectionViewModelLocater(this);
        }

        #endregion
    }
}