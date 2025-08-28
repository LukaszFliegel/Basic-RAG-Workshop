using Basic_RAG_Workshop.Services;

namespace Basic_RAG_Workshop;

public class Application
{
    private readonly IAIService _aiService;

    public Application(IAIService aiService)
    {
        _aiService = aiService;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("🤖 Welcome to Basic RAG Workshop Chat!");
        Console.WriteLine("This is a simple RAG application using Semantic Kernel and Azure OpenAI.");
        Console.WriteLine("Commands:");
        Console.WriteLine("  - Type your message to chat with the AI");
        Console.WriteLine("  - Type 'exit' to quit");
        Console.WriteLine("  - Type 'clear' to clear chat history");
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

            if (userPrompt.Equals("clear", StringComparison.OrdinalIgnoreCase))
            {
                _aiService.ClearChatHistory();
                Console.WriteLine("🗑️ Chat history cleared.\n");
                continue;
            }

            // Display AI response
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Assistant: ");
            Console.ResetColor();

            try
            {
                var responseStream = await _aiService.GetStreamingResponseAsync(userPrompt);
                
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
    }
}
