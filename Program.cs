using Template;
using Template.Modules.Features;
using Template.Modules.HttpClient;
using Template.Modules.Injection;
// using Template.Services.Auth; // PLANTILLA AUTH: descomentar al activar JwtAuthStateProvider
using Blazored.LocalStorage;
// using Microsoft.AspNetCore.Components.Authorization; // PLANTILLA AUTH: requiere paquete Components.Authorization
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// Punto de entrada de la aplicacion Blazor WebAssembly.
// Desde aqui se registra el componente raiz, la configuracion de HttpClient,
// los servicios de aplicacion, persistencia y dependencias compartidas.
//
// Ejemplo al crear un nuevo modulo:
// 1. Registrar su HttpClient en Modules/HttpClient/HttpClientExtensions.cs.
// 2. Registrar repositorio/servicio en Modules/Injection/InjectionExtensions.cs.
// 3. Consumir el servicio desde la pagina o componente Razor.
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient(builder.Configuration);

builder.Services.AddFeature(builder.Configuration);
builder.Services.AddInjection(builder.Configuration);

builder.Services.AddBlazoredLocalStorage();

// =============================================================================
// PLANTILLA AUTH (desactivada): registra autorizacion y estado de sesion JWT.
// Requiere descomentar paquetes en Template.csproj y App.razor (AuthorizeRouteView).
// =============================================================================
// builder.Services.AddAuthorizationCore();
// builder.Services.AddScoped<JwtAuthStateProvider>();
// builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
//     provider.GetRequiredService<JwtAuthStateProvider>());

await builder.Build().RunAsync();
