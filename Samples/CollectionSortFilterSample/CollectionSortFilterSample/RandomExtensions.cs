using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionSortFilterSample
{
    /// <summary>
    /// ランダムな値を生成するための拡張メソッドを定義する。
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// 列挙体からランダムな値を取得する。
        /// </summary>
        /// <typeparam name="TEnum">列挙体</typeparam>
        /// <param name="self">自分自身</param>
        /// <param name="exceptValues">除外する値</param>
        /// <returns>結果値</returns>
        public static TEnum NextEnum<TEnum>(this Random self, params TEnum[] exceptValues)
            where TEnum : struct
        {
            var enumList = new List<TEnum>();
            foreach (TEnum e in Enum.GetValues(typeof(TEnum)))
            {
                if (exceptValues == null || !exceptValues.Contains(e))
                {
                    enumList.Add(e);   
                }
            }
            int idx = self.Next(0, enumList.Count);
            return enumList[idx];
        }

        /// <summary>
        /// 列挙体からランダムな値を取得する。<para/>
        /// 列挙体のすべての値が対象となる。
        /// </summary>
        /// <typeparam name="TEnum">列挙体</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>結果値</returns>
        public static TEnum NextEnum<TEnum>(this Random self)
            where TEnum : struct
        {
            return self.NextEnum<TEnum>(null);
        }

        /// <summary>
        /// 指定したコレクションをランダムに並び替える。
        /// </summary>
        /// <typeparam name="T">コレクションの型</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>並び替えた結果</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> self)
        {
            T[] result = self.ToArray();
            Random r = new Random();
            for (int i = result.Length - 1; i > 1; i--)
            {
                int rValue     = r.Next(0, i);
                T temp         = result[i];
                result[i]      = result[rValue];
                result[rValue] = temp;
            }

            return result;
        }
    }
}
