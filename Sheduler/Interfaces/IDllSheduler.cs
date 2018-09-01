using System;
using System.Threading.Tasks;

namespace Sheduler.Core.Interfaces
{
    public interface IDllSheduler
    {
        void Start();
        void Stop();
        bool IsStarted { get; }
        Task DoWorkerJob(Guid workerId);
    }
}