using System;
using System.Threading.Tasks;
using CleanArchitecture.Wpf.Presenters;

namespace CleanArchitecture.Wpf.UseCases
{
    public class ExampleUseCase : IExampleUseCase
    {
        private readonly IProgressPresenter _progressPresenter;

        public ExampleUseCase(IProgressPresenter progressPresenter)
        {
            _progressPresenter = progressPresenter;
        }

        public IProgressPresenter ProgressPresenter => _progressPresenter;

        public async Task Work()
        {
            ProgressPresenter.SetCount(10);

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                ProgressPresenter.Add(1);
            }
        }
    }
}