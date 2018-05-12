using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Google.Cloud.Vision.V1;
using Prism.Mvvm;

namespace GCP.VisionAPI.Sample.Models
{
    public class TextAnalysisResult : BindableBase
    {
        public static TextAnalysisResult Build(TextAnnotation analysisResult)
        {
            Page page = analysisResult.Pages.FirstOrDefault();
            TextAnalysisResult result = new TextAnalysisResult();

            result.DetectedLanguages.AddRange(page.Property.DetectedLanguages.Select(x => x.LanguageCode).ToArray());
            result.Width = page.Width;
            result.Height = page.Height;

            return result;
        }

        public List<string> DetectedLanguages { get; } = new List<string>();

        private int _width;

        public int Width
        {
            get => _width;
            private set => SetProperty(ref _width, value);
        }

        private int _height;

        public int Height
        {
            get => _height;
            private set => SetProperty(ref _height, value);
        }

    }
}