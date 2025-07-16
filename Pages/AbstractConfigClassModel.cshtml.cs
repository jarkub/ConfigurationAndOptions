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

public class AbsctractConfigClassModel : PageModel
{
    // requires using Microsoft.Extensions.Configuration;
    private readonly IConfiguration Configuration;
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public readonly ILogger<TestModel> logger;
    public readonly Guid Id = Guid.NewGuid();
    public string? Message { get; private set; }

    /*
     * Bind also allows the concretion of an abstract class. Consider the following code which uses the abstract class
     * 
     * Calls to Bind are less strict than calls to Get<>:
     * - Bind allows the concretion of an abstract.
     * - Get<> has to create an instance itself.

     */

    [ActivatorUtilitiesConstructor]
    public AbsctractConfigClassModel(IConfiguration configuration, ILogger<TestModel> _logger)
    {
        Configuration = configuration;
        logger = _logger;
        logger.LogInformation($"Id=[{Id}] {nameof(TestModel)} constructor called");
    }

    public PageResult OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        var nameTitleOptions = new NameTitleOptions(22);
        Configuration.GetSection(NameTitleOptions.NameTitle).Bind(nameTitleOptions);

        Message =   $"<br>Title: {nameTitleOptions.Title} " +
                    $"<br>Name: {nameTitleOptions.Name}  " +
                    $"<br>Age: {nameTitleOptions.Age}"
                    ;






        return Page();
    }
}