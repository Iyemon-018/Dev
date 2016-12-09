namespace JenkinsNotificationTool.Tests.Core.ComponentModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using JenkinsNotification.Core.ComponentModels;
    using JenkinsNotificationTool.Tests.Extensions;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// <see cref="ViewModelBase"/> クラスのユニットテストクラスです。
    /// TODO ViewModel型のプロパティの検証をテストする。
    /// TODO ObservableCollection<ViewModel>型のプロパティの検証をテストする。
    /// </summary>
    public class ViewModelTests : TestBase
    {
        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="output">テスト出力ヘルパー</param>
        public ViewModelTests(ITestOutputHelper output) : base(output)
        {
        }

        #endregion

        #region Methods

        [Fact]
        public void Tests_NotificationError()
        {
            // arrange
            var target = new MockViewModel
                         {
                             FirstName = "John",
                             LastName = "Smith"
                         };

            // act
            target.DayOfWeek = DayOfWeek.Monday;
            // この時点ではエラーでないこと。
            Assert.False(target.HasErrors);

            // ここで検証してエラーが通知されること。
            target.Validate();

            // assert
            Assert.True(target.HasPropertyError(() => target.DayOfWeek));
            ConsoleWriteErrorMessages(target);
        }

        /// <summary>
        /// int型プロパティのテストを行います。
        /// </summary>
        /// <remarks>
        /// 以下の内容をテストします。
        /// ・しきい値を超えた値を設定した場合、int型のプロパティがエラーになること。
        /// </remarks>
        [Fact]
        public void Tests_ValidateProperty_int()
        {
            // arrange
            var target = new MockViewModel {Age = MockViewModel.AgeMinimum};
            Assert.False(target.HasPropertyError(() => target.Age));
            
            // act
            target.Age -= 1;

            // assert
            Assert.True(target.HasPropertyError(() => target.Age));
            ConsoleWriteErrorMessages(target);

            // reset
            target.Age = MockViewModel.AgeMaximum;
            Assert.False(target.HasPropertyError(() => target.Age));

            // act
            target.Age = MockViewModel.AgeMaximum + 1;

            // assert
            Assert.True(target.HasPropertyError(() => target.Age));
            ConsoleWriteErrorMessages(target);
        }

        /// <summary>
        /// string型プロパティのテストを行います。
        /// </summary>
        /// <remarks>
        /// 以下の内容をテストします。
        /// ・文字数制限を超える文字列に更新するとプロパティがエラーとなること。
        /// ・空文字に更新するとプロパティがエラーとなること。
        /// </remarks>
        [Fact]
        public void Tests_ValidateProperty_string()
        {
            // arrange
            var target = new MockViewModel();

            // この時点でエラーが発生しないこと。
            // 空文字の状態でエラーになると困るので。
            // act
            // assert
            Assert.False(target.HasPropertyError(() => target.LastName));

            // 文字数制限に引っかかること。
            // act
            target.LastName = Enumerable.Repeat('A', MockViewModel.LastNameMaxLength + 1).ToString();

            // assert
            Assert.True(target.HasPropertyError(() => target.LastName));
            ConsoleWriteErrorMessages(target);

            // 一旦エラーをクリアする。
            // act
            // assert
            target.LastName = "clear";
            Assert.False(target.HasPropertyError(() => target.LastName));

            // 空文字でエラーが発生すること。
            // act
            target.LastName = string.Empty;

            // assert
            Assert.True(target.HasPropertyError(() => target.LastName));
            ConsoleWriteErrorMessages(target);
        }

        /// <summary>
        /// 指定したViewModelオブジェクトの持つエラー情報をすべてコンソールに出力します。
        /// </summary>
        /// <param name="viewModel">コンソールに出力するViewModelオブジェクト</param>
        private void ConsoleWriteErrorMessages(INotifyDataErrorInfo viewModel)
        {
            if ((viewModel != null) && viewModel.HasErrors)
            {
                var properties = viewModel.GetType().GetProperties();
                foreach (var property in properties)
                {
                    foreach (var error in viewModel.GetErrors(property.Name))
                    {
                        Output.WriteLine(error.ToString());
                    }
                }
            }
        }

        #endregion

        #region Nested Classes

        /// <summary>
        /// テスト用モックViewModelクラスです。
        /// </summary>
        /// <seealso cref="JenkinsNotification.Core.ComponentModels.ViewModelBase" />
        private class MockViewModel : ViewModelBase
        {
            #region Const

            /// <summary>
            /// <see cref="Age"/> の入力可能な最小値
            /// </summary>
            public const int AgeMinimum = 0;

            /// <summary>
            /// <see cref="Age"/> の入力可能な最大値
            /// </summary>
            public const int AgeMaximum = 100;

            /// <summary>
            /// <see cref="LastName"/> に入力可能な最大文字数
            /// </summary>
            public const int LastNameMaxLength = 30;

            /// <summary>
            /// <see cref="FirstName"/> に入力可能な最大文字数
            /// </summary>
            public const int FirstNameMaxLength = 30;

            #endregion

            #region Fields

            /// <summary>
            /// 年齢
            /// </summary>
            private int _age;

            /// <summary>
            /// 曜日
            /// </summary>
            private DayOfWeek _dayOfWeek;

            /// <summary>
            /// 名前
            /// </summary>
            private string _firstName;

            /// <summary>
            /// 名字
            /// </summary>
            private string _lastName;

            #endregion

            #region Properties

            /// <summary>
            /// 年齢を設定、または取得します。
            /// </summary>
            [Display(Name = "年齢")]
            [Range(AgeMinimum, AgeMaximum, ErrorMessage = "{2}は{0}～{1}を入力してください。")]
            public int Age
            {
                get { return _age; }
                set { SetProperty(ref _age, value); }
            }

            /// <summary>
            /// 名字を設定、または取得します。
            /// </summary>
            [Display(Name = "名字")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0}は必ず入力してください。")]
            [MaxLength(LastNameMaxLength, ErrorMessage = "{1}は{0}文字以上入力できません。")]
            public string LastName
            {
                get { return _lastName; }
                set { SetProperty(ref _lastName, value); }
            }

            /// <summary>
            /// 名前を設定、または取得します。
            /// </summary>
            [Display(Name = "名前")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0}は必ず入力してください。")]
            [MaxLength(FirstNameMaxLength, ErrorMessage = "{1}は{0}文字以上入力できません。")]
            public string FirstName
            {
                get { return _firstName; }
                set { SetProperty(ref _firstName, value); }
            }

            /// <summary>
            /// 曜日を設定、または取得します。
            /// </summary>
            public DayOfWeek DayOfWeek
            {
                get { return _dayOfWeek; }
                set { SetProperty(ref _dayOfWeek, value); }
            }

            #endregion

            #region Methods

            /// <summary>
            /// このインスタンスの検証を実行します。<para />
            /// 属性による検証以外で必要な検証はここで行います。
            /// </summary>
            protected override void OnValidate()
            {
                base.OnValidate();

                if (DayOfWeek == DayOfWeek.Monday)
                {
                    NotificationError(() => DayOfWeek, $"曜日には{DayOfWeek.Monday}は設定できません。");
                }
            }

            #endregion
        }

        #endregion
    }
}