namespace JenkinsNotification.Core.ComponentModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.ViewModel;

    public abstract class ViewModelBase : BindableBase, INotifyDataErrorInfo
    {
        private readonly ErrorsContainer<IErrorData> _errorsContainer;

        protected ViewModelBase()
        {
            _errorsContainer = new ErrorsContainer<IErrorData>(OnErrorChanged);
        }

        private void OnErrorChanged([CallerMemberName] string propertyName = null)
        {
            
        }

        #region INotifyDataErrorInfo メンバーの実装

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsContainer.GetErrors(propertyName);
        }

        public bool HasErrors => _errorsContainer.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion

        // http://sourcechord.hatenablog.com/entry/2014/06/11/072617 ここを参考に。

        protected void ValidateProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            var context = new ValidationContext(this) {MemberName = propertyName};
            var validationErrors = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(value, context, validationErrors))
            {
                var errors = validationErrors.Select(x => x.ErrorMessage);
                _errorsContainer.SetErrors(propertyName, errors);
            }
        }

    }
}