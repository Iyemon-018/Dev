using SelializeSample.ComponentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelializeSample.ViewModels
{
    /// <summary>
    /// XmlSerializerView用のViewModelクラスです。
    /// </summary>
    [Serializable]
    public class XmlSerializerViewModel : ViewModelBase
    {
        /// <summary>
        /// オブジェクトの状態を保存するためのスナップショットデータです。
        /// </summary>
        private string _snapshot;
            
        private int _age;

        private string _name;

        private DateTime _birthday;

        private BloodType _bloodType;

        private Gender _gender;

        public XmlSerializerViewModel()
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
        /// スナップショットデータを保存します。
        /// </summary>
        public void SaveSnapshotData()
        {
            using (new TimeTracer("オブジェクトのコピー"))
            {
                _snapshot = this.Serialize();
            }
        }

        /// <summary>
        /// オブジェクトが更新されたかどうかを判定します。
        /// </summary>
        /// <returns>
        /// オブジェクトが更新されている場合は、true を
        /// 更新されていない場合は、false を返します。
        /// </returns>
        public bool IsUpdate()
        {
            using (new TimeTracer("更新チェック"))
            {
                if (string.IsNullOrEmpty(_snapshot))
                {
                    return true;
                }

                var nowData = this.Serialize();
                return !_snapshot.Equals(nowData);
            }
        }

        /// <summary>
        /// オブジェクトの状態を元に戻します。
        /// </summary>
        public void Reset()
        {
            using (new TimeTracer("オブジェクトをリセット"))
            {
                if (!string.IsNullOrEmpty(_snapshot))
                {
                    UpdateObject(_snapshot.Deserialize<XmlSerializerViewModel>());
                }
            }
        }

        /// <summary>
        /// オブジェクトを更新します。
        /// </summary>
        /// <param name="obj">オブジェクトデータ</param>
        private void UpdateObject(XmlSerializerViewModel obj)
        {
            Age = obj.Age;
            Name = obj.Name;
            Birthday = obj.Birthday;
            BloodType = obj.BloodType;
            Gender = obj.Gender;
        }
    }
}
