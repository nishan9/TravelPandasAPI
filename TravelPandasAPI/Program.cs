using Microsoft.EntityFrameworkCore;
using TravelPandasAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Server=tcp:travelpandas.database.windows.net,1433;Initial Catalog=TravelPandas;Persist Security Info=False;User ID=nishan;Password=Cookiedance68;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")); 
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0Domain"]}";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0Audience"],
            ValidIssuer = builder.Configuration["Auth0Domain"]
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .AllowAnyOrigin());

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();

