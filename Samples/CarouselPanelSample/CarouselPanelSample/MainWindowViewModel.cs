using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarouselPanelSample
{
    using System.Collections.ObjectModel;
    using Microsoft.Practices.Prism.Commands;

    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<MusicData> Musics { get; private set; }

        public MainWindowViewModel()
        {
            Musics = new ObservableCollection<MusicData>(
                Enumerable.Range(0, 10)
                          .Select(x => new MusicData(x + 1, $"Title No.{x + 1:d2}", $"Artist {x + 1:d2}")));

            PreviousCommand = new DelegateCommand(() =>
                                                  {
                                                      if (0 < SelectedMusicIndex)
                                                      {
                                                          SelectedMusicIndex--;
                                                      }
                                                      else
                                                      {
                                                          SelectedMusicIndex = Musics.Count - 1;
                                                      }
                                                  });

            NextCommand = new DelegateCommand(() =>
                                              {
                                                  if (SelectedMusicIndex < Musics.Count - 1)
                                                  {
                                                      SelectedMusicIndex++;
                                                  }
                                                  else
                                                  {
                                                      SelectedMusicIndex = 0;
                                                  }
                                              });
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
