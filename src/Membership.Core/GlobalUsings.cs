global using Membership.Abstractions.Interfaces.Logout;
global using Membership.Abstractions.Interfaces.Services;
global using Membership.Shared.DTOs;
global using Membership.Abstractions.Exceptions;
global using Membership.Abstractions.Interfaces.Login;
global using Membership.Shared.Interfaces;
global using Membership.Abstractions.Interfaces.Register;
global using Membership.Abstractions.Interfaces.RefreshToken;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Http;
global using Membership.Abstractions.Entities;
global using Membership.Abstractions.Options;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;