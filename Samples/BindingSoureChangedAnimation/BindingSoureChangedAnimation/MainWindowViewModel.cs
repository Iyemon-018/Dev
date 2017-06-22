namespace BindingSoureChangedAnimation
{
    using Microsoft.Practices.Prism.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        private bool _isAnimation;

        public bool IsAnimation
        {
            get => _isAnimation;
            set => SetProperty(ref _isAnimation, value);
        }
    }
}