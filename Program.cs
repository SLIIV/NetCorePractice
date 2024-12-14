using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddAuthorization();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddDataProtection()
    .PersistKeysToDbContext<ApplicationContext>();
builder.Services.AddSession();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.Use(async (context, next) =>
{
    if (context.Request.Cookies.TryGetValue("accessToken", out string? JwtToken))
    {
        context.Request.Headers.Authorization = $"Bearer {JwtToken}";
    }
    //if (!string.IsNullOrEmpty(JwtToken))
    //{

    //}

    await next();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "users",
    pattern: "{controller=Users}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Map("/hello", [Authorize] (HttpContext context) => $"hello, world" );
//app.Map("/login/{username}", (string username) =>
//{
//    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username)};
//    var jwt = new JwtSecurityToken(
//        issuer: AuthOptions.ISSUER,
//        audience: AuthOptions.AUDIENCE,
//        claims: claims,
//        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
//        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
//    return new JwtSecurityTokenHandler().WriteToken(jwt);
//});


app.Run();


