namespace Mc.Common.Views.Services
{
    using System;

    public interface IDialogService
    {
        IDialogResultService ShowDialog(string caption, string message, DialogButtonType buttonType);
    }

    public interface IDialogResultService
    {
        IDialogResultService OnSelectedOk(Action<IDialogResultData> action);

        IDialogResultService OnSelectedCancel(Action<IDialogResultData> action);

        IDialogResultService OnSelectedYes(Action<IDialogResultData> action);

        IDialogResultService OnSelectedNo(Action<IDialogResultData> action);
    }

    public interface IDialogResultData
    {
        DialogResultKind Result { get; }
    }
}