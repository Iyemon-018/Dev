using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Info
{
    /// <summary>
    /// 文字数制限チェック属性情報を提供する。
    /// </summary>
    public class CheckStringLengthAttribute : CheckAttributeBase
    {
        #region プロパティ

        /// <summary>
        /// 最小文字数
        /// </summary>
        public int MinLength { get; private set; }

        /// <summary>
        /// 最大文字数
        /// </summary>
        public int MaxLength { get; private set; }

        #endregion //プロパティ

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CheckStringLengthAttribute(int minLength, int maxLength)
            : base(CheckAttributeType.StringLength)
        {
            this.MinLength = MinLength;
            this.MaxLength = MaxLength;
        }

        #endregion //初期化処理

        protected override string AttributeString()
        {
            return string.Format("[StringLengthCustom({0}, {1})]", MinLength, MaxLength);
        }
    }
}
