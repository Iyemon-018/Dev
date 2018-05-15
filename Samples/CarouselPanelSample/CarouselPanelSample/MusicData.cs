namespace CarouselPanelSample
{
    using System;

    public class MusicData : ViewModelBase
    {
        /// <summary>
        /// 楽曲番号
        /// </summary>
        private int _number;

        public MusicData(int number, string title, string artist)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrEmpty(artist)) throw new ArgumentNullException(nameof(artist));
            _number = number;
            _title = title;
            _artist = artist;
        }

        /// <summary>
        /// 楽曲番号を設定、または取得します。
        /// </summary>
        public int Number
        {
            get { return _number; }
            private set { SetProperty(ref _number, value); }
        }

        /// <summary>
        /// 楽曲タイトル
        /// </summary>
        private string _title;

        /// <summary>
        /// 楽曲タイトルを設定、または取得します。
        /// </summary>
        public string Title
        {
            get { return _title; }
            private set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// アーティスト名
        /// </summary>
        private string _artist;

        /// <summary>
        /// アーティスト名を設定、または取得します。
        /// </summary>
        public string Artist
        {
            get { return _artist; }
            private set { SetProperty(ref _artist, value); }
        }
    }
}