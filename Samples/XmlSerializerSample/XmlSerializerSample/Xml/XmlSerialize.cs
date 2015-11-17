using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace XmlSerializerSample.Xml
{
    /// <summary>
    /// XML をシリアライズするための拡張メソッドを定義します。
    /// </summary>
    public static class XmlSerialize
    {
        /// <summary>
        /// オブジェクトをXML ファイルにシリアライズします。
        /// </summary>
        /// <typeparam name="T">シリアライズする型</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>シリアライズ結果</returns>
        public static void Serialize<T>(this T self, string filePath)
            where T : class
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("指定したファイルが見つかりません。", filePath);
            }

            string directoryName = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryName);

            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fs, self);
            }
        }
    }
}
