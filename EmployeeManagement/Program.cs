using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
//the addmvc services has to be called before the application build
//variable is created, otherwise it throws an error
builder.Services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
//this is to register the implementation of the IemployeeRepository
//This is to ensure that the request from the homeController is duly served
//currently the only implementation is mockEployeeRepository
//builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

var app = builder.Build();
var process = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
var env = app.Environment;

//creating the configuration settings value present in appsettings.json
IConfiguration config = app.Configuration;

//the adddbcontextpool helps to choose a database to use
builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(config.GetConnectionString("EmployeeDBConnection")));


if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
{
    app.UseExceptionHandler("/Error");
}

//rendering static files
//however, these files will not render if the mapget function is still
//active in the processing pipeline

FileServerOptions fileServerOptions = new FileServerOptions();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
//app.UseFileServer(fileServerOptions); // to reder a default html file
app.UseStaticFiles();


//using MVC
//the method useMvcWithDefaultRoute brings in mvc support and configures a default route
//for the application request processing pipeline
//app.UseMvcWithDefaultRoute();

//the method useMvc only provides Mvc support so we would need to explicitly specify the route
app.UseMvc(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));

//reading the config settings value in appsettings.json
//app.MapGet("/", () => config["MyKey"]);

//reading the hosting environment from the lauchsettings.json
//app.MapGet("/", () => "Hosting Environment: " + env.EnvironmentName);


//reading the process name present in launchsettings.json
//app.MapGet("/", () =>  process);



//app.MapGet("/", () => "Hello World!");

app.Run();
