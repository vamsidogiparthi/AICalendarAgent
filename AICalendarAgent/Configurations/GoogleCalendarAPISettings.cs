namespace AICalendarAgent.Configurations;

public class GoogleCalendarAPISettings
{
    public const string SectionName = "GoogleCalendarAPISettings";
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string[] Scopes { get; set; } = [];
    public string RedirectUri { get; set; } = string.Empty;
    public string CalendarId { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string[] Attendees { get; set; } = [];
}
