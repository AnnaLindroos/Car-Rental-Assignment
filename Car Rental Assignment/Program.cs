using Car_Rental.Business.Classes;
using Car_Rental.Data.Classes;
using Car_Rental.Data.Interfaces;
using Car_Rental_Assignment;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// När jag använder mig av nedanstående services så startar inte applikationen, lägger till IData till Collectiondata så fungerar det
//builder.Services.AddSingleton<CollectionData>();
//builder.Services.AddSingleton<BookingProcessor>();

builder.Services.AddSingleton<BookingProcessor>();
builder.Services.AddSingleton<IData, CollectionData>();
builder.Services.AddSingleton<UserInputs>();

// Min gamla version
//builder.Services.AddSingleton(bp => new BookingProcessor(new CollectionData()));

await builder.Build().RunAsync();
