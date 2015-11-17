using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XmlSerializerSample.Xml;

namespace XmlSerializerSample.Configrations
{
    /// <summary>
    /// メッセージ設定ファイルの情報を定義します。
    /// </summary>
    public sealed partial class MessageConfigration
    {
        #region Static Fields
        
        /// <summary>
        /// 規定のファイル名
        /// </summary>
        public static readonly string FileName = "Message-Config.xml";

        /// <summary>
        /// 規定のファイルパス
        /// </summary>
        public static readonly string FilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), FileName);

        /// <summary>
        /// カレントデータ
        /// </summary>
        private static MessageConfigration _current = null;

        #endregion //Static Fields

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MessageConfigration()
        {

        }

        #endregion //Ctor

        #region Static Properties

        /// <summary>
        /// カレントデータを取得します。
        /// </summary>
        public static MessageConfigration Current
        {
            get { return _current; }
            private set { _current = value; }
        }

        #endregion //Static Properties

        #region Static Methods
        
        /// <summary>
        /// ファイルからデータを読み込みます。
        /// </summary>
        /// <param name="filePath">ファイルパス（空文字の場合、規定のファイルパスから読み込みます。）</param>
        public static void Load(string filePath = "")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = FilePath;
            }
            Current = filePath.Deserialize<MessageConfigration>();
        }

        /// <summary>
        /// カレントデータをファイルへ保存します。
        /// </summary>
        /// <param name="filePath">ファイルパス（空文字の場合、規定のファイルパスから読み込みます。）</param>
        public static void Save(string filePath = "")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = FilePath;
            }
            Current.Serialize<MessageConfigration>(filePath);
        }

        #endregion //Static Methods
    }
}
