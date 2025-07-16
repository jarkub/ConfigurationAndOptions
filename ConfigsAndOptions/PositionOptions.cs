using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp_my.ConfigsAndOptions;

public class PositionOptions
{
    public const string Position = "Position";

    public string Title { 
        get; 
        set; 
    } = string.Empty;
    public string Name { 
        get; 
        set; 
    } = string.Empty;
}