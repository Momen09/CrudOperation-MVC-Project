using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleProject.Data;
using SimpleProject.DependencyInjections;
using SimpleProject.Repositories.implimintation;
using SimpleProject.Repositories.Interfaces;
using SimpleProject.Resources;
using SimpleProject.Services.Implementations;
using SimpleProject.Services.Interfaces;
using SimpleProject.SharedRepository;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

//create one instance for the whole application
//builder.Services.AddSingleton<IProductService,ProductService>();

//create one instance for each request
#region register DependencyInjections
builder.Services.AddServiceDependencyInjection().
    AddRepositoryDependencyInjection().
    AddLocalizationDependencyInjection().
    AddGeneralDependencyInjection(builder.Configuration);
#endregion








var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/home/error");
}

app.UseApplicationBuilderDependencyInjection(app.Services);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

//app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{Action=Index}/{Id?}"
    );


app.Run();
