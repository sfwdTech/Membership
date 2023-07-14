using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("BearerToken",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Proporciona el token JWT",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BearerToken"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion

#region Membership Configuration
builder.Services.AddMembershipCoreServices(jwtOptions =>
    builder.Configuration.GetSection(JwtOptions.SectionKey).Bind(jwtOptions))
    .AddMembershipValidatorsServices()
    .AddMembershipMessageLocalizer()
    .AddRefreshTokenMemoryCacheServices()
    .AddUserManagerAspNetIdentityService(aspNetIdentityOptions =>
        builder.Configuration.GetSection(AspNetIdentityOptions.SectionKey)
        .Bind(aspNetIdentityOptions));
#endregion

#region CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
#endregion

#region Authentication Schema Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    builder.Configuration.GetSection(JwtOptions.SectionKey)
    .Bind(options.TokenValidationParameters);

    options.TokenValidationParameters.IssuerSigningKey =
    new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(
                   builder.Configuration.GetSection(JwtOptions.SectionKey)["SecurityKey"]));

    if (int.TryParse(builder.Configuration.GetSection(JwtOptions.SectionKey)["ClockSkewInMinutes"],
        out int value))
        options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(value);

});
#endregion

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseMembershipExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseMembershipEndpoints();

app.MapGet("/authorizeduser", (HttpContext context, IUserService userService) =>
    Results.Ok($"Hello - {userService.FullName} - {context.User.Identity.Name}"))
    .RequireAuthorization();

app.MapGet("/anonymous", () => Results.Ok("Hello - Anonymous"));


app.Run();
