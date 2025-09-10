using Basic_RAG_Workshop.Models;
using Basic_RAG_Workshop.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

// Configure console for Unicode support
Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

// Build configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

// Bind Azure OpenAI configuration
var azureOpenAIConfig = new AzureOpenAIConfig();
configuration.GetSection("AzureOpenAI").Bind(azureOpenAIConfig);

// instantiate services
var aiService = new AIService(azureOpenAIConfig);

// Run the application
Console.WriteLine("🤖 Welcome to Basic RAG Workshop Chat!");
Console.WriteLine("This is a RAG application using Semantic Kernel and Azure OpenAI.");

Console.WriteLine("\nCommands:");
Console.WriteLine("  - Type 'exit' to quit");
Console.WriteLine();

while (true)
{
    // Display user prompt
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("User: ");
    Console.ResetColor();

    var userPrompt = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userPrompt))
    {
        Console.WriteLine("Please enter a valid message.\n");
        continue;
    }

    if (userPrompt.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("👋 Goodbye!");
        break;
    }

    // Display AI response
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Assistant: ");
    Console.ResetColor();

    try
    {
        var responseStream = aiService.GetStreamingResponse(userPrompt);

        await foreach (var chunk in responseStream)
        {
            Console.Write(chunk);
        }

        Console.WriteLine("\n");
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ Error: {ex.Message}\n");
        Console.ResetColor();
    }
}
