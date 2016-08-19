namespace Mc.Common.Wpf.Core.ComponentModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataAnnotations;

    /// <summary>
    /// プロパティ値の検証結果を通知することのできるオブジェクトの基底クラスです。
    /// </summary>
    /// <seealso cref="Mc.Common.Wpf.Core.ComponentModels.BindableBase" />
    /// <seealso cref="System.ComponentModel.INotifyDataErrorInfo" />
    public abstract class ViewModelBase : BindableBase, INotifyDataErrorInfo
    {
        #region Fields

        /// <summary>
        /// プロパティに対するエラー情報
        /// </summary>
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        /// <summary>
        /// オブジェクトの検証の非同期ロックオブジェクト
        /// </summary>
        private readonly object _validateLock = new object();

        #endregion

        #region Events

        /// <summary>
        /// プロパティまたはエンティティ全体の検証エラーが変更されたときに発生します。
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion

        #region Properties

        /// <summary>
        /// オブジェクトの全てのエラーメッセージを取得します。
        /// </summary>
        public IEnumerable<string> Errors
        {
            get { return _errors.SelectMany(x => x.Value); }
        }

        /// <summary>
        /// エンティティに検証エラーがあるかどうかを示す値を取得します。
        /// </summary>
        /// <returns>
        /// 現在エンティティに検証エラーがある場合は true。それ以外の場合は false。
        /// </returns>
        public bool HasErrors
        {
            get { return _errors.Any(errors => errors.Value != null && _errors.Values.Count > 0); }
        }

        /// <summary>
        /// このオブジェクトが正常かどうかを取得します。
        /// </summary>
        public bool IsValidate
        {
            get { return !HasErrors; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 指定されたプロパティまたはエンティティ全体の検証エラーを取得します。
        /// </summary>
        /// <param name="propertyName">検証エラーを取得するプロパティの名前。または、エンティティ レベルのエラーを取得する場合は null または <see cref="F:System.String.Empty" />。</param>
        /// <returns>プロパティまたはエンティティの検証エラー。</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return _errors.SelectMany(x => x.Value.ToList());
            }

            if (_errors.ContainsKey(propertyName)
             && _errors[propertyName] != null
             && _errors[propertyName].Count > 0)
            {
                return _errors[propertyName].ToList();
            }

            return null;
        }

        /// <summary>
        /// 当オブジェクトの検証を実施します。
        /// </summary>
        /// <param name="parameter">検証パラメータ</param>
        /// <returns>検証結果(true:正常, false:異常)</returns>
        public bool Validate(IValidationParameter parameter = null)
        {
            lock (_validateLock)
            {
                var validateResults = new List<ValidationResult>();
                var result          = Validator.TryValidateObject(this
                                                                , new ValidationContext(this, null, null)
                                                                , validateResults
                                                                , true);

                //
                // 現在発生しているエラーの情報をクリアする。
                // クリアしなければエラー通知が解消されないため。
                //
                ClearErrors();

                // Validator で検証エラーとなった結果を元にエラー通知を行う。
                HandledValidationResult(validateResults);

                // 独自の検証を実施する。
                if (!ValidateCore(parameter))
                {
                    result = false;
                }

                return result;
            }
        }

        /// <summary>
        /// 当オブジェクトのエラーを全てクリアする。
        /// </summary>
        protected void ClearErrors()
        {
            //
            // エラーになっているプロパティ名リストを保持する。
            // クリア後に変更通知を発行する。
            //
            var propertyNames = _errors.Keys.ToList();
            _errors.Clear();

            foreach (var propertyName in propertyNames)
            {
                OnErrorChanged(propertyName);
            }
        }

        /// <summary>
        /// プロパティがエラーであることを通知する。
        /// </summary>
        /// <param name="propertyName">エラー通知発行するプロパティ名</param>
        /// <param name="errorMessage">プロパティ名</param>
        protected void NotifyError(string propertyName, string errorMessage)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, new List<string>());
            }

            _errors[propertyName].Add(errorMessage);
            OnErrorChanged(propertyName);
        }

        /// <summary>
        /// <see cref="ErrorsChanged"/> イベントを呼び出します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected virtual void OnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 当オブジェクトの検証を実施します。
        /// </summary>
        /// <param name="parameter">検証パラメータ</param>
        /// <returns>検証結果(true:正常, false:異常)</returns>
        protected virtual bool ValidateCore(IValidationParameter parameter = null)
        {
            return true;
        }

        /// <summary>
        /// 特定のプロパティを検証します。
        /// </summary>
        /// <param name="value">プロパティの値</param>
        /// <param name="propertyName">検証対象のプロパティ名</param>
        /// <returns>検証結果(true:正常, false:異常)</returns>
        protected bool ValidateProperty(object value, string propertyName)
        {
            lock (_validateLock)
            {
                var validationResults = new List<ValidationResult>();
                var result            = Validator.TryValidateProperty(value
                                                                    , new ValidationContext(this, null, null)
                                                                      {
                                                                          MemberName = propertyName
                                                                      }
                                                                    , validationResults);

                // プロパティの既存のエラー情報はクリアする。
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                }

                // 検証結果を通知する。
                // エラーがクリアされたことを通知する。
                OnErrorChanged(propertyName);

                // エラーが有った内容はここで通知する。
                HandledValidationResult(validationResults);

                return result;
            }
        }

        /// <summary>
        /// 検証エラーの結果をハンドリングし、通知します。
        /// </summary>
        /// <param name="results">検証エラーリスト</param>
        private void HandledValidationResult(IEnumerable<ValidationResult> results)
        {
            var names = from r in results
                        from member in r.MemberNames
                        group r by member
                        into g
                        select g;

            foreach (var name in names)
            {
                var messages = name.Select(x => x.ErrorMessage).ToList();
                _errors.Add(name.Key, messages);
                OnErrorChanged(name.Key);
            }
        }

        #endregion
    }
}