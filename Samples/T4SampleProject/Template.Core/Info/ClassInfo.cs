using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Info
{
    /// <summary>
    /// クラスの情報を定義する。
    /// </summary>
    public class ClassInfo : InfoBase
    {
        #region プロパティ

        /// <summary>
        /// 追加の名前空間
        /// </summary>
        public List<string> NameSpaces { get; private set; }

        /// <summary>
        /// 名前空間を追加するかどうか
        /// </summary>
        public bool HasNameSpaces { get { return NameSpaces.Count > 0; } }

        /// <summary>
        /// 定数リスト
        /// </summary>
        public List<DefinedInfo> DefinedList { get; private set; }

        /// <summary>
        /// 定数があるかどうか。
        /// </summary>
        public bool HasDefined { get { return DefinedList.Count > 0; } }

        /// <summary>
        /// プロパティ情報リスト
        /// </summary>
        public List<PropertyInfo> Properties { get; private set; }

        #endregion //プロパティ

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="name"></param>
        /// <param name="comment"></param>
        public ClassInfo(string name, string comment)
            : base(name, name, comment)
        {
            this.NameSpaces = new List<string>();
            this.DefinedList = new List<DefinedInfo>();
            this.Properties = new List<PropertyInfo>();
        }
    }
}
