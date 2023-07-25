namespace Membership.ExceptionHandlerMiddleware;
public static class MembershipExceptionHandler
{
    static Dictionary<Type, Delegate> _exceptionHandlers = new();

    //public static void AddExceptionHandlers(Assembly assembly)
    //{
    //    var handlerTypes = assembly.GetTypes()
    //        .Where(t => t.Name.EndsWith("ExceptionHandler") &&
    //         t.GetMethods().Any(m => m.Name == "Handle" && m.GetParameters().Length == 2));

    //    _exceptionHandlers = new Dictionary<Type, Delegate>();
    //    foreach (var item in handlerTypes)
    //    {
    //        // add _exceptionHandlers
    //        var method = item.GetMethod("Handle");
    //        var exceptionType = method.GetParameters()[0].ParameterType;
    //        var exceptionParameter = Expression.Parameter(exceptionType, "ex");
    //        var localizerParameter = Expression.Parameter(typeof(IMembershipMessageLocalizer), "localizer");
    //        var bodyParameter = Expression.Call(null, method, exceptionParameter, localizerParameter);
    //        var lambda = Expression.Lambda(bodyParameter, exceptionParameter, localizerParameter);
    //        var Delegate = lambda.Compile();

    //        _exceptionHandlers.Add(exceptionType, Delegate);
    //    }
    //}

    public static void AddHandler(Type exceptionType, Delegate @delegate) =>
        _exceptionHandlers.TryAdd(exceptionType, @delegate);

    public static async Task<bool> WriteResponse(HttpContext context, IMembershipMessageLocalizer localizer)
    {
        IExceptionHandlerFeature exceptionDetail = context.Features.Get<IExceptionHandlerFeature>();

        Exception exceptionError = exceptionDetail?.Error;

        bool isHandled = true;

        if (exceptionError != null)
        {
            if (_exceptionHandlers.TryGetValue(exceptionError.GetType(), out Delegate handler))
            {

                await WriteProblemDetailsAsync(context,
                    handler.DynamicInvoke(exceptionError, localizer) as ProblemDetails);
            }
            else
            {
                isHandled = false;
            }
        }

        return isHandled;
    }


    public static ProblemDetails FromHttp400BadRequest(this ProblemDetails problem,
        string title, string instance, object extensions = null)
    {
        problem.Title = title;
        problem.Status = StatusCodes.Status400BadRequest;
        problem.Type = "https://datatacker.ietf.org/doc/html/rfc7231#section-6.5.1";
        problem.Instance = $"problemDetails/{instance}";

        if (extensions != null)
            problem.Extensions.Add("errors", extensions);
        return problem;
    }

    //static async Task Write400BadRequestAsync(HttpContext context,
    //    string title, string instance, object extensions = null)
    //{
    //    ProblemDetails problemDetails = new()
    //    {
    //        Title = title,
    //        Status = StatusCodes.Status400BadRequest,
    //        Type = "https://datatacker.ietf.org/doc/html/rfc7231#section-6.5.1",
    //        Instance = $"problemDetails/{instance}"
    //    };
    //    if (extensions != null)
    //        problemDetails.Extensions.Add("errors", extensions);
    //    await WriteProblemDetailsAsync(context, problemDetails);
    //}

    static async Task WriteProblemDetailsAsync(HttpContext context,
        ProblemDetails problemDetails)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status.Value;
        var stream = context.Response.Body;
        await JsonSerializer.SerializeAsync(stream, problemDetails);
    }
}
