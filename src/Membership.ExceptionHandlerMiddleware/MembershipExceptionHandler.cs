namespace Membership.ExceptionHandlerMiddleware;
public class MembershipExceptionHandler
{
    public static async Task<bool> WriteResponse(HttpContext context, IMembershipMessageLocalizer localizer)
    {
        IExceptionHandlerFeature exceptionDetail = context.Features.Get<IExceptionHandlerFeature>();

        Exception exceptionError = exceptionDetail?.Error;

        bool isHandled = true;

        if (exceptionError != null)
        {
            switch (exceptionError)
            {
                case RegisterUserException ex:
                    await Write400BadRequestAsync(context,
                        localizer[MessageKeys.RegisterUserExceptionMessage],
                        nameof(RegisterUserException),
                        ex.Errors);
                    break;
                case LoginUserException ex:
                    await Write400BadRequestAsync(context,
                          localizer[MessageKeys.LoginUserExceptionMessage],
                             nameof(LoginUserException));
                    break;
                case RefreshTokenCompromisedException ex:
                    await Write400BadRequestAsync(context,
                           localizer[MessageKeys.RefreshTokenCompromisedExceptionMessage],
                                 nameof(RefreshTokenCompromisedException));
                    break;
                case RefreshTokenExpiredException ex:
                    await Write400BadRequestAsync(context,
                          localizer[MessageKeys.RefreshTokenExpiredExceptionMessage],
                               nameof(RefreshTokenExpiredException));
                    break;
                case RefreshTokenNotFoundException ex:
                    await Write400BadRequestAsync(context,
                           localizer[MessageKeys.RefreshTokenNotFoundExceptionMessage],
                                 nameof(RefreshTokenNotFoundException));
                    break;
                default:
                    isHandled = false;
                    break;
            }
        }

        return isHandled;
    }

    static async Task Write400BadRequestAsync(HttpContext context,
        string title, string instance, object extensions = null)
    {
        ProblemDetails problemDetails = new()
        {
            Title = title,
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatacker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Instance = $"problemDetails/{instance}"
        };
        if (extensions != null)
            problemDetails.Extensions.Add("errors", extensions);
        await WriteProblemDetailsAsync(context, problemDetails);
    }

    static async Task WriteProblemDetailsAsync(HttpContext context,
        ProblemDetails problemDetails)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status.Value;
        var stream = context.Response.Body;
        await JsonSerializer.SerializeAsync(stream, problemDetails);
    }
}
