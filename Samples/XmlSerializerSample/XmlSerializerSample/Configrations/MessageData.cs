using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSerializerSample.Configrations
{
    /// <summary>
    /// メッセージデータの情報を定義します。
    /// </summary>
    public partial class MessageData
    {
        #region Properties

        /// <summary>
        /// メッセージ種別
        /// </summary>
        public MessageType MessageType
        {
            get
            {
                MessageType result;
                return Enum.TryParse<MessageType>(Type, out result) ? result : default(MessageType);
            }
        }

        #endregion //Properties
    }
}
