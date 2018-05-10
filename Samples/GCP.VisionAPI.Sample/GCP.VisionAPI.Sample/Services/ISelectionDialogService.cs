using Microsoft.Win32;

namespace GCP.VisionAPI.Sample.Services
{
    public interface ISelectionDialogService
    {
        string SelectFile();
    }

    class SelectionDialogService : ISelectionDialogService
    {
        public string SelectFile()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "画像ファイル(*.png, *.jpg)|*.png;*.jpg|すべてのファイル(*.*)|*.*",
            };
            return dialog.ShowDialog() ?? false ? dialog.FileName : string.Empty;
        }
    }
}