namespace ASP_P26.Services.Time;

public class IdentityService(ITimeService timeService)
{
    private readonly ITimeService _timeService = timeService;
    private static long _counter;

    public long GetIdentity()
    {
        string temp = _timeService.Timestamp().ToString();
        string reversed = new string(temp.Reverse().ToArray()) + $"{_counter++}";

        return long.Parse(reversed);
    }
}