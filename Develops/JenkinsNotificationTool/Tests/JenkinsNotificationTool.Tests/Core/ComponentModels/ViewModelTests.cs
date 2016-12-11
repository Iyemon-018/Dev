namespace JenkinsNotificationTool.Tests.Core.ComponentModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

        /// <summary>
        /// <see cref="ViewModelBase.NotificationError{TProperty}"/> メソッドを試験します。
        /// </summary>
        /// <remarks>
        /// 以下の内容をテストします。
        /// ・以下の手順で検証エラーが発生すること。
        /// １．検証エラーとならない値を設定する。
        /// ２．検証を実施して
        /// </remarks>
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
        /// <see cref="ViewModelBase"/> オブジェクトプロパティの検証をテストします。
        /// </summary>
        /// <remarks>
        /// 以下の内容をテストします。
        /// ・ViewModelBase型プロパティを検証してエラーが発生した場合、その親クラスで検証エラーを取得できるかどうか。
        /// </remarks>
        [Fact]
        public void Tests_Validate_SubViewModel()
        {
            // arrange
            var target = new MockViewModel
                         {
                             FirstName = "Test",
                             LastName = "Mock",
                             DayOfWeek = DayOfWeek.Saturday,
                         };
            // この時点ではエラーはない。
            Assert.False(target.HasErrors);
            target.SubViewModel.Number = MockSubViewModel.NumberMinimum - 1;

            // act
            target.Validate();

            // assert
            // 子ViewModelのエラー結果を検証する。
            Assert.True(target.SubViewModel.HasErrors);
            // 親ViewModelが子ViewModelのエラー結果を保持する。
            Assert.True(target.HasErrors);

            ConsoleWriteErrorMessages(target.SubViewModel);
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
                        Output.WriteLine($"{viewModel.GetType().Name}.{property.Name} - {error}");
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

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public MockViewModel()
            {
                SubViewModel = new MockSubViewModel();
                SubViewModels = new ObservableCollection<MockSubViewModel>();

                AddChild(SubViewModel);
                AddChildren(SubViewModels);
            }

            #region Properties

            /// <summary>
            /// 年齢を設定、または取得します。
            /// </summary>
            [Display(Name = "年齢")]
            [Range(AgeMinimum, AgeMaximum, ErrorMessage = "{0}は{1}～{2}を入力してください。")]
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
            [MaxLength(LastNameMaxLength, ErrorMessage = "{0}は{1}文字以上入力できません。")]
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
            [MaxLength(FirstNameMaxLength, ErrorMessage = "{0}は{1}文字以上入力できません。")]
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

            /// <summary>
            /// ViewModel型のオブジェクト
            /// </summary>
            private MockSubViewModel _subViewModel;

            /// <summary>
            /// ViewModel型のオブジェクトを設定、または取得します。
            /// </summary>
            public MockSubViewModel SubViewModel
            {
                get { return _subViewModel; }
                set { SetProperty(ref _subViewModel, value); }
            }
            
            /// <summary>
            /// <see cref="ViewModelBase"/> 型のコレクションオブジェクトを取得します。
            /// </summary>
            public ObservableCollection<MockSubViewModel> SubViewModels { get; private set; }

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

                SubViewModel.Validate();
            }

            #endregion
        }

        private enum FromType
        {
            A,
            B,
        }

        private enum ToType
        {
            C,
            D,
        }

        private class MockSubViewModel : ViewModelBase
        {
            public const int NumberMinimum = 0;

            public const int NumberMaximum = 10;

            /// <summary>
            /// No
            /// </summary>
            private int _number;

            /// <summary>
            /// Noを設定、または取得します。
            /// </summary>
            [Display(Name = "No")]
            [Range(NumberMinimum, NumberMaximum, ErrorMessage = "{0}は{1}～{2}までの値を設定してください。")]
            public int Number
            {
                get { return _number; }
                set { SetProperty(ref _number, value); }
            }

            /// <summary>
            /// From
            /// </summary>
            private FromType _from;

            /// <summary>
            /// Fromを設定、または取得します。
            /// </summary>
            public FromType From
            {
                get { return _from; }
                set { SetProperty(ref _from, value); }
            }

            /// <summary>
            /// To
            /// </summary>
            private ToType _to;

            /// <summary>
            /// Toを設定、または取得します。
            /// </summary>
            public ToType To
            {
                get { return _to; }
                set { SetProperty(ref _to, value); }
            }

            /// <summary>
            /// このインスタンスの検証を実行します。<para />
            /// 属性による検証以外で必要な検証はここで行います。
            /// </summary>
            protected override void OnValidate()
            {
                base.OnValidate();

                if (From == FromType.A && To == ToType.D)
                {
                    NotificationError(() => From, $"{nameof(From)} {nameof(To)} の組み合わせが正しくありません。");
                    NotificationError(() => To, $"{nameof(From)} {nameof(To)} の組み合わせが正しくありません。");
                }
            }
        }

        #endregion
    }
}