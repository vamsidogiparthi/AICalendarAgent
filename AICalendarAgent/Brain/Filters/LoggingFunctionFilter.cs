namespace AICalendarAgent.Brain.Filters
{
    public sealed class LoggingFunctionFilter(ILogger logger)
        : IFunctionInvocationFilter,
            IAutoFunctionInvocationFilter
    {
        public async Task OnAutoFunctionInvocationAsync(
            AutoFunctionInvocationContext context,
            Func<AutoFunctionInvocationContext, Task> next
        )
        {
            logger.LogInformation(
                "FunctionInvoking - {PluginName}.{FunctionName}",
                context.Function.PluginName,
                context.Function.Name
            );

            await next(context);

            logger.LogInformation(
                "FunctionInvoked - {PluginName}.{FunctionName}",
                context.Function.PluginName,
                context.Function.Name
            );
        }

        public async Task OnFunctionInvocationAsync(
            FunctionInvocationContext context,
            Func<FunctionInvocationContext, Task> next
        )
        {
            logger.LogInformation(
                "FunctionInvoking - {PluginName}.{FunctionName}",
                context.Function.PluginName,
                context.Function.Name
            );

            await next(context);

            logger.LogInformation(
                "FunctionInvoked - {PluginName}.{FunctionName}",
                context.Function.PluginName,
                context.Function.Name
            );
        }
    }
}
