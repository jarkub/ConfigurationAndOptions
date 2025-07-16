using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using webapp_my.ConfigsAndOptions;

namespace webapp_my.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
/*
* Bind hierarchical configuration
* The preferred way to read related configuration values is using the options pattern. For example, to read the following configuration values:
* 
* An options class:
* - Must be non-abstract.
* - Has public read-write properties of the type that have corresponding items in config are bound.
* - Has its read-write properties bound to matching entries in configuration.
* - Does not have its fields bound. In the preceding code, Position is not bound. The Position field is used so the string "Position" doesn't 
*   need to be hard coded in the app when binding the class to a configuration provider.
* 
*/

public class PositionOptionsModel : PageModel
{
    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration Configuration;
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public readonly ILogger<TestModel> logger;
    public readonly Guid Id = Guid.NewGuid();
    public string? Message { get; private set; }

    [ActivatorUtilitiesConstructor]
    public PositionOptionsModel(IConfiguration configuration, ILogger<TestModel> _logger)
    {
        Configuration = configuration;
        logger = _logger;
        logger.LogInformation($"Id=[{Id}] {nameof(TestModel)} constructor called");
    }

    public PageResult OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        /*
        * The following code:
        * - Calls ConfigurationBinder.Bind to bind the PositionOptions class to the Position section.
        * - Displays the Position configuration data.
        * - By default, changes to the JSON configuration file after the app has started are read.
        */
        var positionOptions = new PositionOptions();
        Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

        Message = $"<br>Title: {positionOptions.Title}" +
                       $"<br>Name: {positionOptions.Name}";

        /*
         * ConfigurationBinder.Get<T> binds and returns the specified type. 
         * ConfigurationBinder.Get<T> may be more convenient than using ConfigurationBinder.Bind. 
         * The following code shows how to use ConfigurationBinder.Get<T> with the PositionOptions class:
         */
        var positionOptions2 = Configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();

        Message += $"<br>Title2: {positionOptions2.Title}" +
                       $"<br>Name2: {positionOptions2.Name}";





        return Page();
    }
}