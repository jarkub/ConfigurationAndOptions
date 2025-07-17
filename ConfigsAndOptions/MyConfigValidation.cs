using System.Text.RegularExpressions;

using Microsoft.Extensions.Options;

namespace webapp_my.ConfigsAndOptions;

public class MyConfigValidation : IValidateOptions<MyConfigOptions>
{
    public MyConfigOptions _config { get; private set; }

    public MyConfigValidation(IConfiguration config)
    {
        _config = config.GetSection(MyConfigOptions.MyConfig)
            .Get<MyConfigOptions>();
    }

    public ValidateOptionsResult Validate(string name, MyConfigOptions options)
    {
        FunWithInterfaces();

        string? vor = null;
        var rx = new Regex(@"^[a-zA-Z''-'\s]{1,40}$");
        var match = rx.Match(options.Key1!);

        if (string.IsNullOrEmpty(match.Value))
        {
            vor = $"{options.Key1} doesn't match RegEx \n";
        }

        if (options.Key2 < 0 || options.Key2 > 1000)
        {
            vor = $"{options.Key2} doesn't match Range 0 - 1000 \n";
        }

        if (_config.Key2 != default)
        {
            if (_config.Key3 <= _config.Key2)
            {
                vor += "Key3 must be > than Key2.";
            }
        }

        if (vor != null)
        {
            return ValidateOptionsResult.Fail(vor);
        }

        return ValidateOptionsResult.Success;
    }

    private bool tryOnce = true;
    public void FunWithInterfaces()
    {
        if (!tryOnce)
        {
            return;
        }
        else
        {
            tryOnce = false;
        }
        var ac = new AConcrete();
        Console.WriteLine(nameof(ac.AStringProperty) + " " + ac.AStringProperty);
        Console.WriteLine(nameof(ac.ABooleanProperty) + " " + ac.ABooleanProperty);
        //Console.WriteLine(nameof(ac.IsNull) + " " + ac.IsNull);
        string type = string.Empty;
        string method = string.Empty;
        try
        {
            type = $"{ac.GetType()}";
            method = $"{nameof(ac.IsNotNull)}";
            Console.WriteLine($"Attempting {type}.{method}");
            // stack overflow
            //Console.WriteLine(ac.IsNotNull);
            //Console.WriteLine($"{type}.{method} WORKED!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{type}.{method} FAILED!");
            Console.WriteLine(ex);
        }

        try
        {
            IInterface iface = (ac as IInterface);
            //string s = $"{nameof(iface.IsNotNull)}";
            type = $"{(((IInterface)ac).GetType())}";
            method = $"{nameof(iface.IsNotNull)}";
            Console.WriteLine($"Attempting {type}.{method}");
            //stack overflow
            //Console.WriteLine(iface.IsNotNull);
            //Console.WriteLine($"{type}.{method} WORKED!");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{type}.{method} FAILED!");
            Console.WriteLine(ex);
        }

        Console.WriteLine(nameof(ac.IsNull2) + " " + ac.IsNull2());
        //Console.WriteLine(nameof(ac.IsNotNull2) + " " + ac.IsNotNull2());

        Console.WriteLine();
        ac.AStringProperty = "Test String";
        Console.WriteLine(nameof(ac.AStringProperty) + " " + ac.AStringProperty);
        Console.WriteLine(nameof(ac.ABooleanProperty) + " " + ac.ABooleanProperty);
        //Console.WriteLine(nameof(ac.IsNull) + " " + ac.IsNull);
        //Console.WriteLine(nameof(ac.IsNotNull) + " " + ac.IsNotNull);
        //Console.WriteLine(nameof(ac.IsNull2) + " " + ac.IsNull2());
        //Console.WriteLine(nameof(ac.IsNotNull2) + " " + ac.IsNotNull2());
        ac.AStringProperty = null;
        Console.WriteLine();

        var ic = ac as IInterface;
        Console.WriteLine(nameof(ic.AStringProperty) + " " + ic.AStringProperty);
        Console.WriteLine(nameof(ic.ABooleanProperty) + " " + ic.ABooleanProperty);
        Console.WriteLine(nameof(ic.IsNull) + " " + ic.IsNull);
        Console.WriteLine(nameof(ic.IsNotNull) + " " + ic.IsNotNull);
        Console.WriteLine(nameof(ic.IsNull2) + " " + ic.IsNull2());
        Console.WriteLine(nameof(ic.IsNotNull2) + " " + ic.IsNotNull2());

        Console.WriteLine();

        ic.AStringProperty = "Test String";
        Console.WriteLine(nameof(ic.AStringProperty) + " " + ic.AStringProperty);
        Console.WriteLine(nameof(ic.ABooleanProperty) + " " + ic.ABooleanProperty);
        Console.WriteLine(nameof(ic.IsNull) + " " + ic.IsNull);
        Console.WriteLine(nameof(ic.IsNotNull) + " " + ic.IsNotNull);
        Console.WriteLine(nameof(ic.IsNull2) + " " + ic.IsNull2());
        Console.WriteLine(nameof(ic.IsNotNull2) + " " + ic.IsNotNull2());
        ic.AStringProperty = null;
        Console.WriteLine();
        ic.SetIsNull1();
        Console.WriteLine(nameof(ic.ABooleanProperty) + " " + ic.ABooleanProperty);
        Console.WriteLine(nameof(ic.IsNull) + " " + ic.IsNull);
        Console.WriteLine(nameof(ic.IsNotNull) + " " + ic.IsNotNull);
        Console.WriteLine(nameof(ic.IsNull2) + " " + ic.IsNull2());
        Console.WriteLine(nameof(ic.IsNotNull2) + " " + ic.IsNotNull2());

        Console.WriteLine();
        ic.SetIsNotNull1();
        Console.WriteLine(nameof(ic.ABooleanProperty) + " " + ic.ABooleanProperty);
        Console.WriteLine(nameof(ic.IsNull) + " " + ic.IsNull);
        Console.WriteLine(nameof(ic.IsNotNull) + " " + ic.IsNotNull);
        Console.WriteLine(nameof(ic.IsNull2) + " " + ic.IsNull2());
        Console.WriteLine(nameof(ic.IsNotNull2) + " " + ic.IsNotNull2());

        Console.WriteLine();
        ic.SetIsNull2();
        Console.WriteLine(nameof(ic.ABooleanProperty) + " " + ic.ABooleanProperty);
        Console.WriteLine(nameof(ic.IsNull) + " " + ic.IsNull);
        Console.WriteLine(nameof(ic.IsNotNull) + " " + ic.IsNotNull);
        Console.WriteLine(nameof(ic.IsNull2) + " " + ic.IsNull2());
        Console.WriteLine(nameof(ic.IsNotNull2) + " " + ic.IsNotNull2());

        Console.WriteLine();
        ic.SetIsNotNull2();
        Console.WriteLine(nameof(ic.ABooleanProperty) + " " + ic.ABooleanProperty);
        Console.WriteLine(nameof(ic.IsNull) + " " + ic.IsNull);
        Console.WriteLine(nameof(ic.IsNotNull) + " " + ic.IsNotNull);
        Console.WriteLine(nameof(ic.IsNull2) + " " + ic.IsNull2());
        Console.WriteLine(nameof(ic.IsNotNull2) + " " + ic.IsNotNull2());

        Console.WriteLine();
        ic.SetIsNull3();
        Console.WriteLine(nameof(ic.ABooleanProperty) + " " + ic.ABooleanProperty);
        Console.WriteLine(nameof(ic.IsNull) + " " + ic.IsNull);
        Console.WriteLine(nameof(ic.IsNotNull) + " " + ic.IsNotNull);
        Console.WriteLine(nameof(ic.IsNull2) + " " + ic.IsNull2());
        Console.WriteLine(nameof(ic.IsNotNull2) + " " + ic.IsNotNull2());

        Console.WriteLine();
        ic.SetIsNotNull3();
        Console.WriteLine(nameof(ic.ABooleanProperty) + " " + ic.ABooleanProperty);
        Console.WriteLine(nameof(ic.IsNull) + " " + ic.IsNull);
        Console.WriteLine(nameof(ic.IsNotNull) + " " + ic.IsNotNull);
    }
}