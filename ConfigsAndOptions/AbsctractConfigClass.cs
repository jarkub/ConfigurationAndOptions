using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp_my.ConfigsAndOptions;

public abstract class AbsctractConfigClass
{
    public abstract string? Name { get; set; }
}

public class NameTitleOptions(int age) : AbsctractConfigClass
{
    public const string NameTitle = "NameTitle";

    public override string? Name { get; set; }
    public string Title { get; set; } = string.Empty;

    public int Age { get; set; } = age;
}
