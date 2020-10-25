﻿using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DungeonBotGame.Models.Api;
using DungeonBotGame.Models.ViewModels;
using Microsoft.JSInterop;

namespace DungeonBotGame.Client.BusinessLogic
{
    public class CodeCompletionService : ICodeCompletionService
    {
        private const string LIBRARY_NAME = "DungeonBot001";
        private const string FILE_NAME = "DungeonBotGame.cs";
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        private DungeonBotViewModel? _dungeonBot;

        public CodeCompletionService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public async Task InitializeCodeEditorAsync(DungeonBotViewModel dungeonBot)
        {
            _dungeonBot = dungeonBot;

            await _jsRuntime.InvokeVoidAsync("initializeMonacoCodeEditor", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public async Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(string sourceCode, int currentPosition)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/CodeCompletions", new CodeCompletionPostRequestModel()
            {
                ActionModuleLibrary = new ActionModuleLibraryViewModel(LIBRARY_NAME, System.Array.Empty<byte>(), new ActionModuleFileViewModel(FILE_NAME, sourceCode)),
                TargetFileName = FILE_NAME,
                TargetFilePosition = currentPosition,
                DungeonBot = _dungeonBot
            });

            var responseModel = await response.Content.ReadFromJsonAsync<CodeCompletionPostResponseModel>();

            if (responseModel == null)
            {
                throw new System.Exception("Unable to get Code Completion results");
            }

            return responseModel;
        }
    }
}