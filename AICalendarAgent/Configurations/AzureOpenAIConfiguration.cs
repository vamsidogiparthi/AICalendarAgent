namespace AICalendarAgent.Configurations;

public class AzureOpenAIConfiguration
{
    public static string SectionName = "AzureOpenAIConfiguration";
    public string ApiKey { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
}
