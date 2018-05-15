namespace NotifyIconSample
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using NotifyIconSample.Annotations;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected IBalloonTipService BalloonTipService { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(member, value))
            {
                return false;
            }

            member = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected ViewModelBase(IBalloonTipService balloonTipService)
        {
            BalloonTipService = balloonTipService;
        }
    }
}