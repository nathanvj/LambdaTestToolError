using Amazon.Lambda.Core;
using LambdaTestToolError.API.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaTestToolError.Lambda;

public class MyLambdaFunction
{
    private readonly CustomOptions _customOptions;

    public MyLambdaFunction() : this(null)
    {
        Startup.ConfigureServices();
    }

    internal MyLambdaFunction(IOptions<CustomOptions> customOptions)
    {
        _customOptions = 
            customOptions.Value ??
            Startup.Services.GetRequiredService<IOptions<CustomOptions>>().Value ??
            throw new ArgumentNullException(nameof(customOptions));
    }

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(string input, ILambdaContext context)
    {
        return $"Hello from {_customOptions.Title}, {_customOptions.Description}. You can read more here: {_customOptions.Url}";
    }
}
