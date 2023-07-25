using BlazorApp1;
using BlazorApp1.Gateways;
using Membership.Blazor;
using Membership.Blazor.Options;
using Membership.Shared.MessageLocalizer;
using Membership.Shared.Validators;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMembershipBlazorServices(
    userEnpoints =>
        builder.Configuration.GetSection(UserEndpointsOptions.SectionKey).Bind(userEnpoints),
    appOptions =>
        builder.Configuration.GetSection(AppOptions.SectionKey).Bind(appOptions))
    .AddMembershipMessageLocalizer()
    .AddMembershipValidatorsServices();


builder.Services.AddHttpClient<WebApiClient>(httpclient =>
    httpclient.BaseAddress = new Uri("https://localhost:7033"))
    .AddMembershipBearerTokenHandler();

await builder.Build().RunAsync();
