using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlSerializerSample.Configrations
{
    /// <summary>
    /// メッセージデータの情報を定義します。
    /// </summary>
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class MessageData
    {
        #region Fields
        
        /// <summary>
        /// ID
        /// </summary>
        private string _id;

        /// <summary>
        /// 種別名
        /// </summary>
        private string _type;

        /// <summary>
        /// メッセージ
        /// </summary>
        private string _message;

        #endregion //Fields

        #region Properties

        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        /// <summary>
        /// 種別名
        /// </summary>
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        /// <summary>
        /// メッセージ
        /// </summary>
        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }

        #endregion //Properties
    }
}
