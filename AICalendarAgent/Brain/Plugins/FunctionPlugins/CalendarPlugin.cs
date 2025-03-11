using System.ComponentModel;
using AICalendarAgent.Services;
using Google.Apis.Calendar.v3.Data;

namespace AICalendarAgent.Brain.Plugins.FunctionPlugins;

public class CalendarPlugin(ICalendarService calendarService)
{
    private readonly ICalendarService _calendarService = calendarService;

    [KernelFunction("create_google_calendar_event")]
    [Description("Create an event in the Google Calendar.")]
    public async Task CreateGoogleCalendarEvent(
        string title,
        string description,
        DateTimeOffset startTime,
        DateTimeOffset endTime
    )
    {
        await _calendarService.CreateEventAsync(title, description, startTime, endTime);
    }

    [KernelFunction("get_my_calendar_list")]
    [Description("Get the list of calendars.")]
    public async Task<List<CalendarListEntry>> GetMyCalendarList()
    {
        return await _calendarService.GetMyCalendarList();
    }
}
