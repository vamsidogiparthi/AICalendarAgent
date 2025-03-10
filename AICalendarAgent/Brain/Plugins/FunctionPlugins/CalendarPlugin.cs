using System.ComponentModel;
using AICalendarAgent.Services.calendarService;

namespace AICalendarAgent.Brain.Plugins.FunctionPlugins;

public class CalendarPlugin(ICalendarService calendarService)
{
    [KernelFunction("create_google_calendar_event")]
    [Description("Create an event in the Google Calendar.")]
    public async Task CreateGoogleCalendarEvent(
        string title,
        string description,
        DateTime startTime,
        DateTime endTime
    )
    {
        await calendarService.CreateEventAsync(title, description, startTime, endTime);
    }
}
