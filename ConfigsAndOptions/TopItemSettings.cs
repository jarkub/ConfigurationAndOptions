using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp_my.ConfigsAndOptions;

/*
 * In appsettings.json, the TopItem section is defined as follows:
 * {
 *   "TopItem": {
 *     "Month": {
 *       "Name": "Green Widget",
 *       "Model": "GW46"
 *     },
 *     "Year": {
 *       "Name": "Orange Gadget",
 *       "Model": "OG35"
 *     }
 *   }
 * }
 * 
 * Rather than creating two classes to bind TopItem:Month and TopItem:Year, the following class is used for each section:
 */
public class TopItemSettings
{
    public const string Month = "Month";
    public const string Year = "Year";

    public string Name { 
        get; 
        set; 
    } = string.Empty;
    public string Model { 
        get; 
        set; 
    } = string.Empty;

}