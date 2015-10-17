using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilitySample
{
    public abstract class RecivedCommandBase<T> : ICallback<T>
    {
        public int EventNo { get; private set; }

        public RecivedCommandBase(int eventNo, Func<T, byte[]> recievedCallback)
        {
            this.EventNo = eventNo;
        }

        public T Recived(byte[] buffer)
        {
            Console.WriteLine(string.Format("> Command : {0} が処理した。", EventNo));
            return RecivedCallback(buffer);
        }

        Func<byte[], T> RecivedCallback
        {
            get;
            private set;
        }
    }

    public class GeneralRecivedCommand<T> : RecivedCommandBase<T>
    {
        public GeneralRecivedCommand(int eventNo, Func<T, byte[]> recievedCallback)
            : base(eventNo, recievedCallback)
        {

        }
    }

}
