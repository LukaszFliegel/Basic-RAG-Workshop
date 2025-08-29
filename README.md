# Basic RAG Workshop

A complete RAG (Retrieval-Augmented Generation) application using Semantic Kernel's modern Vector Store connectors, Azure OpenAI Service, and in-memory vector database.

## Prerequisites

- .NET 8.0 SDK
- Azure OpenAI Service resource with:
  - Chat completion model deployment (e.g., GPT-4)
  - Text embedding model deployment (e.g., text-embedding-3-small)

## Key Technologies

- **Semantic Kernel Vector Store**: Uses the modern `InMemoryVectorStore` connector
- **Azure OpenAI**: For both chat completion and text embeddings
- **iText7**: For PDF text extraction
- **Dependency Injection**: Clean architecture with proper service registration

## Setup

1. **Clone the repository** (if not already done)

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Configure Azure OpenAI Settings**

   You have several options to configure your Azure OpenAI settings:

   ### Option 1: User Secrets (Recommended for development)
   ```bash
   dotnet user-secrets set "AzureOpenAI:Endpoint" "https://your-resource-name.openai.azure.com/"
   dotnet user-secrets set "AzureOpenAI:ApiKey" "your-api-key-here"
   dotnet user-secrets set "AzureOpenAI:DeploymentName" "your-gpt-deployment-name"
   dotnet user-secrets set "AzureOpenAI:EmbeddingDeploymentName" "your-embedding-deployment-name"
   ```

   ### Option 2: Environment Variables
   ```bash
   $env:AzureOpenAI__Endpoint="https://your-resource-name.openai.azure.com/"
   $env:AzureOpenAI__ApiKey="your-api-key-here"
   $env:AzureOpenAI__DeploymentName="your-gpt-deployment-name"
   $env:AzureOpenAI__EmbeddingDeploymentName="your-embedding-deployment-name"
   ```

   ### Option 3: appsettings.json (Not recommended for API keys)
   Edit the `appsettings.json` file and replace the placeholder values.

4. **Add PDF Documents (Optional)**
   
   Place PDF files in the `Documents` folder to enable RAG functionality. The application will automatically process these documents at startup.

## Running the Application

```bash
dotnet run
```

## Usage

The application supports multiple interaction modes:

### RAG-Enhanced Chat (Default)
Simply type your message to chat with AI enhanced by your document knowledge base:
```
User: What is mentioned about Azure in the documents?
```

### Regular Chat (Without RAG)
Use `/chat` command for standard AI conversation:
```
User: /chat Tell me about machine learning
```

### Document Search
Use `/search` command to search your documents directly:
```
User: /search Azure OpenAI
```

### Other Commands
- Type `clear` to clear chat history
- Type `exit` to quit the application

## Project Structure

- `Program.cs` - Main entry point and dependency injection setup
- `Application.cs` - Main application logic and user interaction
- `Models/AzureOpenAIConfig.cs` - Configuration model for Azure OpenAI settings
- `Services/`
  - `IAIService.cs` & `AIService.cs` - Chat completion service
  - `IDocumentService.cs` & `DocumentService.cs` - PDF processing service
  - `IVectorDatabaseService.cs` & `VectorDatabaseService.cs` - Vector database operations
  - `IRAGService.cs` & `RAGService.cs` - RAG orchestration service
- `Documents/` - Folder for PDF documents to be processed
- `appsettings.json` - Configuration file template

## Features

### Core Features
- Interactive console interface with colored output
- Streaming responses from Azure OpenAI
- Chat history preservation across prompts
- Configuration management with multiple sources
- Dependency injection setup
- Unicode/Emoji support for rich text display

### RAG Features
- **PDF Document Processing**: Automatically extracts text from PDF files using iText7
- **Text Chunking**: Intelligently splits documents into overlapping chunks
- **Vector Embeddings**: Creates embeddings using Azure OpenAI embedding models
- **Semantic Kernel Vector Store**: Uses the modern `InMemoryVectorStore` connector for fast similarity search
- **Context-Aware Responses**: LLM responses enhanced with relevant document context
- **Multiple Interaction Modes**: RAG chat, regular chat, and document search

### Document Processing Pipeline
1. **PDF Text Extraction**: Uses iText7 library to extract text from PDF files
2. **Text Chunking**: Splits text into 1000-character chunks with 200-character overlap
3. **Embedding Generation**: Creates vector embeddings for each chunk using Azure OpenAI
4. **Vector Storage**: Stores embeddings using Semantic Kernel's `InMemoryVectorStore`
5. **Similarity Search**: Finds relevant chunks using cosine similarity
6. **Context Enhancement**: Provides relevant context to the LLM for better responses

## Workshop Learning Path

This application demonstrates key RAG concepts:

1. **Basic Chat Interface** - Foundation for AI interaction
2. **Configuration Management** - Secure API key handling
3. **Document Processing** - PDF text extraction and chunking
4. **Vector Embeddings** - Converting text to numerical representations
5. **Vector Store Operations** - Using Semantic Kernel's modern vector store connectors
6. **Similarity Search** - Finding relevant information using vector similarity
7. **Context Integration** - Enhancing LLM responses with retrieved information

## Technical Architecture

### Vector Store Implementation
The application uses Semantic Kernel's `InMemoryVectorStore` which provides:
- **Type-safe Record Models**: `DocumentRecord` class with proper attributes
- **Automatic Embedding Management**: Handles vector operations seamlessly  
- **Flexible Search Options**: Configurable similarity search with multiple distance functions
- **Clean API**: Modern async/await patterns throughout

### Key Classes
- `DocumentRecord`: Vector store record model with proper attributes
- `VectorDatabaseService`: Manages vector operations using `InMemoryVectorStore`
- `DocumentService`: Handles PDF processing and text chunking
- `RAGService`: Orchestrates the complete RAG pipeline
- `AIService`: Manages chat completion with history

## Troubleshooting

### Unicode Characters Display as `??`

If you see `??` instead of emojis or special Unicode characters, try these solutions:

1. **Windows Terminal (Recommended)**: Use Windows Terminal instead of the traditional Command Prompt or PowerShell ISE for better Unicode support.

2. **PowerShell Font**: Ensure your PowerShell console is using a font that supports Unicode characters:
   - Right-click the PowerShell title bar → Properties → Font
   - Select a font like "Consolas", "Cascadia Code", or "Segoe UI"

3. **Code Page**: If still having issues, you can manually set the console code page:
   ```powershell
   chcp 65001
   dotnet run
   ```

4. **Visual Studio Code**: Run the application through VS Code's integrated terminal for the best Unicode support.

### No Documents Found
If you see "No documents found to process":
- Add PDF files to the `Documents` folder
- Ensure PDF files are text-based (not scanned images)
- Check file permissions

### Configuration Errors
If you see configuration incomplete errors:
- Verify all required settings are provided
- Check user secrets with `dotnet user-secrets list`
- Ensure deployment names match your Azure OpenAI resource

The application automatically sets UTF-8 encoding, but terminal/console settings may override this.
