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
        //history.AddUserMessage("what is the local timezone Iana name?");

        // Get the response from the AI
        // var result = await chatCompletionService.GetChatMessageContentAsync(
        //     history,
        //     executionSettings: openAIPromptExecutionSettings,
        //     kernel: kernel
        // );

        // logger.LogInformation("AI Response: {Response}", result.Content ?? "No response");
        // history.AddMessage(result.Role, result.Content ?? "No response");

        // history.AddUserMessage("what is the local timezone Iana id?");

        // result = await chatCompletionService.GetChatMessageContentAsync(
        //     history,
        //     executionSettings: openAIPromptExecutionSettings,
        //     kernel: kernel
        // );

        // logger.LogInformation("AI Response: {Response}", result.Content ?? "No response");

        // history.AddMessage(result.Role, result.Content ?? "No response");

        // history.AddUserMessage("what is the current time in India?");
        // result = await chatCompletionService.GetChatMessageContentAsync(
        //     history,
        //     executionSettings: openAIPromptExecutionSettings,
        //     kernel: kernel
        // );

        // logger.LogInformation("AI Response: {Response}", result.Content ?? "No response");

        // history.AddMessage(result.Role, result.Content ?? "No response");
        // history.AddUserMessage(
        //     "what is the datetime in India. When the equivalent date time in EST is 03/10/2025 4PM?"
        // );

        // result = await chatCompletionService.GetChatMessageContentAsync(
        //     history,
        //     executionSettings: openAIPromptExecutionSettings,
        //     kernel: kernel
        // );

        // logger.LogInformation("AI Response: {Response}", result.Content ?? "No response");

        history.AddUserMessage(
            "Can you add an event in my Google Calendar with the title 'Meeting with John' "
                + "starting at 3 PM and ending at 4 PM EST on March 14th 2025?"
        );

        //history.AddUserMessage("Can you get me list of my calendars?");

        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            executionSettings: openAIPromptExecutionSettings,
            kernel: kernel
        );

        logger.LogInformation("AI Response: {Response}", result.Content ?? "No response");
        history.AddMessage(result.Role, result.Content ?? "No response");
    }
}
