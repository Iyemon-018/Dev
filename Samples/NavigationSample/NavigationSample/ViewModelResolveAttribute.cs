namespace NavigationSample
{
    using System;
    public class ViewModelResolveAttribute : Attribute
    {
        private readonly Type _viewModelType;

        public ViewModelResolveAttribute(Type viewModelType)
        {
            _viewModelType = viewModelType;
        }

        public Type ViewModelType => _viewModelType;
    }
}