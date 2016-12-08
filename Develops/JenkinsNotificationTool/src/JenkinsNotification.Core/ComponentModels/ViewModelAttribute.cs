namespace JenkinsNotification.Core.ComponentModels
{
    using System;

    public sealed class ViewModelAttribute : Attribute
    {
        public ViewModelAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        public Type ViewModelType { get; }
    }
}