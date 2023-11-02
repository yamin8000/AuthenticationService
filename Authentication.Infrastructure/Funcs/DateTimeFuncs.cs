using Microsoft.Extensions.Configuration;

namespace Authentication.Infrastructure.Funcs;

public class DateTimeFuncs
{
    public static DateTime Now()
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(configuration.GetSection("SystemTimeZoneId")["Default"] ?? string.Empty);
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo); 
    }
}