namespace JenkinsNotification.Core.ComponentModels
{
    using System;

    /// <summary>
    /// View からViewModel を識別するための属性クラスです。
    /// </summary>
    public sealed class ViewModelAttribute : Attribute
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="viewModelType">DataContext にバインドするViewModel の型</param>
        public ViewModelAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// DataContext にバインドするViewModel の型を取得します。
        /// </summary>
        public Type ViewModelType { get; }

        #endregion
    }
}