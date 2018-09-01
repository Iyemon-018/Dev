namespace CleanArchitecture.Wpf.Presenters
{
    // このサンプルのPresenter
    // わかりやすく、進捗表示するものとする。
    public interface IProgressPresenter
    {
        int Value { get; }

        int Count { get; }

        bool Completed { get; }

        void SetCount(int count);

        void Restart();

        void Add(int value);
    }
}