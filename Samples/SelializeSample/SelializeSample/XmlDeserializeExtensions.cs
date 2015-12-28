using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SelializeSample
{
    /// <summary>
    /// Xmlからデシリアライズするための拡張メソッドを定義します。
    /// </summary>
    public static class XmlDeserializeExtensions
    {
        /// <summary>
        /// Xml文字列からオブジェクトへデシリアライズします。
        /// </summary>
        /// <typeparam name="T">自分自身の型</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>デシリアライズ結果</returns>
        public static T Deserialize<T>(this string self)
        {
            if (string.IsNullOrEmpty(self))
            {
                throw new ArgumentNullException("指定した文字列が空です。\r\nデシリアライズできません。");
            }

            T result;

            using (var sr = new StringReader(self))
            {
                var serializer = new XmlSerializer(typeof(T));
                result = (T) serializer.Deserialize(sr);
            }

            return result;
        }
    }
}
