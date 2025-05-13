# Semantic Kernel News Chatbot

This is a simple console chatbot built with [Microsoft Semantic Kernel (v1.47.0)](https://github.com/microsoft/semantic-kernel), powered by Azure OpenAI and enhanced with a custom plugin (`NewsPlugin`) that fetches real-time news from BBC RSS feeds.

## Features

- Chat with an LLM using Azure OpenAI Chat Completion API
- Streaming responses in real time
- Automatically invokes custom kernel functions
- `NewsPlugin`: fetches news headlines from BBC by category

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Azure OpenAI resource with a deployed Chat model (e.g., `gpt-35-turbo`)
- [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) configured for API keys
- Internet access (to fetch RSS feeds)

---

## Configuration

All sensitive data like Azure endpoints and keys are stored using **.NET user secrets**.
First, you need to initialize the user-secrets:

```bash
dotnet user-secrets init
```

Then you can populate the user-secret variables:

```bash
dotnet user-secrets init
dotnet user-secrets set "SemanticKernel:ModelId" "your-model-id"
dotnet user-secrets set "SemanticKernel:Endpoint" "https://your-resource.openai.azure.com/"
dotnet user-secrets set "SemanticKernel:ApiKey" "your-azure-openai-key"
```

These values are loaded into the app using IConfiguration.

## Project Structure

```bash
/SemanticKernelNewsChat
│
├── Program.cs               # Main entry point
├── NewsPlugin.cs            # Custom plugin to fetch news
├── README.md                # This file
```

## NewsPlugin
Located in NewsPlugin.cs, this kernel function fetches headlines from BBC by category (e.g., world, technology, business).

### Example usage:

> Prompt: What’s happening in world news today?

The LLM will automatically call get_news and return a list of headlines.

## Demo

There is a little demo video available at [YouTube](https://youtu.be/BXkMXEoLJuI).

## Running the App

```bash
dotnet run
````

Then interact with the chatbot in the terminal:

> Prompt: What's the latest in technology?

## Dependencies
Install the following NuGet packages:

```bash
dotnet add package Microsoft.SemanticKernel
dotnet add package Microsoft.SemanticKernel.Connectors.OpenAI
dotnet add package SimpleFeedReader
```

## Powered By
- [Microsoft Semantic Kernel](https://github.com/microsoft/semantic-kernel)
- [Azure OpenAI](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/)
- [BBC RSS Feeds](https://www.bbc.co.uk/news/10628494)
- [SimpleFeedReader](https://github.com/code-kaiju/SimpleFeedReader)

## Author

Created by Matheus de Lara Calache as an educational project and hands-on experiment with AI + .NET.
Based on the [Use Semantic Kernel to build AI Apps and Agents - a simple intro!](https://youtu.be/kCGZPhnTGHM?si=b66unOL6mAZGMZBX) created by [The Code Wolf](https://www.youtube.com/@alexthecodewolf)

Feel free to fork, star, or contribute!