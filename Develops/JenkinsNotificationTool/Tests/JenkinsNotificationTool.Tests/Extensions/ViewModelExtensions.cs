namespace JenkinsNotificationTool.Tests.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using JenkinsNotification.Core.ComponentModels;
    using Microsoft.Practices.Prism.Mvvm;

    /// <summary>
    /// <see cref="ViewModelBase"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class ViewModelExtensions
    {
        #region Methods

        /// <summary>
        /// 指定したプロパティがエラーかどうかを判定します。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <param name="propertyName">判定対象のプロパティ名</param>
        /// <returns>判定結果(true:エラーです。, false:正常です。)</returns>
        public static bool HasPropertyError(this INotifyDataErrorInfo self, string propertyName)
        {
            return self.GetErrors(propertyName).OfType<string>().Any();
        }

        /// <summary>
        /// 指定したプロパティがエラーかどうかを判定します。
        /// </summary>
        /// <typeparam name="TProperty">検証対象のプロパティの型</typeparam>
        /// <param name="self">自分自身</param>
        /// <param name="propertyExpression">プロパティ名を取得するための式木</param>
        /// <returns>判定結果(true:エラーです。, false:正常です。)</returns>
        public static bool HasPropertyError<TProperty>(this INotifyDataErrorInfo self, Expression<Func<TProperty>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            return self.HasPropertyError(propertyName);
        }

        #endregion
    }
}