namespace JenkinsNotification.Core.ComponentModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Microsoft.Practices.Prism.ViewModel;
    using PropertySupport = Microsoft.Practices.Prism.Mvvm.PropertySupport;

    /// <summary>
    /// リスナーにエラーを通知するためのコンテナクラスです。
    /// </summary>
    /// <typeparam name="T">通知対象のエラーデータの型</typeparam>
    /// <seealso cref="Microsoft.Practices.Prism.ViewModel.ErrorsContainer{T}" />
    public class ErrorsContainerCustom<T> : ErrorsContainer<T>
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="raiseErrorsChanged">エラーを通知するためのイベントハンドラ</param>
        public ErrorsContainerCustom(Action<string> raiseErrorsChanged) : base(raiseErrorsChanged)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 現在のエラー情報をすべてクリアします。
        /// </summary>
        public void ClearAll()
        {
            validationResults.Clear();
        }

        /// <summary>
        /// プロパティにエラーデータを設定します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="validationResult">設定するエラーデータ</param>
        public void SetError(string propertyName, T validationResult)
        {
            var localPropertyName = propertyName ?? string.Empty;
            var hasCurrentValidationResults = validationResults.ContainsKey(localPropertyName);

            if (hasCurrentValidationResults)
            {
                validationResults[propertyName].Add(validationResult);
            }
            else
            {
                validationResults.Add(propertyName, new List<T> {validationResult});
            }
        }

        /// <summary>
        /// プロパティにエラーデータを設定します。
        /// </summary>
        /// <typeparam name="TProperty">プロパティを識別するインスタンスの型</typeparam>
        /// <param name="propertyExpression">プロパティを取得するための式木</param>
        /// <param name="validationResult">設定するエラーデータ</param>
        public void SetError<TProperty>(Expression<Func<TProperty>> propertyExpression, T validationResult)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            SetError(propertyName, validationResult);
        }

        #endregion
    }
}