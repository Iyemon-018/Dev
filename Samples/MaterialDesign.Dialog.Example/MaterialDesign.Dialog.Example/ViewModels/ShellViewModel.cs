using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using MaterialDesign.Dialog.Example.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace MaterialDesign.Dialog.Example.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private static readonly List<UserDataViewModel> AllUserDataCache
            = new List<UserDataViewModel>
              {
                  {new UserDataViewModel {FirstName = "名無しの", LastName = "権兵衛", Age = 46}}
                , {new UserDataViewModel {FirstName = "寿限無", LastName  = "清十郎", Age = 18}}
                , {new UserDataViewModel {FirstName = "田中", LastName   = "太郎", Age  = 23}}
              };

        private readonly IDialogService _dialogService;
        private readonly IProgressDialogService _progressDialogService;
        private readonly IUserDataAddDialogService _userDataAddDialogService;

        [Obsolete("実際には使用しないが、デフォルトコンストラクタがないとXAML デザイナー上でバインドしたときに警告出るので仕方なく追加しておく。")]
        public ShellViewModel()
        {

        }

        public ShellViewModel(IDialogService dialogService, IProgressDialogService progressDialogService
                            , IUserDataAddDialogService userDataAddDialogService)
        {
            _dialogService            = dialogService;
            _progressDialogService    = progressDialogService;
            _userDataAddDialogService = userDataAddDialogService;

            UserDataItems = new ObservableCollection<UserDataViewModel>();

            ShutdownCommand = new DelegateCommand(async () =>
                                                  {
                                                      bool result = await _dialogService.Question("終了します。よろしいですか？");
                                                      if (result)
                                                      {
                                                          App.Current.MainWindow.Close();
                                                      }
                                                  });

            GetAllUserDataCommand = new DelegateCommand(ExecuteGetAllUserDataCommand);

            UserDataAddCommand = new DelegateCommand(async () =>
                                                     {
                                                         var userData = await _userDataAddDialogService.Show();
                                                         if (userData != null)
                                                         {
                                                             UserDataItems.Add(userData);
                                                         }
                                                     });
        }

        private async void ExecuteGetAllUserDataCommand()
        {
            List<UserDataViewModel> allUserData = new List<UserDataViewModel>();

            await _progressDialogService.Begin(3
                                             , async x =>
                                               {
                                                   x.SetMessage($"全ユーザー情報を取得します。{Environment.NewLine}"
                                                                + $"しばらくお待ち下さい");

                                                   int counter = 0;
                                                   foreach (var userData in AllUserDataCache)
                                                   {
                                                       await Task.Delay(TimeSpan.FromSeconds(2)); // 意味もなく少し待つ。
                                                       allUserData.Add(userData);
                                                       counter++;
                                                       x.UpdateProgress(counter);
                                                   }

                                                   x.SetMessage($"ユーザー情報を確認しています。");
                                                   await Task.Delay(TimeSpan.FromSeconds(3)); // 意味もなく少し待つ。

                                                   x.Complete();
                                               });

            UserDataItems.Clear();
            UserDataItems.AddRange(allUserData);
        }

        public ObservableCollection<UserDataViewModel> UserDataItems { get; private set; }

        public ICommand ShutdownCommand { get; private set; }

        public ICommand GetAllUserDataCommand { get; private set; }

        public ICommand UserDataAddCommand { get; private set; }
    }
}