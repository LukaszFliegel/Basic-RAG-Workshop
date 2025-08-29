using Microsoft.Extensions.VectorData;

namespace Basic_RAG_Workshop.Models;

public class DocumentRecord
{
    [VectorStoreKey]
    public string Id { get; set; } = string.Empty;

    [VectorStoreData]
    public string Content { get; set; } = string.Empty;

    [VectorStoreData]
    public string SourceFile { get; set; } = string.Empty;

    //[VectorStoreData]
    //public int ChunkIndex { get; set; }

    //[VectorStoreData]
    //public string Description { get; set; } = string.Empty;

    [VectorStoreVector(1536)]
    public ReadOnlyMemory<float>? DescriptionEmbedding { get; set; }
}


public class DocumentChunk
{
    public string Id { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string SourceFile { get; set; } = string.Empty;
    //public int ChunkIndex { get; set; }
    //public Dictionary<string, object> Metadata { get; set; } = new();
}