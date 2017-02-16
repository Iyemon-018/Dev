namespace Prism._01.BootstrapperSample.Attributes
{
    using System;

    /// <summary>
    /// Viewが使用するViewModelの型を指定するための属性クラスです。
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class ViewModelAttribute : Attribute
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type">ViewModelの型</param>
        public ViewModelAttribute(Type type)
        {
            Type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// ViewModelの型を取得します。
        /// </summary>
        public Type Type { get; private set; }

        #endregion
    }
}