using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


//builder.Services.AddAuthentication(option => {
//    option.DefaultScheme = BearerTokenDefaults.AuthenticationScheme;
//    option.RequireAuthenticatedSignIn = false;
//}).AddBearerToken();


//builder.Services.AddAuthorization(o => o.AddPolicy("product-api", policy => policy.RequireClaim("Role")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
app.MapReverseProxy();

app.MapPost("/login", () => {

    return Results.SignIn(new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
    {
        new Claim(ClaimTypes.Name, "admin"),
        new Claim("Role", "product")
    })),authenticationScheme:BearerTokenDefaults.AuthenticationScheme);
});


app.Run();

