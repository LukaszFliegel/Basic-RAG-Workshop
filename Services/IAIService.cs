namespace Basic_RAG_Workshop.Services;

public interface IAIService
{
    Task<IAsyncEnumerable<string>> GetStreamingResponseAsync(string prompt, CancellationToken cancellationToken = default);
    void ClearChatHistory();
}
