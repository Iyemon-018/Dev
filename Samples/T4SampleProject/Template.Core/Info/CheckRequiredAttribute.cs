using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Info
{
    /// <summary>
    /// 必須入力チェック属性情報を提供する。
    /// </summary>
    public class CheckRequiredAttribute : CheckAttributeBase
    {
        #region 初期化処理

        public CheckRequiredAttribute()
            : base(CheckAttributeType.Required)
        {

        }

        #endregion //初期化処理

        protected override string AttributeString()
        {
            return "[RequiredCustom()]";
        }
    }
}
