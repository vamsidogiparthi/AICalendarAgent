using AICalendarAgent.Configurations;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Extensions.Options;

namespace AICalendarAgent.Services;

public interface ICalendarService
{
    Task CreateEventAsync(
        string title,
        string description,
        DateTimeOffset startTime,
        DateTimeOffset endTime
    );

    Task<List<CalendarListEntry>> GetMyCalendarList();
}

public class CalendarService(
    IOptions<GoogleCalendarAPISettings> options,
    ILogger<CalendarService> logger
) : ICalendarService
{
    public async Task<List<CalendarListEntry>> GetMyCalendarList()
    {
        var googleCredential = GetGoogleCredential();
        var service = new Google.Apis.Calendar.v3.CalendarService(
            new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = googleCredential,
                ApplicationName = options.Value.ApplicationName,
            }
        );

        var request = service.CalendarList.List();
        var calendarList = await request.ExecuteAsync();

        return [.. calendarList.Items];
    }

    public async Task CreateEventAsync(
        string title,
        string description,
        DateTimeOffset startTime,
        DateTimeOffset endTime
    )
    {
        var googleCredential = GetGoogleCredential();

        // authentication flow for the google calendar using user auth token
        // var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        //     new ClientSecrets
        //     {
        //         ClientId = options.Value.ClientId,
        //         ClientSecret = options.Value.ClientSecret,
        //     },
        //     options.Value.Scopes,
        //     options.Value.User,
        //     CancellationToken.None
        // );

        var service = new Google.Apis.Calendar.v3.CalendarService(
            new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = googleCredential,
                ApplicationName = options.Value.ApplicationName,
            }
        );

        var newEvent = new Event
        {
            Summary = title,
            Description = description,
            Start = new EventDateTime
            {
                DateTimeDateTimeOffset = startTime,
                TimeZone = "America/Los_Angeles",
            },
            End = new EventDateTime
            {
                DateTimeDateTimeOffset = endTime,
                TimeZone = "America/Los_Angeles",
            },
        };

        // Create an event in the calendar
        var request = service.Events.Insert(newEvent, options.Value.CalendarId);
        var createdEvent = await request.ExecuteAsync();

        logger.LogInformation(
            $"Event created: {title} - {description} - {startTime} - {endTime} - {createdEvent.HtmlLink}"
        );
    }

    protected GoogleCredential GetGoogleCredential()
    {
        GoogleCredential googleCredential;
        using var stream = new FileStream(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "aicalendaragent-453303-884a42dd2c74.json"
            ),
            FileMode.Open,
            FileAccess.Read
        );
        googleCredential = GoogleCredential.FromStream(stream).CreateScoped(options.Value.Scopes);
        return googleCredential;
    }
}
