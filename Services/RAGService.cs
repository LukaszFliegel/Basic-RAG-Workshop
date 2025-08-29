namespace Basic_RAG_Workshop.Services;

public interface IRAGService
{
    Task<IAsyncEnumerable<string>> GetRAGResponseAsync(string query, CancellationToken cancellationToken = default);
    Task InitializeAsync(string documentsPath);
    Task<List<VectorSearchResult>> SearchDocumentsAsync(string query, int limit = 5);
}

public class RAGService : IRAGService
{
    private readonly IAIService _aiService;
    private readonly IVectorDatabaseService _vectorDb;
    private readonly IDocumentService _documentService;

    public RAGService(
        IAIService aiService, 
        IVectorDatabaseService vectorDb, 
        IDocumentService documentService)
    {
        _aiService = aiService;
        _vectorDb = vectorDb;
        _documentService = documentService;
    }

    public async Task InitializeAsync(string documentsPath)
    {
        Console.WriteLine("🚀 Initializing RAG system...");
        
        // Initialize vector database
        await _vectorDb.InitializeAsync();
        
        // Process documents and add to vector database
        var documents = await _documentService.ProcessDocumentsAsync(documentsPath);
        
        if (documents.Count > 0)
        {
            await _vectorDb.AddDocumentsAsync(documents);
        }
        else
        {
            Console.WriteLine("⚠️  No documents found to process. Add PDF files to the Documents folder for RAG functionality.");
        }
        
        Console.WriteLine("✅ RAG system initialization complete!");
    }

    public async Task<List<VectorSearchResult>> SearchDocumentsAsync(string query, int limit = 5)
    {
        return await _vectorDb.SearchAsync(query, limit);
    }

    public async Task<IAsyncEnumerable<string>> GetRAGResponseAsync(string query, CancellationToken cancellationToken = default)
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
                I couldn't find relevant information in the knowledge base for your question: "{query}"
                
                Let me provide a general response based on my training:
                """;
        }

        // Get response from AI service with context
        return await _aiService.GetStreamingResponseAsync(contextPrompt, cancellationToken);
    }
}
