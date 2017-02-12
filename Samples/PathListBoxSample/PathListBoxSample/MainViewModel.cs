namespace PathListBoxSample
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Mvvm;

    public class MainViewModel : BindableBase
    {
        public ObservableCollection<int> Values { get; private set; }

        public DelegateCommand AddCommand { get; private set; }

        public DelegateCommand SubCommand { get; private set; }

        public MainViewModel()
        {
            Values = new ObservableCollection<int>(Enumerable.Range(1, 5));
            AddCommand = new DelegateCommand(() => Values.Add(Values.Max() + 1));
            SubCommand = new DelegateCommand(() => { if (Values.Any()) Values.Remove(Values.Max());});
    }
    }
}