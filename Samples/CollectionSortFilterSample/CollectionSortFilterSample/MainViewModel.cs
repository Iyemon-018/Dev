using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CollectionSortFilterSample
{
    public class MainViewModel : ViewModelBase
    {
        #region Cont

        /// <summary>
        /// 最大スタッフ数
        /// </summary>
        const int MaxStaffCount = 10;

        /// <summary>
        /// スタッフ名リスト
        /// </summary>
        readonly string[] NameList = new string[MaxStaffCount] { "高本　貴哉"
                                                              , "玉　真里菜"
                                                              , "鴨居　悠哉"
                                                              , "竹中　加菜"
                                                              , "稲木　智宏"
                                                              , "崎谷　あい"
                                                              , "奥本　雄祐"
                                                              , "五十嶋　沙和"
                                                              , "岩室　虎大"
                                                              , "真田　向日葵"
                                                              ,};


        #endregion //Cont

        #region Fields

        /// <summary>選択されたスタッフ情報</summary>
        private Staff _selectedStaff;

        /// <summary>選択されたスタッフID</summary>
        private int _selectedId;

        /// <summary>選択されたスタッフ名</summary>
        private string _selectedStaffName;

        /// <summary>選択されたスタッフの役割</summary>
        private Role _selectedStaffRole;

        #endregion //Fields

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainViewModel()
            : base()
        {
            // データを作成する。
            var r = new Random();

            StaffList = new ObservableCollection<Staff>();

            for (int i = 0; i < MaxStaffCount; i++)
            {
                // ランダムな値をそれぞれ設定する。
                var staff = new Staff()
                {
                    Id   = i,
                    Age  = r.Next(20, 50),
                    Name = NameList[i],
                    Role = r.NextEnum<Role>(Role.All),
                };

                StaffList.Add(staff);
            }

            // 初期値は適当に選択する。
            SelectedStaff = StaffList[r.Next(0, StaffList.Count)];
        }

        #endregion //Ctor

        #region Properties
        
        /// <summary>選択されたスタッフ情報</summary>
        public Staff SelectedStaff
        {
            get { return _selectedStaff; }
            set { SetProperty<Staff>(ref _selectedStaff, value, "SelectedStaff"); }
        }

        /// <summary>選択されたスタッフID</summary>
        public int SelectedId
        {
            get { return _selectedId; }
            set { SetProperty<int>(ref _selectedId, value, "SelectedId"); }
        }

        /// <summary>選択されたスタッフ名</summary>
        public string SelectedStaffName
        {
            get { return _selectedStaffName; }
            set { SetProperty<string>(ref _selectedStaffName, value, "SelectedStaffName"); }
        }

        /// <summary>選択されたスタッフの役割</summary>
        public Role SelectedStaffRole
        {
            get { return _selectedStaffRole; }
            set { SetProperty<Role>(ref _selectedStaffRole, value, "SelectedStaffRole"); }
        }

        /// <summary>
        /// スタッフ情報リスト
        /// </summary>
        public ObservableCollection<Staff> StaffList { get; private set; }

        #endregion //Properties

        #region Methods

        #endregion //Methods

    }
}
