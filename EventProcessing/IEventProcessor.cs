namespace KweetService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}