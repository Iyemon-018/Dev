namespace JenkinsNotification.Core.Extensions
{
    using System;
    using System.IO;
    using JenkinsNotification.Core.Properties;

    /// <summary>
    /// Xml デシリアライズ拡張メソッドクラスです。
    /// </summary>
    public static class XmlDeserializer
    {
        #region Methods

        /// <summary>
        /// 指定したファイルからデシリアライズします。
        /// </summary>
        /// <typeparam name="T">自分自身の型</typeparam>
        /// <param name="filePath">自分自身</param>
        /// <returns>デシリアライズオブジェクト</returns>
        /// <exception cref="System.IO.FileNotFoundException">指定したファイルが見つかりません。</exception>
        public static T Deserialize<T>(this string filePath)
            where T : class, new()
        {
            if (filePath.IsEmpty() || !File.Exists(filePath))
            {
                throw new FileNotFoundException(Resources.FileNotFoundMessage, filePath);
            }

            T result = null;

            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    result = (T) serializer.Deserialize(fs);
                }
            }
            catch (InvalidOperationException exception)
            {
                // ファイルのデシリアライズに失敗
                // TODO write log
                //throw;
            }

            return result;
        }

        #endregion
    }
}