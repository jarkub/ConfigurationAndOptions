using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp_my.ConfigsAndOptions;

public class PositionOptionsWithConfigurationKeyName
{
    public const string Position = "Position";
    [ConfigurationKeyName("position-title")]
    public string Title { 
        get; 
        set; 
    } = string.Empty;
    [ConfigurationKeyName("position-name")]
    public string Name { 
        get; 
        set; 
    } = string.Empty;
}