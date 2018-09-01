namespace CleanArchitecture.Wpf.Presenters
{
    // このサンプルのPresenter
    // わかりやすく、進捗表示するものとする。
    public interface IProgressPresenter
    {
        int Value { get; }

        bool Completed { get; }

        void SetCount(int count);

        void Add(int value);
    }
}