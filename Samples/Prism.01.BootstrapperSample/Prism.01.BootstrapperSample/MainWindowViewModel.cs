namespace Prism._01.BootstrapperSample
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Prism.Commands;
    using Prism._01.BootstrapperSample.ViewModels;

    /// <summary>
    /// MainWindow 用のViewModel機能クラスです。
    /// </summary>
    /// <seealso cref="Prism._01.BootstrapperSample.ViewModelBase" />
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// 選択されたユーザー
        /// </summary>
        private Person _selectedPerson;

        #endregion

        #region Ctor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            People = new ObservableCollection<Person>();
            AddPersonCommand = new DelegateCommand(OnExecuteAddPersonCommand);
            RemovePersonCommand = new DelegateCommand(OnExecuteRemovePersonCommand);
        }

        #endregion

        #region Properties

        /// <summary>
        /// ユーザー情報リストを取得します。
        /// </summary>
        public ObservableCollection<Person> People { get; private set; }

        /// <summary>
        /// ユーザー追加コマンドを設定、取得します。
        /// </summary>
        public DelegateCommand AddPersonCommand { get; private set; }

        /// <summary>
        /// ユーザー削除コマンドを設定、取得します。
        /// </summary>
        public DelegateCommand RemovePersonCommand { get; private set; }

        /// <summary>
        /// 選択されたユーザーを設定、または取得します。
        /// </summary>
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// ユーザー追加コマンドを実行します。
        /// </summary>
        private void OnExecuteAddPersonCommand()
        {
            People.Add(new Person());
        }

        /// <summary>
        /// ユーザー削除コマンドを実行します。
        /// </summary>
        private void OnExecuteRemovePersonCommand()
        {
            if (SelectedPerson != null)
            {
                People.Remove(SelectedPerson);
            }
        }

        #endregion
    }
}