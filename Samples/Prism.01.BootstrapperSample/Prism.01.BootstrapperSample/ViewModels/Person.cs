namespace Prism._01.BootstrapperSample.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 性別種別を定義します。
    /// </summary>
    public enum GenderType
    {
        Male,
        Female
    }

    /// <summary>
    /// ユーザー情報クラスです。
    /// </summary>
    /// <seealso cref="Prism._01.BootstrapperSample.ViewModelBase" />
    public class Person : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// 住所
        /// </summary>
        private string _address;

        /// <summary>
        /// 年齢
        /// </summary>
        private int _age;

        /// <summary>
        /// 性別
        /// </summary>
        private GenderType _gender;

        /// <summary>
        /// メールアドレス
        /// </summary>
        private string _mailAddress;

        /// <summary>
        /// 氏名
        /// </summary>
        private string _name;

        #endregion

        #region Properties

        /// <summary>
        /// 性別を設定、または取得します。
        /// </summary>
        public GenderType Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        /// <summary>
        /// 氏名を設定、または取得します。
        /// </summary>
        [Display(Name = "氏名")]
        [Required(ErrorMessage = "{0}は必ず入力してください。")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// 年齢を設定、または取得します。
        /// </summary>
        [Display(Name = "年齢")]
        [Range(0, 120, ErrorMessage = "{0}は、{1}～{2}まで入力することができます。")]
        public int Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        /// <summary>
        /// 住所を設定、または取得します。
        /// </summary>
        [Display(Name = "住所")]
        [Required(ErrorMessage = "{0}は必ず入力してください。")]
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        /// <summary>
        /// メールアドレスを設定、または取得します。
        /// </summary>
        [Display(Name = "メールアドレス")]
        [Required(ErrorMessage = "{0}は必ず入力してください。")]
        public string MailAddress
        {
            get { return _mailAddress; }
            set { SetProperty(ref _mailAddress, value); }
        }

        #endregion
    }
}