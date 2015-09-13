using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace T4SampleProject.ComponentModel
{
    /// <summary>
    /// 汎用的なバインディングソース機能を提供する。
    /// </summary>
    public abstract class BindableBase : ValidatableBase, INotifyPropertyChanged
    {
        #region INotifyPropertyChangedインターフェース
        
        /// <summary>
        /// プロパティ変更通知イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティが変更されたことを通知する。
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var h = PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// プロパティの値を設定する。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="field">フィールド</param>
        /// <param name="value">設定値</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>true:更新, false:同値</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = "")
        {
            if (object.Equals(field, value) == true) return false;

            field = value;

            // 値を検証する。
            ValidateProperty(value, propertyName);

            // プロパティの変更を通知する。
            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion //INotifyPropertyChangedインターフェース
    }
}
