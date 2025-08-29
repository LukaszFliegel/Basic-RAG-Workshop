using Microsoft.Extensions.VectorData;
using System.Text.Json.Serialization;

namespace Basic_RAG_Workshop.Models;

public class DocumentRecord
{
    [VectorStoreRecordKey]
    public string Id { get; set; } = string.Empty;

    [VectorStoreRecordData]
    public string Content { get; set; } = string.Empty;

    [VectorStoreRecordData]
    public string SourceFile { get; set; } = string.Empty;

    [VectorStoreRecordData]
    public int ChunkIndex { get; set; }

    [VectorStoreRecordData]
    public string Description { get; set; } = string.Empty;

    [VectorStoreRecordVector(1536, DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float>? Vector { get; set; }
}
