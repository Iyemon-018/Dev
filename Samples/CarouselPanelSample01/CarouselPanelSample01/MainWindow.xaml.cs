namespace CarouselPanelSample01
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            // ひとつ前の要素を選択する。
            var selectedIndex = pathListBox.SelectedIndex;
            if (selectedIndex < pathListBox.Items.Count - 1)
            {
                pathListBox.SelectedIndex++;
            }
            else
            {
                pathListBox.SelectedIndex = 0;
            }
        }

        private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            // 次の要素を選択する。
            var selectedIndex = pathListBox.SelectedIndex;
            if (0 < selectedIndex)
            {
                pathListBox.SelectedIndex--;
            }
            else
            {
                pathListBox.SelectedIndex = pathListBox.Items.Count - 1;
            }
        }
    }
}
