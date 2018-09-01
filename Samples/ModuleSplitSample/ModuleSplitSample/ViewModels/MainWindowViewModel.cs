using ModuleSplitSample.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ModuleSplitSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<ISampleInformation> SampleInformations { get; private set; }

        public MainWindowViewModel()
        {
            SampleInformations = new ObservableCollection<ISampleInformation>();
            SampleInformations.Add(new CurrentSampleInformation());
            var moduleNames = new []{ "ModuleA", "ModuleB" };
            foreach (var moduleName in moduleNames)
            {
                var items = LoadInterface(moduleName);
                SampleInformations.AddRange(items);
            }
        }

        private IEnumerable<ISampleInformation> LoadInterface(string moduleName)
        {
            var assembly = Assembly.LoadFrom($"ModuleSplitSample.{moduleName}.dll");
            var types = assembly.GetTypes().Where(typeof(ISampleInformation).IsAssignableFrom);
            if (types.Any())
            {
                return types.Select(x => Activator.CreateInstance(x) as ISampleInformation);
            }
            return null;
        }

        private class CurrentSampleInformation : ISampleInformation
        {
            public string Name => "Name";

            public string TypeName => "TypeName";

            public string ModelNumberName => "ModelNumberName";
        }
    }
}
