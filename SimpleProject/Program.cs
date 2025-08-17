using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using SimpleProject.Services.Implementations;
using SimpleProject.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//coonect to the database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext")));

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

//create one instance for the whole application
//builder.Services.AddSingleton<IProductService,ProductService>();

//create one instance for each request
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

//create a new instance for each injection
//builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddControllersWithViews();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/home/error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{Action=Index}/{Id?}"
    );


app.Run();
