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

// N�r jag anv�nder mig av nedanst�ende services s� startar inte applikationen, l�gger till IData till Collectiondata s� fungerar det
//builder.Services.AddSingleton<CollectionData>();
//builder.Services.AddSingleton<BookingProcessor>();


builder.Services.AddSingleton<IData, CollectionData>();
builder.Services.AddSingleton<BookingProcessor>();

// Min gamla version
//builder.Services.AddSingleton(bp => new BookingProcessor(new CollectionData()));

await builder.Build().RunAsync();
