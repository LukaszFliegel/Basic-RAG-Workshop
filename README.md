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

## Assumptions

To keep the code short and presentable during online session, some assumptions were made:
- examples will be kept minimal and focused on core concepts to aid understanding
- we won't create interfaces for services and use DI container, but rather simple service classes with single responsibility.
- no error handling/logging/retries etc.