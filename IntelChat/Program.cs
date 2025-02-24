using IntelChat.Hubs;
using IntelChat.Models;
using IntelChat.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//ADDED SERVICES-----------------------------------------------------------------------------------------------------------
builder.Services.AddScoped<NewStateService>();
builder.Services.AddScoped<ChatStateService>();
builder.Services.AddSingleton<NounService>(); //noun service singleton to reduce DB reads
builder.Services.AddSingleton<VerbService>(); //verb singleton - reduce reads
builder.Services.AddSingleton<EntityService>(); //verb singleton - reduce reads
builder.Services.AddScoped<NotificationService>(); //notif serviice
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/octed-stream"});
});

// Add your DbContext with the connection string from appsettings.json
builder.Services.AddDbContext<NnetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie();
builder.Services.AddHttpContextAccessor();

// Add Entity Framework Support to Quick Grid
// builder.Services.AddQuickGridEntityFrameworkAdapter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToPage("/_Host");
app.MapGet("/hello-dotnet-8", () => "Hello, .NET 8.0"); //.net 8 minimal api test

app.Run();
