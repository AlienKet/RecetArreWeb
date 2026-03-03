using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RecetArreWeb;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//configurar HttpClient con handler de Autorización
//builder.Services.AddScoped<AuthorizationMessageHandler>();

//builder.Services.AddScoped(sp =>
//{
//    var handler = sp.GetRequiredService<AuthorizationMessageHandler>();
//    handler.InnerHandler=new HttpClientHandler();

//    var httpClient = new HttpClient(handler)
//    {
//        BaseAddress = new Uri("https://localhost:7019/")
//    };
//});


await builder.Build().RunAsync();
