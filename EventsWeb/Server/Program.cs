using System.Text;
using Events_Data;
using Events_Repository.Data;
using Events_Repository.Repository;
using Events_Repository.Repository.IRepository;
using EventsWeb.Server;
using EventsWeb.Server.CategoryController;
using EventsWeb.Server.FileUploadController;
using EventsWeb.Server.ProductController;
using EventsWeb.Shared.Model;
using EventsWeb_ApplyMigrations.DbMigrate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Add ApplicationDbContext to the container
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//For IDentity Server
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Populate Api settings Class with Api Settings from appsettings.JsonFile 
var apiSettingsSection = builder.Configuration.GetSection("ApiSettings");
builder.Services.Configure<ApiSettings>(apiSettingsSection);

//Get ApiSettings
var apisettings = apiSettingsSection.Get<ApiSettings>();
var key = Encoding.UTF8.GetBytes(apisettings.SecretKey);

//Add JWTTokenAuthentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = true,
        ValidateIssuer = true,
    };

});

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Added Repository and Services to Container with Scoped LifeCycle
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IDbMigration, DBMigration>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

//Add SeedDatabse Method to PipeLine
SeedDatabse();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
 
app.Run();

/// <summary>
/// Creates a scoped Service of IDbMigration and that Scoped Service can invoke the Db initialize.
/// </summary>
void SeedDatabse()
{
    using (var scope = app.Services.CreateScope())

    {   // Gets IDbMigration From Service Container
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbMigration>();
        //Invoke ApplyDbMigration
        dbInitializer.ApplyDbMigration();
    }
}