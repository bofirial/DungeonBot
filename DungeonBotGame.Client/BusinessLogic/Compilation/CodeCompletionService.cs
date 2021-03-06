﻿using System;
using System.Collections.Immutable;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DungeonBotGame.Models.Api;
using DungeonBotGame.Models.ViewModels;
using Microsoft.JSInterop;

namespace DungeonBotGame.Client.BusinessLogic.Compilation
{
    public interface ICodeCompletionService
    {
        Task InitializeCodeEditorAsync(DungeonBotViewModel dungeonBotViewModel);

        Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(string sourceCode, int currentPosition);
    }

    public class CodeCompletionService : ICodeCompletionService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        private bool _isInitialized;

        private DungeonBotViewModel? _dungeonBot;

        public CodeCompletionService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public async Task InitializeCodeEditorAsync(DungeonBotViewModel dungeonBot)
        {
            _dungeonBot = dungeonBot;

            if (!_isInitialized)
            {
                _isInitialized = true;

                await _jsRuntime.InvokeVoidAsync("initializeMonacoCodeEditor", DotNetObjectReference.Create(this));
            }
        }

        [JSInvokable]
        public async Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(string sourceCode, int currentPosition)
        {
            if (_dungeonBot == null)
            {
                throw new InvalidOperationException($"DungeonBot must not be null to look up Code Completions.");
            }
            var actionModuleFile = _dungeonBot.ActionModuleFiles[0];

            var response = await _httpClient.PostAsJsonAsync($"api/CodeCompletions", new CodeCompletionPostRequestModel(
                _dungeonBot.ActionModuleFiles[0].FileName,
                currentPosition,
                _dungeonBot with { ActionModuleFiles = ImmutableList.Create(actionModuleFile with { Content = sourceCode }), ActionModuleContext = null }));

            var responseModel = await response.Content.ReadFromJsonAsync<CodeCompletionPostResponseModel>();

            if (responseModel == null)
            {
                throw new InvalidOperationException("Failure getting Code Completions.");
            }

            return responseModel;
        }
    }
}
