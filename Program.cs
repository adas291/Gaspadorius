using System.Diagnostics.Tracing;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Gaspadorius.Auth;
using Gaspadorius.Data.Models;
using Gaspadorius.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddAuthentication()
                .AddJwtBearer(options => {
                    options.TokenValidationParameters.ValidAudience = builder.Configuration["Jwt:ValidAudience"];
                    options.TokenValidationParameters.ValidIssuer = builder.Configuration["Jwt:ValidIssuer"];
                    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]));
                    options.MapInboundClaims = false;
                });


Gaspadorius.Config.ConfigureServices(builder.Configuration);

builder.Services.AddSingleton<Gaspadorius.Auth.JwtTokenService>();
builder.Services.AddSingleton<Gaspadorius.Auth.UserManager>();

builder.Services.AddDbContext<Gaspadorius.DataContext.UserContext>(options =>
{
    options.UseSqlite("Data source =ef.db");
});

// builder.Services.AddIdentity<User, Role>()
//     .AddDefaultTokenProviders();

// builder.Services.AddScoped<RoleManager<Role>>();


builder.Services.AddAuthorization(options =>{
    options.AddPolicy(PolicyNames.ResourceOwner, policy => policy.Requirements.Add(new ResourceOwnerRequirement()));
    options.AddPolicy(PolicyNames.HasPhone, policy => policy.Requirements.Add(new PhoneRequirement()));
    // options.AddPolicy(PolicyNames.HasPhone, policy => policy.RequireAssertion(context 
    //                                                => context.User.HasClaim( c 
    //                                                => c.Type == JwtRegisteredClaimNames.PhoneNumber)));
});

builder.Services.AddSingleton<IAuthorizationHandler, ResourceOwnerAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, HasPhoneNumberHandler>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();