using Basic_RAG_Workshop;
using Basic_RAG_Workshop.Models;
using Basic_RAG_Workshop.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
configuration.GetSection(AzureOpenAIConfig.SectionName).Bind(azureOpenAIConfig);

// Configure services
var services = new ServiceCollection();
services.AddSingleton(azureOpenAIConfig);
services.AddSingleton<IAIService, AIService>();
services.AddSingleton<IDocumentService, DocumentService>();
services.AddSingleton<IVectorDatabaseService, VectorDatabaseService>();
services.AddSingleton<IRAGService, RAGService>();
services.AddTransient<Application>();

var serviceProvider = services.BuildServiceProvider();

// Run the application
try
{
    var app = serviceProvider.GetRequiredService<Application>();
    await app.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"Application error: {ex.Message}");
    return 1;
}
