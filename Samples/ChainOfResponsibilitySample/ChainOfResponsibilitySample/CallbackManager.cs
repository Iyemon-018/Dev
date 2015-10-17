using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilitySample
{
    /// <summary>
    /// 受信コールバック管理クラス
    /// </summary>
    public class CallbackManager
    {
        /// <summary>
        /// 実行対象のコールバックリスト
        /// </summary>
        protected List<ICallback> Callbacks { get; set; }

        /// <summary>
        /// 登録済みのコールバック数
        /// </summary>
        public int Count { get { return Callbacks.Count; } }

        /// <summary>
        /// 指定したインデックスのコールバックにアクセスする。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>コールバック</returns>
        /// <exception cref="System.OutOfRangeException">index が登録数の範囲外だった場合に発行される。</exception>
        public ICallback this[int index]
        {
            get { return Callbacks[index]; }
            set { Callbacks[index] = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CallbackManager()
        {
            Callbacks = new List<ICallback>();
        }

        /// <summary>
        /// コールバックを追加する。
        /// </summary>
        /// <param name="callback"></param>
        public void AddCallback(ICallback callback)
        {
            Callbacks.Add(callback);
        }

        public bool ExecuteCallback(int eventNo, object header, byte[] buffer)
        {
            for (int i = 0; i < Count; i++)
            {
                var cb = this[i];
                if (cb.EventNo == eventNo)
                {
                    cb.Recived(header, buffer);
                    return true;
                }
            }

            return false;
        }
    }
}
