using GCP.VisionAPI.Sample.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace GCP.VisionAPI.Sample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _notifyMessage;

        public string NotifyMessage
        {
            get => _notifyMessage;
            set => SetProperty(ref _notifyMessage, value);
        }

        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }


        public DelegateCommand OpenSelectionDialogCommand { get; private set; }

        public DelegateCommand AnalyzeCommand { get; private set; }

        private readonly ISelectionDialogService selectionDialogService;

        public MainWindowViewModel(ISelectionDialogService selectionDialogService)
        {
            this.selectionDialogService = selectionDialogService;
            OpenSelectionDialogCommand = new DelegateCommand(() => FileName = this.selectionDialogService.SelectFile());

            AnalyzeCommand = new DelegateCommand(ExecuteAnalyzeCommand);
        }

        private void ExecuteAnalyzeCommand()
        {
            
        }
    }
}