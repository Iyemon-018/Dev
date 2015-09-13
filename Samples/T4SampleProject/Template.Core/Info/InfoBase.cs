using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Info
{
    public abstract class InfoBase
    {
        #region プロパティ

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 形名
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// コメント
        /// </summary>
        public string Comment { get; private set; }

        #endregion //プロパティ

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeName"></param>
        /// <param name="comment"></param>
        protected InfoBase(string name, string typeName, string comment)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.Comment = comment;
        }

        #endregion //初期化処理
    }
}

