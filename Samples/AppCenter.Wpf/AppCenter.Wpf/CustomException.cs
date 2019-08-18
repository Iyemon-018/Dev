namespace AppCenter.Wpf
{
    using System;
    using System.Runtime.Serialization;

    public sealed class CustomException : Exception
    {
        public bool ShouldReportProcessed { get; }

        /// <summary>
        ///   <see cref="T:System.Exception" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public CustomException(bool shouldReportProcessed)
        {
            ShouldReportProcessed = shouldReportProcessed;
        }

        /// <summary>
        ///   指定したエラー メッセージを使用して、<see cref="T:System.Exception" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">エラーを説明するメッセージ。</param>
        public CustomException(string message, bool shouldReportProcessed) : base(message)
        {
            ShouldReportProcessed = shouldReportProcessed;
        }

        /// <summary>
        ///   指定したエラー メッセージおよびこの例外の原因となった内部例外への参照を使用して、<see cref="T:System.Exception" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">例外の原因を説明するエラー メッセージ。</param>
        /// <param name="innerException">
        ///   現在の例外の原因である例外。内部例外が指定されていない場合は null 参照 (Visual Basic では、<see langword="Nothing" />)。
        /// </param>
        public CustomException(string message, Exception innerException, bool shouldReportProcessed) : base(message, innerException)
        {
            ShouldReportProcessed = shouldReportProcessed;
        }

        /// <summary>
        ///   シリアル化したデータを使用して、<see cref="T:System.Exception" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="info">
        ///   スローされている例外に関するシリアル化済みオブジェクト データを保持している <see cref="T:System.Runtime.Serialization.SerializationInfo" />。
        /// </param>
        /// <param name="context">
        ///   転送元または転送先についてのコンテキスト情報を含む <see cref="T:System.Runtime.Serialization.StreamingContext" /> です。
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="info" /> パラメーターが <see langword="null" /> です。
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///   クラス名が <see langword="null" /> であるか、<see cref="P:System.Exception.HResult" /> が 0 です。
        /// </exception>
        public CustomException(SerializationInfo info, StreamingContext context, bool shouldReportProcessed) : base(info, context)
        {
            ShouldReportProcessed = shouldReportProcessed;
        }
    }
}