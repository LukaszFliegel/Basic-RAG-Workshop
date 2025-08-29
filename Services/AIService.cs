using Basic_RAG_Workshop.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Basic_RAG_Workshop.Services;

public class AIService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chatCompletionService;
    private readonly ChatHistory _chatHistory;

    public AIService(AzureOpenAIConfig config)
    {
        var builder = Kernel.CreateBuilder();
        
        builder.AddAzureOpenAIChatCompletion(
            deploymentName: config.DeploymentName,
            endpoint: config.Endpoint,
            apiKey: config.ApiKey);

        _kernel = builder.Build();
        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
        _chatHistory = new ChatHistory();

        _chatHistory.AddSystemMessage("You are a helpful assistant.");
    }

    public async IAsyncEnumerable<string> GetStreamingResponse(string prompt)
    {
        // Add user message to chat history
        _chatHistory.AddUserMessage(prompt);

        // Get streaming response from the chat completion service
        var response = _chatCompletionService.GetStreamingChatMessageContentsAsync(_chatHistory);

        // Stream the response while collecting it for chat history
        var fullResponse = new System.Text.StringBuilder();

        await foreach (var content in response)
        {
            if (!string.IsNullOrEmpty(content.Content))
            {
                fullResponse.Append(content.Content);
                yield return content.Content;
            }
        }

        // Add the complete assistant response to chat history
        _chatHistory.AddAssistantMessage(fullResponse.ToString());
    }
}
