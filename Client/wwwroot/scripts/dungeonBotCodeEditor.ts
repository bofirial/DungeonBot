export { }

declare const require: any;
declare const monaco: any;
declare global {
    interface Window {
        initializeMonacoCodeEditor: any;
        codeCompletionService: any;
    }
}

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

            window.codeCompletionService.invokeMethodAsync('GetCodeCompletionsAsync', model.getValue(), cursorPosition)
                .then(codeCompletions => console.log(codeCompletions));

            return new Promise((resolve, reject) => {
                resolve([]);
            });
        }
    });
});

window.initializeMonacoCodeEditor = (codeCompletionService) => {
    window.codeCompletionService = codeCompletionService;
};