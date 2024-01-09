using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var process = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
var env = app.Environment;

//creating the configuration settings value present in appsettings.json
IConfiguration config = app.Configuration;

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
app.UseFileServer(fileServerOptions); // to reder a default html file
app.UseStaticFiles();

//reading the config settings value in appsettings.json
//app.MapGet("/", () => config["MyKey"]);

//reading the hosting environment from the lauchsettings.json
app.MapGet("/", () => "Hosting Environment: " + env.EnvironmentName);


//reading the process name present in launchsettings.json
//app.MapGet("/", () =>  process);



//app.MapGet("/", () => "Hello World!");

app.Run();
