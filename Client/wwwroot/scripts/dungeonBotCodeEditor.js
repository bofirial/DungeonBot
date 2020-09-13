require(["vs/editor/editor.main"], function () {
    monaco.languages.registerCompletionItemProvider('csharp', {
        triggerCharacters: [
            '.',
            ';',
            '(',
            ' ',
            ',',
            ')'
        ],
        provideCompletionItems: function (model, position) {
            const textUntilPosition = model.getValueInRange({ startLineNumber: 1, startColumn: 1, endLineNumber: position.lineNumber, endColumn: position.column });
            const cursorPosition = textUntilPosition.length;
            return new Promise((resolve, reject) => {
                window.codeCompletionService.invokeMethodAsync('GetCodeCompletionsAsync', model.getValue(), cursorPosition)
                    .then(codeCompletions => {
                    console.log(codeCompletions);
                    resolve({ suggestions: codeCompletions.completionItems });
                });
            });
        }
    });
});
window.initializeMonacoCodeEditor = (codeCompletionService) => {
    window.codeCompletionService = codeCompletionService;
};
export {};
//# sourceMappingURL=dungeonBotCodeEditor.js.map