namespace JenkinsNotification.CustomControls
{
    using System.Windows;
    using Core.Utility;
    using Microsoft.Practices.Prism.Mvvm;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.IView" />
    /// <seealso cref="System.Windows.Window" />
    public class View : Window, IView
    {
        static View()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(View), new FrameworkPropertyMetadata(typeof(View)));
        }

        public View()
        {
            ViewUtility.InjectionViewModelLocator(this);
        }
    }
}