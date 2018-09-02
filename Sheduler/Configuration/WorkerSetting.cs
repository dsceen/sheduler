namespace Sheduler.Core.Configuration
{
    public class WorkerSetting
    {
        public string PathToDll { get; set; }
        public string PathToConfig { get; set; }

        /// <summary>
        /// Use cron format: &lt;Minute&gt; &lt;Hour&gt; &lt;Day_of_the_Month&gt; &lt;Month_of_the_Year&gt; &lt;Day_of_the_Week&gt; &lt;Year&gt;<br/>
        /// 
        /// Example: <br/>
        ///* * * * * *                         Each minute<br/>
        ///59 23 31 12 5 *                     One minute  before the end of year if the last day of the year is Friday<br/>
        ///45 17 7 6 * *                       Every year, on June 7th at 17:45<br/>
        ///</summary>
        public string StartAt { get; set; }

        public int RestartAfterFailCount { get; set; }
    }
}
