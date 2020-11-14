using System.Threading.Tasks;
using Fluxor;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace DungeonBotGame.Client.Store
{
    public class LocalStorageMiddleware : Middleware
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.None
        };

        public LocalStorageMiddleware(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async override Task InitializeAsync(IStore store)
        {
            foreach (var feature in store.Features.Values)
            {
                var stateKey = $"fluxor-{feature.GetName()}";

                var storedValue = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", stateKey);

                if (!string.IsNullOrEmpty(storedValue))
                {
                    var state = JsonConvert.DeserializeObject(storedValue, feature.GetStateType(), _jsonSerializerSettings);
                    feature.RestoreState(state);
                }

                feature.StateChanged += async (sender, args) =>
                {
                    var data = JsonConvert.SerializeObject(feature.GetState(), _jsonSerializerSettings);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", stateKey, data);
                };
            }

            await base.InitializeAsync(store);
        }
    }
}
