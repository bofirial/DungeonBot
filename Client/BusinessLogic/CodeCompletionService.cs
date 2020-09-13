﻿using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DungeonBot.Models;
using Microsoft.JSInterop;

namespace DungeonBot.Client.BusinessLogic
{
    public class CodeCompletionService : ICodeCompletionService
    {
        private const string FILE_NAME = "DungeonBot.cs";
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public CodeCompletionService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public async Task InitializeCodeEditorAsync()
        {
            await _jsRuntime.InvokeVoidAsync("initializeMonacoCodeEditor", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public async Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(string sourceCode, int currentPosition)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/CodeCompletions", new CodeCompletionPostRequestModel()
            {
                CombatLogic = new CombatLogic()
                {
                    SourceCodeFiles = new System.Collections.Generic.List<SourceCodeFile>()
                    {
                        new SourceCodeFile()
                        {
                            FileName = FILE_NAME,
                            Content = sourceCode
                        }
                    }
                },
                TargetFileName = FILE_NAME,
                TargetFilePosition = currentPosition
            });

            return await response.Content.ReadFromJsonAsync<CodeCompletionPostResponseModel>();
        }
    }
}
