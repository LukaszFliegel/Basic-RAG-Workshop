# Basic RAG Workshop

A simple RAG (Retrieval-Augmented Generation) application using Semantic Kernel and Azure OpenAI Service.

## Prerequisites

- .NET 8.0 SDK
- Azure OpenAI Service resource

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
   dotnet user-secrets set "AzureOpenAI:DeploymentName" "your-deployment-name"
   ```

   ### Option 2: Environment Variables
   ```bash
   $env:AzureOpenAI__Endpoint="https://your-resource-name.openai.azure.com/"
   $env:AzureOpenAI__ApiKey="your-api-key-here"
   $env:AzureOpenAI__DeploymentName="your-deployment-name"
   ```

   ### Option 3: appsettings.json (Not recommended for API keys)
   Edit the `appsettings.json` file and replace the placeholder values.

## Running the Application

```bash
dotnet run
```

## Usage

1. Start the application
2. Enter your prompts when asked
3. The AI response will be streamed back to the console
4. Type 'exit' to quit the application

## Project Structure

- `Program.cs` - Main entry point and dependency injection setup
- `Application.cs` - Main application logic and user interaction
- `Models/AzureOpenAIConfig.cs` - Configuration model for Azure OpenAI settings
- `Services/IAIService.cs` - Service interface for AI operations
- `Services/AIService.cs` - Service implementation using Semantic Kernel
- `appsettings.json` - Configuration file template

## Features

- Interactive console interface
- Streaming responses from Azure OpenAI
- Configuration management with multiple sources
- Dependency injection setup
- Error handling and validation
- Unicode/Emoji support for rich text display

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

The application automatically sets UTF-8 encoding, but terminal/console settings may override this.
