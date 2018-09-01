using System.Windows.Input;
using CleanArchitecture.Wpf.Presenters;
using CleanArchitecture.Wpf.UseCases;
using Prism.Commands;
using Prism.Mvvm;

namespace CleanArchitecture.Wpf.ViewModels
{
    // ShellView のVM クラス
    public class ShellViewModel : BindableBase
    {
        private readonly IExampleUseCase _exampleUseCase;

        public ShellViewModel()
        {

        }

        // コンストラクタでInjection する。
        public ShellViewModel(IExampleUseCase exampleUseCase)
        {
            _exampleUseCase = exampleUseCase;
            _canBeginWork = true;

            WorkCommand = new DelegateCommand(async () =>
                                              {
                                                  CanBeginWork = false;
                                                  await _exampleUseCase.Work();
                                                  CanBeginWork = true;
                                              })
                                            .ObservesCanExecute(() => CanBeginWork);
        }

        public IProgressPresenter Progress => _exampleUseCase.ProgressPresenter;

        public ICommand WorkCommand { get; private set; }

        private bool _canBeginWork;

        public bool CanBeginWork
        {
            get => _canBeginWork;
            private set => SetProperty(ref _canBeginWork, value);
        }

    }
}