using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp_my.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]

/*
 * Application configuration providers
 * The following code displays the enabled configuration providers in the order they were added:
 * The preceding list of highest to lowest priority default configuration sources shows the providers in the opposite order 
 * they are added to template generated application. For example, the JSON configuration provider is added before the Command-line configuration provider.
 * Configuration providers that are added later have higher priority and override previous key settings. For example, if MyKey is set in both 
 * appsettings.json and the environment, the environment value is used. 
 * Using the default configuration providers, the Command-line configuration provider overrides all other providers.
 * For more information on CreateBuilder, see Default builder settings.
 */
public class Index2Model : PageModel
{
    private IConfigurationRoot ConfigRoot;
    public string? RequestId { get; private set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public readonly ILogger<Index2Model> logger;
    public readonly Guid Id = Guid.NewGuid();
    public string? Message { get; private set; }

    [ActivatorUtilitiesConstructor]
    public Index2Model(IConfiguration configRoot, ILogger<Index2Model> _logger)
    {
        ConfigRoot = (IConfigurationRoot)configRoot;
        logger = _logger;
        logger.LogInformation($"Id=[{Id}] {nameof(Index2Model)} constructor called.");
    }

    public PageResult OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        logger.LogInformation($"Id=[{Id}] {nameof(Index2Model)} OnGet called. RequestId=[{RequestId}]");

        string str = "";
        foreach (var provider in ConfigRoot.Providers.ToList())
        {
            str += provider.ToString() + "<br>";
        }

        Message = str;//.ToString();

        return Page();
    }
}