using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Prism.Mvvm;

namespace ExplorerTreeView
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            Files = new ObservableCollection<FileInfo>();
            var location = Assembly.GetExecutingAssembly().Location;

            // ３階層上がる
            location = Enumerable.Range(0, 3).Aggregate(location, (current, value) => Directory.GetParent(current).FullName);

             //Directory.GetFiles(location)
        }

        public ObservableCollection<FileInfo> Files { get; private set; }
    }
}