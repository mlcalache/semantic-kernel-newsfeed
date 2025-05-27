using Microsoft.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

// Load configuration from secrets
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>()
    .Build();

string? modelId = configuration["SemanticKernel:ModelId"];
string? endpoint = configuration["SemanticKernel:Endpoint"];
string? apiKey = configuration["SemanticKernel:ApiKey"];

var builder = Kernel.CreateBuilder();

builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

builder.Plugins.AddFromType<BBCNewsPlugin>();

var kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();

var chatMessages = new ChatHistory();

while (true)
{

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("Some available categories are: ");
    Console.WriteLine("Top Stories");
    Console.WriteLine("World");
    Console.WriteLine("UK");
    Console.WriteLine("Business");
    Console.WriteLine("Politics");
    Console.WriteLine("Health");
    Console.WriteLine("Education & Family");
    Console.WriteLine("Science & Environment");
    Console.WriteLine("Technology");
    Console.WriteLine("Entertainment & Arts");

    Console.ForegroundColor = ConsoleColor.White;
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

    Console.ForegroundColor = ConsoleColor.Magenta;
    await foreach (var content in completion)
    {
        Console.Write(content.Content);
        fullMessage += content.Content;
    }

    chatMessages.AddAssistantMessage(fullMessage);
    Console.WriteLine();
}