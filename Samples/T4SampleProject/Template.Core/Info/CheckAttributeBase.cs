using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Core.Info
{
    /// <summary>
    /// 入力チェック種別
    /// </summary>
    public enum CheckAttributeType
    {
        /// <summary>
        /// 必須入力チェック
        /// </summary>
        Required,

        /// <summary>
        /// 範囲入力チェック
        /// </summary>
        Range,

        /// <summary>
        /// 文字数制限チェック
        /// </summary>
        StringLength,
    }

    /// <summary>
    /// 入力チェック属性情報を定義する。
    /// </summary>
    public abstract class CheckAttributeBase
    {
        #region プロパティ

        /// <summary>
        /// 入力チェック種別
        /// </summary>
        public CheckAttributeType CheckType { get; private set; }

        #endregion //プロパティ

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        protected CheckAttributeBase(CheckAttributeType type)
        {
            this.CheckType = type;
        }

        #endregion //初期化処理

        #region チェック属性テキスト取得関連

        /// <summary>
        /// チェック属性テキストを取得する。
        /// </summary>
        /// <returns></returns>
        public string AttributeText()
        {
            return AttributeString();
        }

        protected abstract string AttributeString();

        #endregion //チェック属性テキスト取得関連
    }
}
