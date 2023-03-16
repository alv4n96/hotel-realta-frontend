using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Realta.Frontend;
using Realta.Frontend.HttpRepository;
using Realta.Frontend.HttpRepository.Hotel;
using Realta.Frontend.HttpRepository.Hotel.Facilities;
using Realta.Frontend.HttpRepository.Hotel.Hotels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7068/api/") });

// booking

// hotel
builder.Services.AddScoped<IHotelsHttpRepository, HotelsHttpRepository>();
builder.Services.AddScoped<IFacilitiesHttpRepository, FacilitiesHttpRepository>();

// hr

// master

// payment

// purchasing

// resto

// users



await builder.Build().RunAsync();