namespace NavigationSample.Services
{
    public interface IDataStore
    {
        IPageViewData CurrentViewData { get; }
    }
}