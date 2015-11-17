using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlSerializerSample.Xml
{
    /// <summary>
    /// XML からデシリアライズするための拡張メソッドを定義します。
    /// </summary>
    public static class XmlDeserialize
    {
        /// <summary>
        /// XML ファイルをデシリアライズします。
        /// </summary>
        /// <typeparam name="T">オブジェクトの型</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>デシリアライズ結果</returns>
        public static T Deserialize<T>(this string self)
            where T : class
        {
            if (string.IsNullOrEmpty(self))
            {
                throw new ArgumentNullException("ファイルパスが設定されていません。");
            }

            if (!File.Exists(self))
            {
                throw new FileNotFoundException("ファイルが見つかりません。", self);
            }

            T result = null;

            using (var fs = new FileStream(self, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(T));
                result = serializer.Deserialize(fs) as T;
            }

            return result;
        }
    }
}
