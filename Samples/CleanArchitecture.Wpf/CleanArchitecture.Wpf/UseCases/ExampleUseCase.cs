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
            _progressPresenter.SetCount(10);
        }

        public IProgressPresenter ProgressPresenter => _progressPresenter;

        public async Task Work()
        {
            // 実プロダクトでは通信やらDB アクセスやらHttp リクエストやらを実施する。
            // サンプルなので一定時間待って進捗をカウントアップする。
            _progressPresenter.Restart();

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                ProgressPresenter.Add(1);
            }
        }
    }
}