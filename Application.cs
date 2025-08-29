using Basic_RAG_Workshop.Services;

namespace Basic_RAG_Workshop;

public class Application
{
    private readonly IAIService _aiService;
    private readonly IRAGService _ragService;

    public Application(IAIService aiService, IRAGService ragService)
    {
        _aiService = aiService;
        _ragService = ragService;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("🤖 Welcome to Basic RAG Workshop Chat!");
        Console.WriteLine("This is a RAG application using Semantic Kernel and Azure OpenAI.");
        
        // Initialize RAG system
        var documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
        await _ragService.InitializeAsync(documentsPath);
        
        Console.WriteLine("\nCommands:");
        Console.WriteLine("  - Type your message to chat with the AI (with RAG)");
        Console.WriteLine("  - Type '/chat <message>' for chat without RAG");
        Console.WriteLine("  - Type '/search <query>' to search documents only");
        Console.WriteLine("  - Type 'exit' to quit");
        Console.WriteLine("  - Type 'clear' to clear chat history");
        Console.WriteLine();

        while (true)
        {
            // Display user prompt
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("User: ");
            Console.ResetColor();
            
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Please enter a valid message.\n");
                continue;
            }

            if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("👋 Goodbye!");
                break;
            }

            if (userInput.Equals("clear", StringComparison.OrdinalIgnoreCase))
            {
                _aiService.ClearChatHistory();
                Console.WriteLine("🗑️ Chat history cleared.\n");
                continue;
            }

            try
            {
                if (userInput.StartsWith("/chat ", StringComparison.OrdinalIgnoreCase))
                {
                    // Regular chat without RAG
                    var chatMessage = userInput.Substring(6);
                    await HandleRegularChatAsync(chatMessage);
                }
                else if (userInput.StartsWith("/search ", StringComparison.OrdinalIgnoreCase))
                {
                    // Search documents only
                    var searchQuery = userInput.Substring(8);
                    await HandleDocumentSearchAsync(searchQuery);
                }
                else
                {
                    // RAG-enhanced chat (default)
                    await HandleRAGChatAsync(userInput);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Error: {ex.Message}\n");
                Console.ResetColor();
            }
        }
    }

    private async Task HandleRegularChatAsync(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Assistant: ");
        Console.ResetColor();

        var responseStream = await _aiService.GetStreamingResponseAsync(message);
        await foreach (var chunk in responseStream)
        {
            Console.Write(chunk);
        }
        Console.WriteLine("\n");
    }

    private async Task HandleRAGChatAsync(string query)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Assistant: ");
        Console.ResetColor();

        var responseStream = await _ragService.GetRAGResponseAsync(query);
        await foreach (var chunk in responseStream)
        {
            Console.Write(chunk);
        }
        Console.WriteLine("\n");
    }

    private async Task HandleDocumentSearchAsync(string query)
    {
        Console.WriteLine("🔍 Searching documents...\n");
        
        var searchResults = await _ragService.SearchDocumentsAsync(query);
        
        if (searchResults.Count == 0)
        {
            Console.WriteLine("No relevant documents found.\n");
            return;
        }

        Console.WriteLine($"Found {searchResults.Count} relevant document(s):\n");
        
        for (int i = 0; i < searchResults.Count; i++)
        {
            var result = searchResults[i];
            Console.WriteLine($"📄 Result {i + 1} (Relevance: {result.Score:F2})");
            Console.WriteLine($"Source: {result.Record.SourceFile}");
            Console.WriteLine($"Content: {TruncateText(result.Record.Content, 200)}");
            Console.WriteLine(new string('-', 50));
        }
        Console.WriteLine();
    }

    private static string TruncateText(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;
        
        return text.Substring(0, maxLength) + "...";
    }
}
