using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SelializeSample
{
    /// <summary>
    /// 深いコピー用の拡張メソッドです。
    /// </summary>
    public static class DeepCopyExtensions
    {
        /// <summary>
        /// オブジェクトをコピーします。
        /// </summary>
        /// <typeparam name="T">コピーする型</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>コピーしたオブジェクトを異なるインスタンスで取得します。</returns>
        public static T DeepCopy<T>(this T self)
        {
            T result;

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, self);
                ms.Position = 0;
                result = (T)formatter.Deserialize(ms);
            }

            return result;
        }
    }
}
