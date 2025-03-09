namespace AICalendarAgent.Configurations;

public class OpenAIConfiguration
{
    public static string SectionName = "OpenAIConfiguration";
    public string ApiKey { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
}
