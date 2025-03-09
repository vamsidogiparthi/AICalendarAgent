using AICalendarAgent.Configurations;
using Microsoft.Extensions.Options;

namespace AICalendarAgent.Brain;

public interface IBrain
{
    Task Run(Kernel kernel);
}

public class Brain(IServiceProvider serviceProvider, ILogger<Brain> logger) : IBrain
{
    public async Task Run(Kernel kernel)
    {
        var chatCompletionService = serviceProvider.GetRequiredService<IChatCompletionService>();

        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
        };

        var history = new ChatHistory();
        history.AddUserMessage("what is the local timezone Iana name?");

        // Get the response from the AI
        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            executionSettings: openAIPromptExecutionSettings,
            kernel: kernel
        );

        logger.LogInformation("AI Response: {Response}", result);
    }
}
