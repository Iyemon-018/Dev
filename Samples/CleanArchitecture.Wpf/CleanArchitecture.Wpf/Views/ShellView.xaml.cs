using System.Windows;
using CleanArchitecture.Wpf.Presenters;
using CleanArchitecture.Wpf.UseCases;
using CleanArchitecture.Wpf.ViewModels;

namespace CleanArchitecture.Wpf.Views
{
    /// <summary>
    /// このサンプルのView
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();

            // ここは本来Di Container を使うべき。
            // サンプルのため、以下の方法でInjection している。
            var useCase = new ExampleUseCase(new ProgressPresenter());
            DataContext = new ShellViewModel(useCase);
        }
    }
}