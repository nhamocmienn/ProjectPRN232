using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using RapPhim1.DAO;
using RapPhim1.Data;
using RapPhim1.Service;
using RapPhim1.Services;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

// Add services to the container.
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserDAO>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<MovieDAO>();
builder.Services.AddScoped<ActorDAO>();
builder.Services.AddScoped<GenreDAO>();
builder.Services.AddScoped<DirectorDAO>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();


builder.Services.AddScoped<RoomDAO>();
builder.Services.AddScoped<SeatDAO>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ISeatService,SeatService>();
builder.Services.AddScoped<SeatTypeDAO>();
builder.Services.AddScoped<ISeatTypeService, SeatTypeService>();

builder.Services.AddScoped<ShowtimeDAO>();
builder.Services.AddScoped<IShowtimeService, ShowtimeService>();

builder.Services.AddScoped<ServiceItemDAO>();
builder.Services.AddScoped<IServiceItemService, ServiceItemService>();

builder.Services.AddScoped<OrderDAO>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<GenreDAO>();
builder.Services.AddScoped<IGenreService, GenreService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = builder.Configuration["Jwt:Key"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
    };
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
