using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp_my.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
/*
* The default JsonConfigurationProvider loads configuration in the following order:
* 
* 1. appsettings.json
* 2. appsettings.{ Environment }.json : For example, the appsettings.Production.json and appsettings.Development.json files.
*   The environment version of the file is loaded based on the IHostingEnvironment.EnvironmentName.
*   For more information, see Use multiple environments in ASP.NET Core.
*   
*   appsettings.{Environment}.json values override keys in appsettings.json. For example, by default:
*   - In development, appsettings.Development.json configuration overwrites values found in appsettings.json.
*   - In production, appsettings.Production.json configuration overwrites values found in appsettings.json. For example, when deploying the app to Azure.
*   
*   If a configuration value must be guaranteed, see GetValue. The preceding example only reads strings and doesn¡¯t support a default value.
*   Using the default configuration, the appsettings.json and appsettings.{Environment}.json files are enabled with reloadOnChange: true. 
*   Changes made to the appsettings.json and appsettings.{Environment}.json file after the app starts are read by the JSON configuration provider.
*/

public class TestModel : PageModel
{
    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration Configuration;
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public readonly ILogger<TestModel> logger;
    public readonly Guid Id = Guid.NewGuid();
    public string? Message { get; private set; }

    [ActivatorUtilitiesConstructor]
    public TestModel(IConfiguration configuration, ILogger<TestModel> _logger)
    {
        Configuration = configuration;
        logger = _logger;
        logger.LogInformation($"Id=[{Id}] {nameof(TestModel)} constructor called");
    }

    public PageResult OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        var myKeyValue = Configuration["MyKey"];
        var title = Configuration["Position:Title"];
        var name = Configuration["Position:Name"];
        var defaultLogLevel = Configuration["Logging:LogLevel:Default"];


        Message = $"<br>MyKey value=[{myKeyValue}] <br>" +
                       $"Title=[{title}] <br>" +
                       $"Name=[{name}] <br>" +
                       $"Default Log Level=[{defaultLogLevel}]";

        return Page();
    }
}