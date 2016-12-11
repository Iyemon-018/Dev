namespace JenkinsNotification.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// <see cref="T:Exception"/> の拡張メソッドを定義します。
    /// </summary>
    public static class ExceptionExtensions
    {
        #region Methods

        /// <summary>
        /// 例外スタックトレースからメッセージ コレクションを取得します。<para/>
        /// メッセージは内部例外のスタックトレースも含まれます。
        /// </summary>
        /// <param name="self">自分自身</param>
        /// <returns>メッセージコレクション</returns>
        public static IEnumerable<string> ToStackTraceMessages(this Exception self)
        {
            if (self == null) return Enumerable.Empty<string>();

            var result = new List<string> {self.Message};
            result.AddRange(self.StackTrace.Split('\n'));
            if (self.InnerException != null)
            {
                var innerStackTraceMessages = self.InnerException.ToStackTraceMessages();
                result.Add(self.InnerException.Message);
                result.AddRange(innerStackTraceMessages);
            }
            return result;
        }

        #endregion
    }
}