using SelializeSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SelializeSample.Views
{
    /// <summary>
    /// BinaryFormatterView.xaml の相互作用ロジック
    /// </summary>
    public partial class BinaryFormatterView : Window
    {
        /// <summary>
        /// 当画面のViewModel
        /// </summary>
        private BinaryFormatterViewModel _vm;

        public BinaryFormatterView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// リセットボタンをクリックした際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント発行元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // リセット
            _vm.Reset();
        }

        /// <summary>
        /// 戻るボタンをクリックした際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント発行元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.IsUpdate())
            {
                var result = MessageBox.Show("データが更新されています。\r\n入力内容を破棄しますか？"
                                            , "確認"
                                            , MessageBoxButton.YesNo
                                            , MessageBoxImage.Question
                                            , MessageBoxResult.No);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }
            }
            Close();
        }

        /// <summary>
        /// 当ウィンドウがロードされた際に呼ばれるイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント発行元オブジェクト</param>
        /// <param name="e">イベント引数オブジェクト</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _vm = DataContext as BinaryFormatterViewModel;
            if (_vm != null)
            {
                // これ以降に変更されたら変更ダイアログを表示します。
                _vm.SaveSnapshotData();
            }
        }
    }
}
