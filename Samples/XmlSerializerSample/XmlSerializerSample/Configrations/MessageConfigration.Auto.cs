using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlSerializerSample.Configrations
{
    /// <summary>
    /// メッセージ設定ファイルの情報を定義します。
    /// </summary>
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class MessageConfigration
    {
        #region Fields
        
        /// <summary>
        /// メッセージデータリスト
        /// </summary>
        private MessageData[] messageDatasField;

        #endregion //Fields

        #region Properties
        
        /// <summary>
        /// メッセージデータリスト
        /// </summary>
        [XmlArrayItemAttribute("MessageData", IsNullable = false)]
        public MessageData[] MessageDatas
        {
            get
            {
                return this.messageDatasField;
            }
            set
            {
                this.messageDatasField = value;
            }
        }

        #endregion //Properties
    }
}
