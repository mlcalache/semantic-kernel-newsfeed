using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.IO.Compression;

// Load configuration from secrets
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>()
    .Build();

string? modelId = configuration["SemanticKernel:ModelId"];
string? endpoint = configuration["SemanticKernel:Endpoint"];
string? apiKey = configuration["SemanticKernel:ApiKey"];

var builder = Kernel.CreateBuilder();

// Services

builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

// Plugins

builder.Plugins.AddFromType<NewsPlugin>();



var kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();

var chatMessages = new ChatHistory();

while (true)
{
    Console.Write("Prompt: ");
    chatMessages.AddUserMessage(Console.ReadLine());

    var completion = chatService.GetStreamingChatMessageContentsAsync(
        chatMessages,
        executionSettings: new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        },
        kernel: kernel);

    var fullMessage = "";

    await foreach (var content in completion)
    {
        Console.Write(content.Content);
        fullMessage += content.Content;
    }

    chatMessages.AddAssistantMessage(fullMessage);
    Console.WriteLine();
} 