namespace JenkinsNotificationTool.Tests.Core.Services
{
    using System;
    using System.Collections.Generic;
    using JenkinsNotification.Core.Services;
    using Xunit.Abstractions;

    public class MockDialogService : IDialogService
    {
        private readonly ITestOutputHelper _outputHelper;

        private static readonly Queue<bool> _results = new Queue<bool>();

        public static void SetResult(string key, bool result)
        {
            _results.Enqueue(result);
        }

        public static void ClearResult()
        {
            _results.Clear();
        }

        public MockDialogService(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public void ShowInformation(string message)
        {
            _outputHelper.WriteLine("[Show Information]" + Environment.NewLine + message);
        }

        public bool ShowQuestion(string message)
        {
            _outputHelper.WriteLine("[Show Question]" + Environment.NewLine + message);
            return _results.Dequeue();
        }

        public bool ShowWarning(string message)
        {
            _outputHelper.WriteLine("[Show Warning]" + Environment.NewLine + message);
            return _results.Dequeue();
        }

        public void ShowError(string message)
        {
            _outputHelper.WriteLine("[Show Error]" + Environment.NewLine + message);
        }
    }
}