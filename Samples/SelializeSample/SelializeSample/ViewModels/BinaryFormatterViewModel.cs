using SelializeSample.ComponentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelializeSample.ViewModels
{
    /// <summary>
    /// BinaryForamtterView用のViewModelクラスです。
    /// </summary>
    [Serializable]
    public class BinaryFormatterViewModel : ViewModelBase
    {
        private int _age;

        private string _name;

        private DateTime _birthday;

        private BloodType _bloodType;

        private Gender _gender;

        public BinaryFormatterViewModel()
        {
            Age = 20;
            Name = string.Empty;
            Birthday = new DateTime(1990, 1, 1);
            BloodType = SelializeSample.BloodType.TypeA;
            Gender = SelializeSample.Gender.Male;
        }

        /// <summary>年齢</summary>
        public int Age
        {
            get { return _age; }
            set { SetProperty<int>(ref _age, value); }
        }

        /// <summary>名前</summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty<string>(ref _name, value); }
        }

        /// <summary>誕生日</summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set { SetProperty<DateTime>(ref _birthday, value); }
        }

        /// <summary>血液型</summary>
        public BloodType BloodType
        {
            get { return _bloodType; }
            set { SetProperty<BloodType>(ref _bloodType, value); }
        }

        /// <summary>性別</summary>
        public Gender Gender
        {
            get { return _gender; }
            set { SetProperty<Gender>(ref _gender, value); }
        }

        /// <summary>
        /// オブジェクトを更新します。
        /// </summary>
        /// <param name="obj">オブジェクトデータ</param>
        protected override void UpdateObject(ViewModelBase obj)
        {
            base.UpdateObject(obj);

            var vm = obj as BinaryFormatterViewModel;
            if (vm != null)
            {
                Age       = vm.Age;
                Name      = vm.Name;
                Birthday  = vm.Birthday;
                BloodType = vm.BloodType;
                Gender    = vm.Gender;
            }
        }

        /// <summary>
        /// オブジェクト同士を比較します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト</param>
        /// <returns>true:一致, false:不一致</returns>
        public override bool Equals(object obj)
        {
            var other = obj as BinaryFormatterViewModel;
            if (other == null)
            {
                return false;
            }
            return Age == other.Age
                 & Name.Equals(other.Name)
                 & Birthday == other.Birthday
                 & BloodType == other.BloodType
                 & Gender == other.Gender;
        }
    }
}
