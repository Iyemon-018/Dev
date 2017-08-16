using System.Windows;

namespace MediaPlayerNoiseApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<MediaPlayer> _mediaPlayers = new List<MediaPlayer>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            string selectedMusicFilePath = FileNamesListBox.SelectedItem as string;
            if (selectedMusicFilePath != null)
            {
                MediaPlayer targetPlayer = _mediaPlayers.FirstOrDefault(x => x.Source.LocalPath.Equals(selectedMusicFilePath));
                if (targetPlayer != null)
                {
                    if (targetPlayer.IsMuted) targetPlayer.IsMuted = false;
                    targetPlayer.Play();
                }
            }
        }
        
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.EnumerateFiles(@"C:\work\Musics", "*.wav").ToArray();
            FileNamesListBox.ItemsSource = files;
            foreach (string file in files)
            {
                var mediaPlayer= new MediaPlayer
                                     {
                                         IsMuted = true
                                     };
                mediaPlayer.Open(new Uri(file));
                _mediaPlayers.Add(mediaPlayer);
            }
        }
    }
}
