namespace Basic_RAG_Workshop.Services;

public class RAGService
{
    private readonly AIService _aiService;
    private readonly VectorDatabaseService _vectorDb;

    public RAGService(
        AIService aiService, 
        VectorDatabaseService vectorDb)
    {
        _aiService = aiService;
        _vectorDb = vectorDb;
    }

    public async Task<List<VectorSearchResult>> SearchDocumentsAsync(string query, int limit = 5)
    {
        return await _vectorDb.SearchAsync(query, limit);
    }

    public async Task<IAsyncEnumerable<string>> GetRAGResponseAsync(string query)
    {
        // Search for relevant documents
        var searchResults = await _vectorDb.SearchAsync(query, limit: 3);
        
        string contextPrompt;
        
        if (searchResults.Count > 0)
        {
            // Build context from search results
            var context = string.Join("\n\n", searchResults.Select(r =>
                $"[Source: {r.Record.SourceFile}]\n{r.Record.Content}"));
            
            contextPrompt = $"""
                Based on the following context from the knowledge base, please answer the user's question. 
                If the context doesn't contain enough information to answer the question, please say so and provide a general response.

                Context:
                {context}

                User Question: {query}

                Please provide a helpful answer based on the context above:
                """;
        }
        else
        {
            contextPrompt = $"""
                No relevant information was found in the knowledge base for the user's question: "{query}"
                
                Please provide a helpful general response based on your training data. 
                Let the user know that you don't have specific information about their question in the knowledge base, 
                but offer to help with general information on the topic if possible.
                
                User Question: {query}
                """;
        }

        return _aiService.GetStreamingResponse(contextPrompt);
    }
}
