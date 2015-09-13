using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4SampleProject
{
    public class Person
    {
        #region プロパティ
        
        /// <summary>ユーザーIDを設定、取得する。</summary>
        public int ID { get; set; }

        /// <summary>ユーザー名を設定、取得する。</summary>
        public string Name { get; set; }

        #endregion

        #region 初期化処理

        public Person()
        {
            ID = 0;
            Name = string.Empty;
        }

        #endregion
    }
}