using System;
using System.Windows.Input;
using MaterialDesign.Dialog.Example.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace MaterialDesign.Dialog.Example.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;

        [Obsolete("実際には使用しないが、デフォルトコンストラクタがないとXAML デザイナー上でバインドしたときに警告出るので仕方なく追加しておく。")]
        public ShellViewModel()
        {

        }

        public ShellViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            ShutdownCommand = new DelegateCommand(async () =>
                                                  {
                                                      var result = await _dialogService.Question("終了します。よろしいですか？");
                                                      if (result)
                                                      {
                                                          App.Current.MainWindow.Close();
                                                      }
                                                  });
        }

        public ICommand ShutdownCommand { get; private set; }
    }
}