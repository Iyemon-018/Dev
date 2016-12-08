namespace JenkinsNotification.Core.Utility
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using Microsoft.Practices.Prism.Mvvm;

    /// <summary>
    /// 画面に関するユーティリティ機能クラスです。
    /// </summary>
    public static class ViewUtility
    {
        /// <summary>
        /// 現在のエントリポイントでアクティブな<see cref="T:Window"/>を取得します。
        /// </summary>
        /// <returns>アクティブな<see cref="T:Window"/></returns>
        public static Window GetActiveWindow()
        {
            return GetCurrentWindows().FirstOrDefault(x => x.IsActive);
        }

        /// <summary>
        /// 現在のエントリポイントのメイン画面を取得します。
        /// </summary>
        /// <returns>メイン画面</returns>
        public static Window GetMainWindow()
        {
            return Application.Current.MainWindow;
        }

        /// <summary>
        /// 現在のエントリポイントでインスタンス化されている<see cref="T:Window"/> コレクションを取得します。
        /// </summary>
        /// <returns><see cref="T:Window"/> コレクション</returns>
        private static IEnumerable<Window> GetCurrentWindows()
        {
            return Application.Current.Windows.OfType<Window>();
        }

        /// <summary>
        /// デザイナーモードで起動しているかどうかを判定します。
        /// </summary>
        /// <returns>判定結果(true:デザイナーモード, false:実行モード)</returns>
        public static bool IsDesignMode()
        {
            return DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }

        public static void InjectionViewModelLocator(DependencyObject view)
        {
            ViewModelLocator.SetAutoWireViewModel(view, true);
        }
    }
}