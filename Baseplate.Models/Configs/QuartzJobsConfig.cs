namespace Baseplate.Models;

public class QuartzJobsConfig
{
    public const string SectionName = "QuartzJobs";
    public DeleteStaleChatroomsConfig DeleteStaleChatrooms { get; set; } = new();

}

public class DeleteStaleChatroomsConfig
{
    public string CronSchedule { get; set; } = "0 0 23 ? * SUN";
    public string TimeZone { get; set; } = "Australia/Sydney";
    public bool Enabled { get; set; } = true;
    public int PreviousDaysCount { get; set; } = 7;
}