using Prism.Mvvm;

namespace CleanArchitecture.Wpf.Presenters
{
    // View に進捗率を通知したいのでBindableBase を継承する。
    public class ProgressPresenter : BindableBase, IProgressPresenter
    {
        private int _value;

        public int Value
        {
            get => _value;
            private set => SetProperty(ref _value, value);
        }

        private bool _completed;

        public bool Completed
        {
            get => _completed;
            private set => SetProperty(ref _completed, value);
        }

        private int _count;

        public void SetCount(int count)
        {
            _count = count;
            this.Value = 0;
            this.Completed = false;
        }

        public void Add(int value)
        {
            var newValue = this.Value + value;
            if (_count > newValue)
            {
                this.Value = _count;
                this.Completed = true;
            }
            else
            {
                this.Value += value;
            } 
        }
    }
}