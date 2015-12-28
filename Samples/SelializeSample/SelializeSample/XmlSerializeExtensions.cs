using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SelializeSample
{
    /// <summary>
    /// Xmlへシリアライズするための拡張メソッドを定義します。
    /// </summary>
    public static class XmlSerializeExtensions
    {
        /// <summary>
        /// Xml文字列へへシリアライズします。
        /// </summary>
        /// <typeparam name="T">自分自身の型</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>シリアライズ文字列</returns>
        public static string Serialize<T>(this T self)
        {
            string result;
            using (var sw = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(sw, self);
                result = sw.ToString();
            }
            return result;
        }
    }
}
