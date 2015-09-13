using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Info
{
    public class CheckRangeAttribute : CheckAttributeBase
    {
        #region プロパティ

        /// <summary>
        /// 最小値
        /// </summary>
        public string Minimum { get; private set; }

        /// <summary>
        /// 最大値
        /// </summary>
        public string Maximum { get; private set; }

        /// <summary>
        /// 型名
        /// </summary>
        public string TypeName { get; private set; }

        #endregion //プロパティ

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public CheckRangeAttribute(string typeName, string minimum, string maximum)
            : base(CheckAttributeType.Range)
        {
            this.TypeName = typeName;
            this.Minimum = minimum;
            this.Maximum = maximum;
        }

        #endregion //初期化処理

        protected override string AttributeString()
        {
            return string.Format("[RangeCustom(typeof({0}), \"{1}\", \"{2}\")]", TypeName, Minimum, Maximum);
        }
    }
}
