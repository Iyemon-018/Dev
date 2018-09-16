using System;
using System.ComponentModel;
using Prism.Mvvm;

namespace MaterialDesign.Dialog.Example.Dialogs.ViewModels
{
    public class ProgressDialogViewModel : BindableBase
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private int _count;

        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        private int _progress;

        public int Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        private bool _completed;

        public bool Completed
        {
            get => _completed;
            set => SetProperty(ref _completed, value);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(Completed):
                    OnCompletedChanged(Completed);
                    break;
            }
        }

        private void OnCompletedChanged(bool newValue)
        {
            if (newValue)
            {
                OnCompletedProgress();
            }
        }

        public event EventHandler CompletedProgress;

        protected virtual void OnCompletedProgress()
        {
            CompletedProgress?.Invoke(this, EventArgs.Empty);
        }
    }
}