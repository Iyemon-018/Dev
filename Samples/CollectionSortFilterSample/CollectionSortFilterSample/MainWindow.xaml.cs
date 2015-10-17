using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CollectionSortFilterSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        #region ReadOnly Fields

        /// <summary>
        /// SelectedFilterRole 依存関係プロパティ定義
        /// </summary>
        public static readonly DependencyProperty SelectedFilterRoleProperty =
            DependencyProperty.Register("SelectedFilterRole"
                                      , typeof(Role)
                                      , typeof(MainWindow)
                                      , new PropertyMetadata(Role.All
                                                            , new PropertyChangedCallback(OnSelectedFilterRoleChanged)
                                      ));

        #endregion //ReadOnly Fields

        /// <summary>
        /// 当View と関連するViewModel
        /// </summary>
        MainViewModel _vm = new MainViewModel();

        /// <summary>
        /// CollectionView
        /// </summary>
        private ICollectionView _staffCollectionView;

        #endregion //Fields

        #region Ctor
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = _vm;

            // データのフィルータリング、ソートを設定する。
            // ただし、CollecionView を使用すると、
            // StaffList をバインドしたすべてのコレクションに影響を与える。
            _staffCollectionView = CollectionViewSource.GetDefaultView(_vm.StaffList);

            // フィルター条件の設定
            _staffCollectionView.Filter = x =>
            {
                var staff = x as Staff;
                if (staff != null)
                {
                    if (SelectedFilterRole == Role.All)
                    {
                        // "All" 選択時は全スタッフ表示
                        return true;
                    }

                    //該当する役割のスタッフだけ表示する。
                    return staff.Role == SelectedFilterRole;
                }

                return true;
            };

            // ソート条件の設定
            // 年功序列とする。
            // 新たにデータが追加されてもこのソート条件は有効
            _staffCollectionView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Descending));
        }

        #endregion //Ctor

        #region Properties

        #region Dependency Properties

        /// <summary>
        /// 選択されている役割フィルター条件
        /// </summary>
        [Description("選択されている役割フィルター条件"),
        Category("Custom"), Browsable(true)]
        public Role SelectedFilterRole
        {
            get { return (Role)GetValue(SelectedFilterRoleProperty); }
            set { SetValue(SelectedFilterRoleProperty, value); }
        }

        #endregion //Dependency Properties

        #endregion //Properties

        #region Methods

        /// <summary>
        /// SelectedFilterRold プロパティが変更された。
        /// </summary>
        /// <param name="d">イベント発行元オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static void OnSelectedFilterRoleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var w = d as MainWindow;
            if (w != null)
            {
                w.OnSelectedFilterRoleChanged((Role)e.NewValue);
            }
        }

        /// <summary>
        /// SelectedFilterRole プロパティの値が変更された。
        /// </summary>
        /// <param name="newValue">更新値</param>
        protected virtual void OnSelectedFilterRoleChanged(Role newValue)
        {
            if (_staffCollectionView != null)
            {
                // CollectionView を再描画
                // フィルター条件が再度実行される。
                // ソートだけならこの処理は不要。
                _staffCollectionView.Refresh();
            }
        }

        #endregion //Methods

    }
}
