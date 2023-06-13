using Microsoft.EntityFrameworkCore;
using OrixNetCoreApp.Data;
using OrixNetCoreApp.Services.ThaiDate;
using OrixNetCoreApp.Controllers;
using Microsoft.AspNetCore.Identity;
using OrixNetCoreApp.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext")
    ?? throw new InvalidOperationException("connection string not found"))
);

// Step3 Identity   :Add services to the container. Add after add Identity   
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext")
    ?? throw new InvalidOperationException("connection string not found"))
);

//Step2  Identity ™’È‡¡“ ÏUer ·≈È«‡≈◊Õ°using 
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<IdentityContext>();

//custom identity option
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Custom services
builder.Services.AddScoped<IThaiDate, ThaiDate>();

var app = builder.Build();

//enable static file (wwwroot folder)
app.UseStaticFiles();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
