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

## Goal

Goal of this workshop is to show basic RAG concept.
Workshop is diveded into two parts:
- implementing simple chatbot using LLM (without any augumented generation)
- then add RAG capabilities using Semantic Kernel and in memory vector store

If you are lost, you can check branches for each step of the workshop:
- main - initial skeleton of the console app with configuration
- milestone/1-basic-chat - basic chatbot using LLM, the first part of the workshop
- milestone/2-vector-db - adding vector store and RAG capabilities, the second and final part of the workshop

## Useful links

[Getting started with Semantic Kernel](https://learn.microsoft.com/en-us/semantic-kernel/get-started/quick-start-guide?pivots=programming-language-csharp)

[Chat completion](https://learn.microsoft.com/en-us/semantic-kernel/concepts/ai-services/chat-completion/?tabs=csharp-AzureOpenAI%2Cpython-AzureOpenAI%2Cjava-AzureOpenAI&pivots=programming-language-csharp)

[Using the In-Memory connector](https://learn.microsoft.com/en-us/semantic-kernel/concepts/vector-store-connectors/out-of-the-box-connectors/inmemory-connector?pivots=programming-language-csharp)

[Text Embedding generation in Semantic Kernel](https://learn.microsoft.com/en-us/semantic-kernel/concepts/ai-services/embedding-generation/?tabs=csharp-AzureOpenAI&pivots=programming-language-csharp)


## Assumptions

To keep the code short and presentable during online session, some assumptions were made:
- examples will be kept minimal and focused on core concepts to aid understanding
- we won't create interfaces for services and use DI container, but rather simple service classes with single responsibility.
- no error handling/logging/retries etc.

## Troubleshooting EPAM's Dial connectivity 

In case you cannot reach **https://ai-proxy.lab.epam.com**
Alternative Plan:
1. Disconnect from the VPN and check the current IP address of ai-proxy.lab.epam.com by opening a command prompt (Windows) or terminal (Mac/Linux) and typing:  
   `nslookup ai-proxy.lab.epam.com`.
2. Edit your hosts file:
   - On Windows: Open Notepad as an administrator, navigate to C:\Windows\System32\drivers\etc\hosts, and add a line with the IP address followed by ai-proxy.lab.epam.com.
   - On Mac/Linux: Open the terminal, type `sudo nano /etc/hosts`, and add a line with the IP address followed by ai-proxy.lab.epam.com.
3. Connect to the VPN and enjoy.
   Caveat: Every time the external IP address changes, you will need to repeat steps 1 and 2. While we do not expect these changes, they may occur, and we will not advertise this change.


