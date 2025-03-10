namespace AICalendarAgent.Services.calendarService;

public interface ICalendarService
{
    Task CreateEventAsync(string title, string description, DateTime startTime, DateTime endTime);
}

public class CalendarService : ICalendarService
{
    public async Task CreateEventAsync(
        string title,
        string description,
        DateTime startTime,
        DateTime endTime
    )
    {
        // Create an event in the calendar
        Console.WriteLine($"Event created: {title} - {description} - {startTime} - {endTime}");
    }
}
