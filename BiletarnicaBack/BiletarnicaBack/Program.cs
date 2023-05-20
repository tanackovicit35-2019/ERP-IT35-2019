using BiletarnicaBack.Entities;
using BiletarnicaBack.Repo;
using BiletarnicaBack.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDogadjajRepo, DogadjajRepo>();
builder.Services.AddScoped<IIzvodjacRepo, IzvodjacRepo>();
builder.Services.AddScoped<IKategorijaRepo, KategorijaRepo>();
builder.Services.AddScoped<IKartaRepo, KartaRepo>();
builder.Services.AddScoped<IPlacanjeRepo, PlacanjeRepo>();
builder.Services.AddScoped<IPorudzbinaRepo, PorudzbinaRepo>();
builder.Services.AddScoped<IStavkaRepo, StavkaRepo>();
builder.Services.AddScoped<IKorisnikRepo, KorisnikRepo>();
builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<Context>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Biletarnica") ?? throw new InvalidOperationException("Conncetion string not found")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Log", policy => policy.RequireAuthenticatedUser().RequireClaim("korisnikID"));
    options.AddPolicy("Zaposleni", policy => policy.RequireRole("zaposleni"));
    options.AddPolicy("Kupac", policy => policy.RequireRole("kupac"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
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
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.MapControllers();

app.Run();
