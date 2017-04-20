namespace MediaPlayer
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Mvvm;

    public class MainViewModel : BindableBase
    {
        public DelegateCommand PlayModeChangeCommand { get; private set; }

        public DelegateCommand NextCommand { get; private set; }

        public DelegateCommand PreviewCommand { get; private set; }

        public ObservableCollection<string> MusicFileNames { get; private set; }

        /// <summary>
        /// 現在の楽曲ファイル名
        /// </summary>
        private string _currentMusicFileName;

        /// <summary>
        /// 現在の楽曲ファイル名を設定、または取得します。
        /// </summary>
        public string CurrentMusicFileName
        {
            get { return _currentMusicFileName; }
            set { SetProperty(ref _currentMusicFileName, value); }
        }

        public MediaClock MediaClock { get; private set; }

        private static readonly string CurrentMusicDirectory = @"C:\Users\i_m_a\Music\GoogleMusic";
        
        public MainViewModel()
        {
            PropertyChanged += OnPropertyChangedCore;

            MusicFileNames = new ObservableCollection<string>(Directory.GetFiles(CurrentMusicDirectory, "*.mp3").Select(Path.GetFileName));

            if (MusicFileNames.Any())
            {
                CurrentMusicFileName = MusicFileNames[0];
            }

            PlayModeChangeCommand = new DelegateCommand(() =>
                                                        {
                                                            if (MediaClock.CurrentState == ClockState.Active && !MediaClock.IsPaused)
                                                            {
                                                                // 再生中
                                                                MediaClock.Controller.Pause();
                                                            }
                                                            else if (MediaClock.CurrentState == ClockState.Active && MediaClock.IsPaused)
                                                            {
                                                                // 一時停止中
                                                                MediaClock.Controller.Resume();
                                                            }
                                                            else if (MediaClock.CurrentState == ClockState.Filling)
                                                            {
                                                                // ???
                                                                MediaClock.Controller.Resume();
                                                            }
                                                            else if (MediaClock.CurrentState == ClockState.Stopped)
                                                            {
                                                                // 停止中
                                                                MediaClock.Controller.Begin();
                                                            }
                                                        });
        }

        private void OnPropertyChangedCore(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CurrentMusicFileName):
                    OnCurrentMusicFileNameChanged(CurrentMusicFileName);
                    break;
            }
        }

        private void OnCurrentMusicFileNameChanged(string newValue)
        {
            var uri = new Uri(Path.Combine(CurrentMusicDirectory, newValue));
            if (MediaClock == null)
            {
                var mediaTimeLine = new MediaTimeline(uri);
                MediaClock = mediaTimeLine.CreateClock(true) as MediaClock;
                MediaClock.Controller.Stop();
            }
            else
            {
                MediaClock.Controller.Stop();
                MediaClock.Timeline.Source = uri;
                MediaClock.Controller.Stop();
            }
        }
    }
}