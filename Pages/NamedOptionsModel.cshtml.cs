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

public class NamedOptionsModel : PageModel
{
    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration Configuration;
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public readonly ILogger<TestModel> logger;
    public readonly Guid Id = Guid.NewGuid();
    public string? Message { get; private set; }

    private readonly TopItemSettings _monthTopItem;
    private readonly TopItemSettings _yearTopItem;


    /*
     * The following code was added to Program.cs to bind the Position section and add it to the dependency injection service container:
     *          builder.Services.Configure<PositionOptions>(
     *              builder.Configuration.GetSection(PositionOptions.Position));
     *  
     *  All options are named instances. 
     *  IConfigureOptions<TOptions> instances are treated as targeting the Options.
     *  DefaultName instance, which is string.Empty. 
     *  IConfigureNamedOptions<TOptions> also implements IConfigureOptions<TOptions>. 
     *  The default implementation of the IOptionsFactory<TOptions> has logic to use each appropriately. 
     *  The null named option is used to target all of the named instances instead of a specific named instance. 
     *  ConfigureAll and PostConfigureAll use this convention.
     *  
     *  IOptionsFactory<TOptions> is responsible for creating new options instances. It has a single Create method. 
     *  The default implementation takes all registered IConfigureOptions<TOptions> and IPostConfigureOptions<TOptions> and runs all the configurations first, 
     *  followed by the post-configuration. It distinguishes between IConfigureNamedOptions<TOptions> and IConfigureOptions<TOptions> and only calls the appropriate interface.
     *  
     *  IOptionsMonitorCache<TOptions> is used by IOptionsMonitor<TOptions> to cache TOptions instances. 
     *  The IOptionsMonitorCache<TOptions> invalidates options instances in the monitor so that the value is recomputed (TryRemove). 
     *  Values can be manually introduced with TryAdd. The Clear method is used when all named instances should be recreated on demand.
     */

    [ActivatorUtilitiesConstructor]
    public NamedOptionsModel(IOptionsSnapshot<TopItemSettings> namedOptionsAccessor, IConfiguration configuration, ILogger<TestModel> _logger)
    {
        _monthTopItem = namedOptionsAccessor.Get(TopItemSettings.Month);
        _yearTopItem = namedOptionsAccessor.Get(TopItemSettings.Year);


        Configuration = configuration;
        logger = _logger;
        logger.LogInformation($"Id=[{Id}] {nameof(TestModel)} constructor called");
    }

    public PageResult OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        
        Message =   $"<br> Month:Name {_monthTopItem.Name} " +
                    $"<br> Month:Model {_monthTopItem.Model} <br>" +
                    $"<br> Year:Name {_yearTopItem.Name} " +
                    $"<br> Year:Model {_yearTopItem.Model} ";

        return Page();
    }
}