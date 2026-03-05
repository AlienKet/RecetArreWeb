using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RecetArreWeb;
using RecetArreWeb.Auth;
using RecetArreWeb.Handlers;
using RecetArreWeb.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//configurar HttpClient con handler de Autorización
builder.Services.AddScoped<AuthorizationMessageHandler>();

builder.Services.AddScoped<HttpClient>(sp =>
{
    var handler = sp.GetRequiredService<AuthorizationMessageHandler>();
    handler.InnerHandler = new HttpClientHandler();

   return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7019/")
    };
});

//Registrar servicios 

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//TODO: Todos los demas servicios ejemplo: ICategoriaService,IIngredienteService, IRecetaService, etc

//Configurar autenticacion

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


await builder.Build().RunAsync();
