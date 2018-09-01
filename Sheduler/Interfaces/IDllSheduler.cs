namespace Sheduler.Core.Interfaces
{
    public interface IDllSheduler
    {
        void Start();
        void Stop();
        bool IsStarted { get; }
    }
}