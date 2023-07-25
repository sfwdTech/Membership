namespace Membership.Blazor.Services;
internal class OAuthStateService : IOAuthStateService
{
    readonly IJSRuntime _jSRuntime;

    public OAuthStateService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        T value = default;
        try
        {
            var savedValue = await _jSRuntime.InvokeAsync<string>("sessionStorage.getItem", key);
            if(savedValue != null)
            {
                var valueToDeserialize = Encoding.UTF8.GetString(Convert.FromBase64String(savedValue));
                value = JsonSerializer.Deserialize<T>(valueToDeserialize);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return value;
    }

    public async Task RemoveAsync(string key) => 
        await _jSRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);

    public async Task SetAsync<T>(string key, T value)
    {
        try
        {
            var serializedValue = JsonSerializer.Serialize(value);
            var valueToSave = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedValue));

            await _jSRuntime.InvokeVoidAsync("sessionStorage.setItem", key, valueToSave);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
