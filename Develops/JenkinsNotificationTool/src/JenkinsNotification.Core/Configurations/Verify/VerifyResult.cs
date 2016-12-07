namespace JenkinsNotification.Core.Configurations.Verify
{
    public class VerifyResult
    {
        public bool Correct { get; private set; }

        public string ErrorMessage { get; private set; }

        public VerifyResult() : this(true, string.Empty)
        {
            
        }

        public VerifyResult(bool correct, string errorMessage)
        {
            Correct = correct;
            ErrorMessage = errorMessage;
        }
    }
}