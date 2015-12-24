using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelializeSample
{
    /// <summary>
    /// ある処理にかかった時間を計測します。
    /// </summary>
    public class TimeTracer : IDisposable
    {
        /// <summary>
        /// オブジェクトが解放されたかどうか。
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// 出力するメッセージ
        /// </summary>
        private string _message;

        /// <summary>
        /// 時間計測用ストップウォッチ
        /// </summary>
        private Stopwatch _watch;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message">メッセージ</param>
        public TimeTracer(string message)
        {
            _message = message;
            _watch = new Stopwatch();
            _watch.Start();
        }

        ~TimeTracer()
        {
            Dispose(false);
        }

        /// <summary>
        /// 計測を停止します。
        /// </summary>
        public void Stop()
        {
            if (_watch != null)
            {
                _watch.Stop();
                Console.WriteLine("[TimeTracer] {0} : {1}", _message, _watch.Elapsed);
            }
        }

        /// <summary>
        /// オブジェクトを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// オブジェクトを解放します。
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            Stop();

            if (disposing)
            {
                
            }

            _disposed = true;
        }
    }
}
