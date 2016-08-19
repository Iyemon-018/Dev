namespace Mc.Common.Tests.Wpf.Core.ComponentModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Mc.Common.Wpf.Core.ComponentModels;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// <see cref="BindableBase" /> のテストクラスです。
    /// </summary>
    [TestClass]
    public class BindableBaseTests
    {
        #region Inner Class

        public class DummyBindable : BindableBase
        {
            /// <summary>
            /// 数値型の値を設定、取得します。
            /// </summary>
            private int _value;

            /// <summary>
            /// 数値型の値を設定、取得します。
            /// </summary>
            public int Value
            {
                get { return _value; }
                set { SetProperty(ref _value, value); }
            }
            /// <summary>
            /// 文字列型の値を設定、取得します。
            /// </summary>
            private string _text;

            /// <summary>
            /// 文字列型の値を設定、取得します。
            /// </summary>
            public string Text
            {
                get { return _text; }
                set { SetProperty(ref _text, value); }
            }
            /// <summary>
            /// 日時型の値を設定、取得します。
            /// </summary>
            private DateTime _dateTime;

            /// <summary>
            /// 日時型の値を設定、取得します。
            /// </summary>
            public DateTime DateTime
            {
                get { return _dateTime; }
                set { SetProperty(ref _dateTime, value); }
            }

            /// <summary>
            /// 列挙体の値を設定、取得します。
            /// </summary>
            private DayOfWeek _dayOfWeek;

            /// <summary>
            /// 列挙体の値を設定、取得します。
            /// </summary>
            public DayOfWeek DayOfWeek
            {
                get { return _dayOfWeek; }
                set { SetProperty(ref _dayOfWeek, value); }
            }
        }

        #endregion

        /// <summary>
        /// <see cref="BindableBase.DeferPropertyChanged" /> をテストします。(成功パターン)
        /// </summary>
        /// <remarks>
        /// 以下の内容をテストします。
        /// ・<see cref="BindableBase.DeferPropertyChanged"/> のスコープを出るまでに<see cref="BindableBase.PropertyChanged"/> が呼ばれないこと。
        /// ・スコープを超えたあとに、プロパティを変更した順番に<see cref="BindableBase.PropertyChanged"/> が実行されること。
        /// </remarks>
        [TestMethod]
        public void Test_Success_DeferPropertyChanged_順序通りに実行できること()
        {
            // arrange
            var b        = new DummyBindable();                     // テスト対象のオブジェクト
            bool isScope = false;                                   // スコープを通過したかどうか
            int index    = 0;                                       // プロパティの変更順序インデックス
            var result   = new Dictionary<int, string>();           // プロパティの実行順序の連想配列
            b.PropertyChanged += (sender, e) =>
                                 {
                                     Assert.IsTrue(isScope);
                                     result[index] = e.PropertyName;
                                     index++;
                                 };

            // act
            using (b.DeferPropertyChanged())
            {
                isScope = true;

                b.DateTime  = DateTime.Now;
                b.DayOfWeek = DayOfWeek.Thursday;
                b.Value     = 123;
                b.Text      = "Test";

                isScope = false;
            }

            // assert
            // 順番にチェックするためにインデックスを初期化する。
            index = 0;
            Assert.IsTrue(result[index++] == "DateTime");
            Assert.IsTrue(result[index++] == "DayOfWeek");
            Assert.IsTrue(result[index++] == "Value");
            Assert.IsTrue(result[index++] == "Text");
            Debug.Print(string.Join(Environment.NewLine, result.Select(x => $"{x.Key}, プロパティ名:{x.Value}")));
        }
    }
}