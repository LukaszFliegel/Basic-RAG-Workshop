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
