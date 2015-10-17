using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilitySample
{
    /// <summary>
    /// コールバック実行インターフェース
    /// </summary>
    public interface ICallback<T>
    {
        /// <summary>
        /// コマンドのイベント番号
        /// </summary>
        int EventNo { get; }

        Func<byte[], T> RecivedCallback {get;}

    }
}
