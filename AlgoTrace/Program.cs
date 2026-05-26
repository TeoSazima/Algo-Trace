using AlgoTrace;
using AlgoTrace.Services; // Tento using pro složku se servisou
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- TENTO ØÁDEK OPRAVÍ TEN UNHANDLED ERROR ---
builder.Services.AddScoped<AlgorithmService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
