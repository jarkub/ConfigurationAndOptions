using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

using webapp_my.ConfigsAndOptions;

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

public class OptionsValidationModel : PageModel
{
    // requires using Microsoft.Extensions.Configuration;
    //private readonly IConfiguration Configuration;
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public readonly ILogger<OptionsValidationModel> logger;
    private readonly IOptions<MyConfigOptions> config;
    public readonly Guid Id = Guid.NewGuid();
    public string? Message { get; private set; }
    bool IsValid;
    public string? ErrorMessage { get; private set; }

    [ActivatorUtilitiesConstructor]
    public OptionsValidationModel(IOptionsSnapshot<MyConfigOptions> _config, ILogger<OptionsValidationModel> _logger)
    {
        config = _config;
        logger = _logger;
        logger.LogInformation($"Id=[{Id}] {nameof(OptionsValidationModel)} constructor called");

        try
        {
            var configValue = config.Value;

        }
        catch (OptionsValidationException ex)
        {
            foreach (var failure in ex.Failures)
            {
                logger.LogError(failure);
            }
        }
    }

    public PageResult OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        try
        {
            Message =   $"<br> Key1: {config.Value.Key1} " +
                        $"<br> Key2: {config.Value.Key2} " +
                        $"<br> Key3: {config.Value.Key3}";
        }
        catch (OptionsValidationException optValEx)
        {
            ErrorMessage = optValEx.Message;
        }

        return Page();
    }
}