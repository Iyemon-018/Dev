using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarouselPanelSample
{
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using Microsoft.Practices.Prism.Commands;

    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<MusicData> Musics { get; private set; }

        public IEnumerable<MusicData> ActualMusics { get; private set; }

        /// <summary>
        /// 表示数
        /// </summary>
        private int _capacity;

        /// <summary>
        /// 表示数を設定、または取得します。
        /// </summary>
        public int Capacity
        {
            get { return _capacity; }
            private set { SetProperty(ref _capacity, value); }
        }

        /// <summary>
        /// 非表示データのインデックス
        /// </summary>
        private int _hiddenItemIndex;

        /// <summary>
        /// 非表示データのインデックスを設定、または取得します。
        /// </summary>
        public int HiddenItemIndex
        {
            get { return _hiddenItemIndex; }
            private set { SetProperty(ref _hiddenItemIndex, value); }
        }

        public MainWindowViewModel()
        {
            ActualMusics = Enumerable.Range(0, 10).Select(x => new MusicData(x + 1, $"Title No.{x + 1:d2}", $"Artist {x + 1:d2}"));
            Musics = new ObservableCollection<MusicData>(ActualMusics);
            Capacity = 6;

            PreviousCommand = new DelegateCommand(() =>
                                                  {
                                                      if (0 < SelectedMusicIndex)
                                                      {
                                                          SelectedMusicIndex--;
                                                      }
                                                      else
                                                      {
                                                          SelectedMusicIndex = ActualMusics.Count() - 1;
                                                      }
                                                  });

            NextCommand = new DelegateCommand(() =>
                                              {
                                                  if (SelectedMusicIndex < ActualMusics.Count() - 1)
                                                  {
                                                      SelectedMusicIndex++;
                                                  }
                                                  else
                                                  {
                                                      SelectedMusicIndex = 0;
                                                  }
                                              });
        }

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            var result = base.SetProperty(ref storage, value, propertyName);
            switch (propertyName)
            {
                case nameof(SelectedMusicIndex):
                    OnSelectedMusicIndexChanged(SelectedMusicIndex);
                    break;
            }
            return result;
        }

        private void OnSelectedMusicIndexChanged(int newValue)
        {
            var nextIndex = Musics.Count - (newValue + Capacity);
            if (nextIndex < 0)
            {
                nextIndex = Math.Abs(nextIndex);
                HiddenItemIndex = nextIndex;
            }
            else
            {
                HiddenItemIndex = newValue + Capacity;
            }
        }

        /// <summary>
        /// 選択中の項目インデックス
        /// </summary>
        private int _selectedMusicIndex;

        /// <summary>
        /// 選択中の項目インデックスを設定、または取得します。
        /// </summary>
        public int SelectedMusicIndex
        {
            get { return _selectedMusicIndex; }
            private set { SetProperty(ref _selectedMusicIndex, value); }
        }
        
        public DelegateCommand PreviousCommand { get; private set; }

        public DelegateCommand NextCommand { get; private set; }
    }
}
