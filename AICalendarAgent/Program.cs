﻿using AICalendarAgent.Brain;
using AICalendarAgent.Brain.Filters;
using AICalendarAgent.Brain.Plugins.FunctionPlugins;
using AICalendarAgent.Configurations;

// See https://aka.ms/new-console-template for more information


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .AddCommandLine(args)
    .Build();

var builder = Kernel.CreateBuilder();

builder.Services.AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Trace));
builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddOptions();
builder.Services.Configure<OpenAIConfiguration>(
    configuration.GetSection(OpenAIConfiguration.SectionName)
);
builder.Services.AddTransient<IBrain, Brain>();

var openAIConfiguration =
    configuration.GetSection(OpenAIConfiguration.SectionName).Get<OpenAIConfiguration>()
    ?? throw new Exception("OpenAI configuration is missing");

builder.AddAzureOpenAIChatCompletion(
    openAIConfiguration.ModelId,
    openAIConfiguration.Endpoint,
    openAIConfiguration.ApiKey
);

var kernel = builder.Build();
kernel.Plugins.AddFromType<TimePlugin>();
builder.Services.AddSingleton<IFunctionInvocationFilter, LoggingFunctionFilter>();
builder.Services.AddSingleton<IAutoFunctionInvocationFilter, LoggingFunctionFilter>();

var logger = kernel.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Starting the AI Calendar Agent");
kernel.GetRequiredService<IBrain>().Run(kernel).Wait();
