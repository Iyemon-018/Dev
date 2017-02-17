namespace Prism._01.BootstrapperSample
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Prism.Mvvm;

    /// <summary>
    /// 全てのViewModel の汎用的な機能を実装したクラスです。
    /// </summary>
    /// <seealso cref="BindableBase" />
    /// <seealso cref="INotifyDataErrorInfo" />
    public abstract class ViewModelBase : BindableBase, INotifyDataErrorInfo
    {
        #region Fields

        /// <summary>
        /// このオブジェクトの持つエラー情報コンテナ
        /// </summary>
        private readonly ErrorsContainer<string> _errorsContainer;

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected ViewModelBase()
        {
            _errorsContainer = new ErrorsContainer<string>(OnErrorsChanged);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            var isChanged = base.SetProperty(ref storage, value, propertyName);
            if (!isChanged) return false;

            ValidateProperty(propertyName, value);

            return true;
        }

        /// <summary>
        /// <see cref="ErrorsChanged"/> イベントを呼び出します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// プロパティの値を検証します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="value">変更されたプロパティの値</param>
        /// <remarks>
        /// このメソッドは、属性による検証にのみ対応しています。
        /// </remarks>
        private void ValidateProperty<T>(string propertyName, T value)
        {
            var context = new ValidationContext(this) {MemberName = propertyName};
            var errors = new List<ValidationResult>();
            var validate = Validator.TryValidateProperty(value, context, errors);
            if (validate)
            {
                // エラーなし
                _errorsContainer.ClearErrors(propertyName);
            }
            else
            {
                // エラーあり
                _errorsContainer.SetErrors(propertyName, errors.Select(x => x.ErrorMessage));
            }
        }

        #endregion

        #region INotifyDataErrorInfo Members

        /// <summary>
        /// 指定されたプロパティまたはエンティティ全体の検証エラーを取得します。
        /// </summary>
        /// <param name="propertyName">検証エラーを取得するプロパティの名前。または、エンティティ レベルのエラーを取得する場合は null または <see cref="F:System.String.Empty" />。</param>
        /// <returns>プロパティまたはエンティティの検証エラー。</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsContainer.GetErrors(propertyName);
        }

        /// <summary>
        /// エンティティに検証エラーがあるかどうかを示す値を取得します。
        /// </summary>
        public bool HasErrors => _errorsContainer.HasErrors;

        /// <summary>
        /// プロパティまたはエンティティ全体の検証エラーが変更されたときに発生します。
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion
    }
}