namespace Prism._01.BootstrapperSample
{
    using System.Collections.ObjectModel;
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
            People = new ObservableCollection<Person>
                         {
                             new Person
                                 {
                                     Name = "JugemuJugemu.",
                                     Age = 20,
                                     Gender = GenderType.Male,
                                     Address = "大阪府大阪市福島区吉野三丁目１２－２５ 参考ビル401",
                                     MailAddress = "jjugemu@microcircus.com"
                                 },
                         };
            AddPersonCommand = new DelegateCommand(OnExecuteAddPersonCommand);
            RemovePersonCommand = new DelegateCommand<Person>(OnExecuteRemovePersonCommand);
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
        /// <remarks>
        /// ジェネリックのDelegateCommandを使用すると、CommandParameterが使用できるようになる。
        /// </remarks>
        public DelegateCommand<Person> RemovePersonCommand { get; private set; }

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
        /// <param name="person">選択されたユーザー</param>
        private void OnExecuteRemovePersonCommand(Person person)
        {
            if (person != null)
            {
                People.Remove(person);
            }
        }

        #endregion
    }
}