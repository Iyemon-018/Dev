using System.Windows;

namespace DataGridColumnBinding
{
    /// <summary>
    /// ViewModelのバインディングソースの代理として働くクラスです。
    /// </summary>
    public class BindingProxy : Freezable
    {
        /// <summary>
        /// Freezableオブジェクトのインスタンスを生成します。
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
        
        /// <summary>
        /// 間をとりもつプロパティ
        /// データバインドした場合は、このプロパティがViewModelの代わりになる。
        /// </summary>
        public object Data
        {
            get { return (object) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        
        /// <summary>
        /// Data の依存関係プロパティ定義
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof (object), typeof (BindingProxy), new UIPropertyMetadata(null));
    }
}