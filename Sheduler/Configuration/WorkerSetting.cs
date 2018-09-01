namespace Sheduler.Core.Configuration
{
    public class WorkerSetting
    {
        public string PathToDll { get; set; }
        public string PathToConfig { get; set; }
        public string StartAt { get; set; }
        public int RestartAfterFailCount { get; set; }
    }
}
