using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Info
{
    /// <summary>
    /// プロパティ情報を定義する。
    /// </summary>
    public class PropertyInfo : InfoBase
    {
        #region プロパティ

        /// <summary>
        /// フィールド名を取得する。
        /// </summary>
        public string FieldName
        {
            get
            {
                return string.IsNullOrEmpty(Name) == true ? string.Empty
                    : "_" + Name.Substring(0, 1).ToLower() + Name.Substring(1);
            }
        }

        /// <summary>
        /// デフォルト値
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// デフォルト値を設定するかどうか
        /// </summary>
        public bool HasDefaultValue
        {
            get { return !string.IsNullOrEmpty(DefaultValue); }
        }

        /// <summary>
        /// 項目名称
        /// </summary>
        public string ItemName { get; private set; }

        /// <summary>
        /// 備考コメント
        /// </summary>
        public string Remarks { get; private set; }

        /// <summary>
        /// 備考コメントが有るかどうか
        /// </summary>
        public bool HasRemarks { get { return !string.IsNullOrEmpty(Remarks); } }

        /// <summary>
        /// 入力チェック属性リスト
        /// </summary>
        public List<CheckAttributeBase> CheckAttributes { get; private set; }

        #endregion //プロパティ

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeName"></param>
        /// <param name="comment"></param>
        /// <param name="itemName"></param>
        public PropertyInfo(string name, string typeName, string comment, string itemName, string remarks)
            : base(name, typeName, comment)
        {
            this.DefaultValue = string.Empty;
            this.ItemName = itemName;
            this.Remarks = remarks;
            CheckAttributes = new List<CheckAttributeBase>();
        }

        #endregion //初期化処理
    }
}
