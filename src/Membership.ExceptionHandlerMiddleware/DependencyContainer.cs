namespace Membership.ExceptionHandlerMiddleware;
public static class DependencyContainer
{
    public static IApplicationBuilder UseMembershipExceptionHandler(this IApplicationBuilder app)
    {
        // esta es otra estrategia para agregar los manejadores de excepciones
        //MembershipExceptionHandler.AddExceptionHandlers(Assembly.GetExecutingAssembly());

        AddHandlers();

        app.UseExceptionHandler(builder =>
        {
            builder.Run(async (context) =>
            await MembershipExceptionHandler.WriteResponse(context,
            app.ApplicationServices.GetRequiredService<IMembershipMessageLocalizer>()!));
        });

        return app;
    }

    // Esta es una estrategia para agregar los manejadores de excepciones
    static void AddHandlers()
    {
        MembershipExceptionHandler.AddHandler(typeof(RegisterUserException),
                       (RegisterUserException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.RegisterUserExceptionMessage],
                nameof(RegisterUserException), ex.Errors));

        MembershipExceptionHandler.AddHandler(typeof(LoginUserException),
                       (LoginUserException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.LoginUserExceptionMessage],
                nameof(LoginUserException)));

        MembershipExceptionHandler.AddHandler(typeof(RefreshTokenCompromisedException),
                       (RefreshTokenCompromisedException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.RefreshTokenCompromisedExceptionMessage],
                nameof(RefreshTokenCompromisedException)));

        MembershipExceptionHandler.AddHandler(typeof(RefreshTokenExpiredException),
                       (RefreshTokenExpiredException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.RefreshTokenExpiredExceptionMessage],
                nameof(RefreshTokenExpiredException)));

        MembershipExceptionHandler.AddHandler(typeof(RefreshTokenNotFoundException),
                       (RefreshTokenNotFoundException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.RefreshTokenNotFoundExceptionMessage],
                nameof(RefreshTokenNotFoundException)));

        MembershipExceptionHandler.AddHandler(typeof(UnableToGetIDPTokensException),
                       (UnableToGetIDPTokensException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.UnableToGetIDPTokensExceptionMessage],
                nameof(UnableToGetIDPTokensException)));

        MembershipExceptionHandler.AddHandler(typeof(InvalidAuthorizationCodeException),
                       (InvalidAuthorizationCodeException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.InvalidAuthorizationCodeExceptionMessage],
                nameof(InvalidAuthorizationCodeException)));

        MembershipExceptionHandler.AddHandler(typeof(InvalidRedirectUriException),
                       (InvalidRedirectUriException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.InvalidRedirectUriExceptionMessage],
                nameof(InvalidRedirectUriException)));

        MembershipExceptionHandler.AddHandler(typeof(InvalidClientIdException),
                       (InvalidClientIdException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.InvalidClientIdExceptionMessage],
                nameof(InvalidClientIdException)));

        MembershipExceptionHandler.AddHandler(typeof(InvalidScopeException),
                       (InvalidScopeException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.InvalidScopeExceptionMessage],
                nameof(InvalidScopeException)));

        MembershipExceptionHandler.AddHandler(typeof(InvalidCodeVerifierException),
                       (InvalidCodeVerifierException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.InvalidCodeVerifierExceptionMessage],
                nameof(InvalidCodeVerifierException)));

        MembershipExceptionHandler.AddHandler(typeof(InvalidScopeActionException),
                       (InvalidScopeActionException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.InvalidScopeActionExceptionMessage],
                nameof(InvalidScopeActionException)));

        MembershipExceptionHandler.AddHandler(typeof(MissingCallbackStateParameterException),
                       (MissingCallbackStateParameterException ex, IMembershipMessageLocalizer localizer) =>
                       new ProblemDetails().FromHttp400BadRequest(localizer[MessageKeys.MissingCallbackStateParameterExceptionMessage],
                nameof(MissingCallbackStateParameterException)));

    }
}
