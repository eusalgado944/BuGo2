using Bugo_api.Services;
using Bugo_blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7280");
});

builder.Services.AddScoped<ChamadoService>(sp => 
    new ChamadoService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("API")));

builder.Services.AddScoped<AuthService>(sp => 
    new AuthService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"), sp.GetRequiredService<IJSRuntime>()));

await builder.Build().RunAsync();
