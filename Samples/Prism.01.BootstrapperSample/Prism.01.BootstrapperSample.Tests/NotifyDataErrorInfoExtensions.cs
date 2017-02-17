namespace Prism._01.BootstrapperSample.Tests
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using Prism.Mvvm;

    /// <summary>
    /// <see cref="INotifyDataErrorInfo"/> の拡張メソッドを定義します。
    /// </summary>
    public static class NotifyDataErrorInfoExtensions
    {
        public static bool HasErrorsProperty(this INotifyDataErrorInfo self, string propertyName)
        {
            return self.GetErrors(propertyName).Cast<string>().Any();
        }

        public static bool HasErrorsProperty<T, TProperty>(this T self, Expression<Func<T, TProperty>> propertyExpression)
            where T : INotifyDataErrorInfo
        {
            var member = propertyExpression.Body as MemberExpression;
            if (member == null) throw new InvalidOperationException();

            var propertyName = member.Member.Name;
            return self.HasErrorsProperty(propertyName);
        }
    }
}