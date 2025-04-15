using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OldPhoneKeypad.Core.Interfaces.ViewModels;
using OldPhoneKeypad.Core.ViewModels;
using OldPhoneKeypad.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IEditViewModel, EditViewModel>();

await builder.Build().RunAsync();
