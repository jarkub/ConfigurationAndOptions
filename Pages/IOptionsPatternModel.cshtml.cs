using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

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

public class IOptionsPatternModel : PageModel
{
    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration Configuration;
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public readonly ILogger<TestModel> logger;
    public readonly Guid Id = Guid.NewGuid();
    public string? Message { get; private set; }

    private readonly PositionOptions _options;

    /*
     * The following code was added to Program.cs to bind the Position section and add it to the dependency injection service container:
     *          builder.Services.Configure<PositionOptions>(
     *              builder.Configuration.GetSection(PositionOptions.Position));
     *  
     *  Note that changes to the JSON configuration file after the app has started are NOT read. 
     *  To read changes after the app has started, use IOptionsSnapshot.
     *  
     *  Options interfaces
     *  1. IOptions<TOptions>:
     *  - Does not support:
     *      - Reading of configuration data after the app has started.
     *      - Named options
     *  - Is registered as a Singleton and can be injected into any service lifetime.
     */

    [ActivatorUtilitiesConstructor]
    public IOptionsPatternModel(IOptions<PositionOptions> options, IConfiguration configuration, ILogger<TestModel> _logger)
    {
        _options = options.Value;

        Configuration = configuration;
        logger = _logger;
        logger.LogInformation($"Id=[{Id}] {nameof(TestModel)} constructor called");
    }

    public PageResult OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        
        Message = $"<br>Title: {_options.Title} " +
                       $"<br>Name: {_options.Name}";

        return Page();
    }
}