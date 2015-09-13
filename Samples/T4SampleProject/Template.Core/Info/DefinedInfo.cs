using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Info
{
    /// <summary>
    /// 定数情報を提供する。
    /// </summary>
    public class DefinedInfo : InfoBase
    {
        #region プロパティ

        /// <summary>
        /// 値
        /// </summary>
        public string Value { get; private set; }

        #endregion //プロパティ

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeName"></param>
        /// <param name="comment"></param>
        /// <param name="value"></param>
        public DefinedInfo(string name, string typeName, string comment, string value)
            : base(name, typeName, comment)
        {
            this.Value = value;
        }

        #endregion //初期化処理
    }
}
