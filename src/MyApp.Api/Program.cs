using System.Globalization;
using MyApp.Core;
using MyApp.Domain.DI;
using MyApp.Infrastructure.DI;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

Config.SetConfigurationProvider(DependencyResolver.GetConfigurationProvider());

////////////////////////////////////////////////////////////////////////////////////////////////////
// Compose
////////////////////////////////////////////////////////////////////////////////////////////////////
var appBuilder = WebApplication.CreateBuilder(args);

new DomainModule().Load(appBuilder.Services);
new InfrastructureModule().Load(appBuilder.Services);

appBuilder.Services.AddControllers();

var app = appBuilder.Build();

////////////////////////////////////////////////////////////////////////////////////////////////////
// Configure
////////////////////////////////////////////////////////////////////////////////////////////////////
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

#if !DEBUG
app.UseHttpsRedirection();
#endif

#if DEBUG
app.UseCors(builder =>
    builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build());
#endif

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
