using System.ComponentModel.DataAnnotations;

namespace webapp_my.ConfigsAndOptions;

/*
 * In appsettings.json, the MyConfigOptions section is defined as follows:
 * {
 *   "MyConfig": {
 *     "Key1": "My Key One",
 *     "Key2": 10,
 *     "Key3": 32
 *   }
 * }
 * 
 * The following class is used to bind to the "MyConfig" configuration section and applies a couple of DataAnnotations rules:
 */
public class MyConfigOptions
{
    public const string MyConfig = "MyConfig";

    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
    public string? Key1 { 
        get; 
        set; 
    }
    [Range(0, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int Key2 { 
        get; 
        set; 
    }
    public int Key3 { 
        get; 
        set; 
    }
}