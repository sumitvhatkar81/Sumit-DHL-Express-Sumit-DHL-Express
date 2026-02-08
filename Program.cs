using Microsoft.EntityFrameworkCore;
using DHL_Document_App.Data;
using DHL_Document_App.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("DocDb"));
builder.Services.AddScoped<IDocumentService, DocumentService>();

// Configure controllers
builder.Services.AddControllers();

// Configure form options for file uploads
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50MB
    options.ValueLengthLimit = int.MaxValue;
    options.ValueCountLimit = int.MaxValue;
    options.KeyLengthLimit = int.MaxValue;
});

builder.Services.AddRazorPages();


var app = builder.Build();

// Configure request size limits for file uploads
app.Use(async (context, next) =>
{
    // Set max request body size to 50MB for file uploads
    var maxRequestBodySizeFeature = context.Features.Get<IHttpMaxRequestBodySizeFeature>();
    if (maxRequestBodySizeFeature != null && !maxRequestBodySizeFeature.IsReadOnly)
    {
        maxRequestBodySizeFeature.MaxRequestBodySize = 50 * 1024 * 1024;
    }
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}
// Remove HTTPS redirection in development to avoid SSL/TLS protocol errors
// app.UseHttpsRedirection(); // Commented out for development

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
