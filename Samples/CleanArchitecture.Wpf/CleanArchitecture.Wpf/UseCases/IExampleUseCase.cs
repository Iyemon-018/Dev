using System.Threading.Tasks;
using CleanArchitecture.Wpf.Presenters;

namespace CleanArchitecture.Wpf.UseCases
{
    // このサンプルのUseCase
    // このUseCase が実装する機能は今回は対象外とする。
    public interface IExampleUseCase
    {
        IProgressPresenter ProgressPresenter { get; }

        Task Work();
    }
}