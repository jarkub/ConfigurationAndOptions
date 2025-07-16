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

public class IOptionsSnapshotPatternModel : PageModel
{
    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration Configuration;
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public readonly ILogger<TestModel> logger;
    public readonly Guid Id = Guid.NewGuid();
    public string? Message { get; private set; }

    private readonly PositionOptions _snapshotOptions;

    /*
     * The following code was added to Program.cs to bind the Position section and add it to the dependency injection service container:
     *          builder.Services.Configure<PositionOptions>(
     *              builder.Configuration.GetSection(PositionOptions.Position));
     *  
     *  Note that changes to the JSON configuration file after the app has started are NOT read. 
     *  To read changes after the app has started, use IOptionsSnapshot.
     *  
     *  Options interfaces
     *  2. IOptionsSnapshot<TOptions>:
     *  - Is useful in scenarios where options should be recomputed on every request. For more information, see Use IOptionsSnapshot to read updated data.
     *  - Is registered as Scoped and therefore can't be injected into a Singleton service.
     *  - Supports named options
     *  
     *  Use IOptionsSnapshot to read updated data
     *  - Using IOptionsSnapshot<TOptions>:
     *      - Options are computed once per request when accessed and cached for the lifetime of the request.
     *      - May incur a significant performance penalty because it's a Scoped service and is recomputed per request. 
     *          For more information, see this GitHub issue and Improve the performance of configuration binding.
     *      - Changes to the configuration are read after the app starts when using configuration providers that support reading updated configuration values.
     *  - The difference between IOptionsMonitor and IOptionsSnapshot is that:
     *      - IOptionsMonitor is a Singleton service that retrieves current option values at any time, which is especially useful in singleton dependencies.
     *      - IOptionsSnapshot is a Scoped service and provides a snapshot of the options at the time the IOptionsSnapshot<T> object is constructed. 
     *          Options snapshots are designed for use with transient and scoped dependencies.
     */

    [ActivatorUtilitiesConstructor]
    public IOptionsSnapshotPatternModel(IOptionsSnapshot<PositionOptions> snapshotOptions, IConfiguration configuration, ILogger<TestModel> _logger)
    {
        _snapshotOptions = snapshotOptions.Value;

        Configuration = configuration;
        logger = _logger;
        logger.LogInformation($"Id=[{Id}] {nameof(TestModel)} constructor called");
    }

    public PageResult OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        
        Message = $"<br>Title: {_snapshotOptions.Title} " +
                       $"<br>Name: {_snapshotOptions.Name}";

        return Page();
    }
}