using BT_Remunerare;
using BT_Remunerare.Helpers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Startup startup = new(builder.Configuration);
startup.ConfigureServices(builder.Services);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseCors("AllowRequests");
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(builder.Configuration["Swagger:Endpoint"], "My API V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.SeedData();
app.Run();

public partial class Program { }