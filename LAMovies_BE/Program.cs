using LAMovies_BE.Config;
using Libs.Data;
using Libs.Models;
using Libs.Dtos;
using Libs.Repositories;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using LAMovies_BE.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

}, ServiceLifetime.Transient);

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
var tokenValidationParameter = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JwtConfig:ValidAudience"],
    ValidIssuer = builder.Configuration["JwtConfig:ValidIssuer"],
    RequireExpirationTime = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameter;
});
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedEmail = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BossOnly", policy => policy.RequireRole("Boss"));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    options.AddPolicy("AllRole", policy => policy.RequireAssertion(context =>
                                            context.User.IsInRole("Boss")
                                            || context.User.IsInRole("Admin")
                                            || context.User.IsInRole("User")));
    options.AddPolicy("All", policy => policy.RequireAssertion(context =>
                                            context.User.IsInRole("Boss")
                                            && context.User.IsInRole("Admin")
                                            && context.User.IsInRole("User")));
    options.AddPolicy("AdminPolicyInsert", policy => policy.RequireRole("Admin").RequireClaim("Username", "admin"));
});
builder.Services.AddSingleton(tokenValidationParameter);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IPricingRepository, PricingRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUserInRoomRepository, UserInRoomRepository>();


//builder.Services.Configure<PayPalSettings>(Configuration.GetSection("PayPalSettings"));

var app = builder.Build();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<NotificationHub>("/chatHub");
app.MapHub<MeetHub>("/metting");

app.MapControllers();

app.Run();
