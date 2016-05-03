using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Mvvm;

namespace ExplorerTreeView
{
    public class FileInfo : BindableBase
    {
        public FileInfo()
        {
            Children = new ObservableCollection<FileInfo>();
        }

        /// <summary>
        /// ファイル名/フォルダ名
        /// </summary>
        private string _name;

        /// <summary>
        /// ファイル名/フォルダ名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        
        /// <summary>
        /// 子要素
        /// </summary>
        public ObservableCollection<FileInfo> Children { get ; private set; }
    }
}