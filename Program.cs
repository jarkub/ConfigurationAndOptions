using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

using webapp_my.ConfigsAndOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0

/*
 * The Options Pattern:
 * An alternative approach when using the options pattern is to bind the Position section and add it to the dependency injection service container. 
 * In the following code, PositionOptions is added to the service container with Configure and bound to configuration:
 */
builder.Services.Configure<PositionOptions>(
    builder.Configuration.GetSection(PositionOptions.Position));
builder.Services.Configure<PositionOptionsWithConfigurationKeyName>(
    builder.Configuration.GetSection(PositionOptionsWithConfigurationKeyName.Position));

// Configure the named options
builder.Services.Configure<TopItemSettings>(TopItemSettings.Month,
    builder.Configuration.GetSection("TopItem:Month"));
builder.Services.Configure<TopItemSettings>(TopItemSettings.Year,
    builder.Configuration.GetSection("TopItem:Year"));

//builder.Services.AddControllersWithViews();
/*
 * Options Validation
 * The following code:
 * - Calls AddOptions to get an OptionsBuilder<TOptions> that binds to the MyConfigOptions class.
 * - Calls ValidateDataAnnotations to enable validation using DataAnnotations.
 * 
 * The ValidateDataAnnotations extension method is defined in the Microsoft.Extensions.Options.DataAnnotations NuGet package. 
 * For web apps that use the Microsoft.NET.Sdk.Web SDK, this package is referenced implicitly from the shared framework.
 */
builder.Services.AddOptions<MyConfigOptions>()
            .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig))
            .ValidateDataAnnotations()
            .ValidateOnStart();

//builder.Services.AddOptions<MyConfigOptions>()
//            .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig))
//            .ValidateDataAnnotations()
//        .Validate(config =>
//        {
//            if (config.Key2 != 0)
//            {
//                return config.Key3 > config.Key2;
//            }

//            return true;
//        }, "Key3 must be > than Key2.");   // Failure message.

builder.Services.Configure<MyConfigOptions>(builder.Configuration.GetSection(
                                        MyConfigOptions.MyConfig));

builder.Services.AddSingleton<IValidateOptions
                              <MyConfigOptions>, MyConfigValidation>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

/*
 * The following code adds the static assets middleware to the application.
 * The static assets middleware serves static files from the wwwroot folder.
 * The MapStaticAssets method is an extension method that maps the static assets middleware to the application.
 * The MapRazorPages method is an extension method that maps the Razor Pages middleware to the application.
 */
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
