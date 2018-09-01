using System.Windows.Input;
using CleanArchitecture.Wpf.UseCases;
using Prism.Commands;
using Prism.Mvvm;

namespace CleanArchitecture.Wpf.ViewModels
{
    // ShellView のVM クラス
    public class ShellViewModel : BindableBase
    {
        private readonly IExampleUseCase _exampleUseCase;

        // コンストラクタでInjection する。
        public ShellViewModel(IExampleUseCase exampleUseCase)
        {
            _exampleUseCase = exampleUseCase;

            WorkCommand = new DelegateCommand(async () => await _exampleUseCase.Work());
        }

        public ICommand WorkCommand { get; private set; }
    }
}