using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MaterialDesign.Dialog.Example.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace MaterialDesign.Dialog.Example.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly IUserDataAddDialogService _userDataAddDialogService;

        [Obsolete("実際には使用しないが、デフォルトコンストラクタがないとXAML デザイナー上でバインドしたときに警告出るので仕方なく追加しておく。")]
        public ShellViewModel()
        {

        }

        public ShellViewModel(IDialogService dialogService, IUserDataAddDialogService userDataAddDialogService)
        {
            _dialogService = dialogService;
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

            UserDataAddCommand = new DelegateCommand(async () =>
                                                     {
                                                         var userData = await _userDataAddDialogService.Show();
                                                         if (userData != null)
                                                         {
                                                             UserDataItems.Add(userData);
                                                         }
                                                     });
        }

        public ObservableCollection<UserDataViewModel> UserDataItems { get; private set; }

        public ICommand ShutdownCommand { get; private set; }

        public ICommand UserDataAddCommand { get; private set; }
    }
}