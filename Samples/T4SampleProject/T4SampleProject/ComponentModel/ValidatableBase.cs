using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Runtime.CompilerServices;

namespace T4SampleProject.ComponentModel
{
    public abstract class ValidatableBase : INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfoインターフェース
        
        /// <summary>
        /// 検証エラー結果が変化したことを通知するイベント
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// 指定したプロパティのエラー情報をすべて取得する。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>検証エラー情報</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName) == true)
            {
                return null;
            }

            if (_errors.ContainsKey(propertyName) == true
                && _errors[propertyName] != null
                && _errors[propertyName].Count > 0)
            {
                return _errors[propertyName];
            }
            else
            {
                return _errors.SelectMany(err => err.Value.ToList());
            }
        }

        /// <summary>
        /// オブジェクトがエラーかどうか。(true:エラー, false:正常)
        /// </summary>
        public bool HasErrors
        {
            get { return _errors.Any(errors => errors.Value != null && _errors.Values.Count > 0); }
        }

        #endregion //INotifyDataErrorInfoインターフェース

        #region メンバ

        /// <summary>
        /// 検証エラーメッセージ
        /// </summary>
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        /// <summary>
        /// 非同期検証用ロックオブジェクト
        /// </summary>
        private object _lock = new object();

        #endregion //メンバ

        #region プロパティ

        /// <summary>
        /// オブジェクトが正常かどうか(true:正常, false:異常)
        /// </summary>
        public bool HasNotErrors { get { return !HasErrors; } }

        /// <summary>
        /// エラーメッセージをすべて取得する。
        /// </summary>
        public IEnumerable<string> AllErrorMessages
        {
            get
            {
                return _errors.SelectMany(errors => errors.Value);
            }
        }

        #endregion //プロパティ

        #region 検証結果通知関連

        /// <summary>
        /// 検証エラー結果変更通知を発行する。
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnErrorChanged([CallerMemberName]string propertyName = "")
        {
            var h = ErrorsChanged;
            if (h != null)
            {
                h(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 指定したプロパティを検証する。
        /// </summary>
        /// <param name="value">プロパティ値</param>
        /// <param name="propertyName">プロパティ名</param>
        protected void ValidateProperty(object value, [CallerMemberName]string propertyName = "")
        {
            // 依存関係プロパティの仕組み上、非同期で検証ロジックが実行される。
            lock (_lock)
            {
                var results = new List<ValidationResult>();
                Validator.TryValidateProperty(value
                                            , new ValidationContext(this, null, null)
                                            {
                                                MemberName = propertyName,
                                            }
                                            , results);

                if (_errors.ContainsKey(propertyName) == true)
                {
                    _errors.Remove(propertyName);
                }

                // 検証エラー結果を通知
                // エラーがない場合は、この１回でおｋ
                OnErrorChanged(propertyName);

                // エラーが有る場合は、以下で通知する。
                HandleValidationResults(results);
            }
        }

        /// <summary>
        /// オブジェクトの全データを検証する。
        /// </summary>
        public void Validate()
        {
            // 依存関係プロパティの仕組み上、非同期で検証ロジックが実行される。
            lock (_lock)
            {
                var results = new List<ValidationResult>();
                Validator.TryValidateObject(this
                                            , new ValidationContext(this, null, null)
                                            , results
                                            , true);

                // エラー情報をすべてクリア
                var propertyNames = _errors.Keys.ToList();
                _errors.Clear();

                foreach (var propertyName in propertyNames)
                {
                    OnErrorChanged(propertyName);
                }

                // エラーが有る場合は以下で通知する。
                HandleValidationResults(results);
            }
        }

        /// <summary>
        /// 検証エラー結果をハンドリングする。
        /// </summary>
        /// <param name="results">検証結果リスト</param>
        private void HandleValidationResults(List<ValidationResult> results)
        {
            // プロパティ名でグルーピング
            var propertyNames = from r in results
                                from memberName in r.MemberNames
                                group r by memberName into g
                                select g;

            // エラーメッセージ情報に追加する。
            foreach (var p in propertyNames)
            {
                var messages = p.Select(r => r.ErrorMessage).ToList();
                _errors.Add(p.Key, messages);
                OnErrorChanged(p.Key);
            }
        }

        /// <summary>
        /// プロパティがエラーであることを通知する。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <exception cref="System.ArgumentNullException">errorMessage が空の場合、発行する。</exception>
        protected void NotifyError(string propertyName, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage) == true)
            {
                throw new ArgumentNullException("エラーメッセージを必ず設定してください。", "errorMessage");
            }

            if (_errors.ContainsKey(propertyName) == false)
            {
                _errors.Add(propertyName, new List<string>());
            }

            _errors[propertyName].Add(errorMessage);
            OnErrorChanged(propertyName);
        }

        #endregion //検証結果通知関連
    }
}
