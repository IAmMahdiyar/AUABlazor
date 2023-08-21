using AUA.ProjectName.WebUi;
using AUA.ProjectName.WebUi.AppConfiguration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configuration();

await builder.Build().RunAsync();
