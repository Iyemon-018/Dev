using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using GCP.VisionAPI.Sample.Services;
using Google.Cloud.Vision.V1;
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

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            private set => SetProperty(ref _imageSource, value);
        }


        public ObservableCollection<string> AnalysisResults { get; } = new ObservableCollection<string>();

        public ObservableCollection<Block> Blocks { get; } = new ObservableCollection<Block>();

        private string _analysisResultFlatten;

        public string AnalysisResultFlatten
        {
            get => _analysisResultFlatten;
            private set => SetProperty(ref _analysisResultFlatten, value);
        }


        public DelegateCommand OpenSelectionDialogCommand { get; private set; }

        public DelegateCommand AnalyzeCommand { get; private set; }

        private readonly ISelectionDialogService selectionDialogService;

        public MainWindowViewModel(ISelectionDialogService selectionDialogService)
        {
            this.selectionDialogService = selectionDialogService;
            OpenSelectionDialogCommand = new DelegateCommand(() =>
            {
                FileName = this.selectionDialogService.SelectFile();
                ImageSource = new BitmapImage(new Uri(FileName));
            });

            AnalyzeCommand = new DelegateCommand(ExecuteAnalyzeCommand);
        }

        private void ExecuteAnalyzeCommand()
        {
            NotifyMessage = "画像解析（OCR）を開始する。";
            TextAnnotation response = getAnalysisResult(FileName);
            NotifyMessage = "解析結果を集計する。";
            Block[] blocks = response.Pages.SelectMany(x => x.Blocks).ToArray();

            this.Blocks.Clear();
            this.Blocks.AddRange(blocks);

            Paragraph[] paragraphs = blocks.SelectMany(x => x.Paragraphs).ToArray();
            Word[] words = paragraphs.SelectMany(x => x.Words).ToArray();
            Symbol[] symbols = words.SelectMany(x => x.Symbols).ToArray();
            string[] textList = symbols.Select(x => x.Text).ToArray();
            AnalysisResults.Clear();
            AnalysisResults.AddRange(textList);
            AnalysisResultFlatten = string.Join("", AnalysisResults);
            NotifyMessage = "解析が完了した。";
        }

        private TextAnnotation getAnalysisResult(string fileName)
        {
            Image image = Image.FromFile(fileName);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            TextAnnotation response = client.DetectDocumentText(image);

            return response;
        }
    }
}