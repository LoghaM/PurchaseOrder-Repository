using Microsoft.AspNetCore.Authentication.Cookies;
using PurchaseOrderWebApplication.Filters;
using Serilog;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    
    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
    {
        option.LoginPath = "/Login/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    });
    builder.Services.AddAuthentication();
    builder.Services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");
    builder.Services.AddRazorPages();

    //Add support to logging with SERILOG
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    //IDependency scope added
    builder.Services.AddScoped<IDependency, Dependency>();

    //web optimizer - Minification, Bundling
    builder.Services.AddWebOptimizer(pipeline =>
    {
        pipeline.MinifyJsFiles("js/**/*.js");
        pipeline.MinifyCssFiles("css/**/*.css");
        pipeline.AddCssBundle("/css/bundle.css", "css/**/*.css");
        pipeline.AddJavaScriptBundle("/js/bundle.js", "js/**/*.js");
    });
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    //Add support to logging request with SERILOG
    app.UseSerilogRequestLogging();
    app.UseRouting();

    app.UseAuthorization();

    app.UseMiddleware<Middleware>();
    app.UseWebOptimizer();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=PurchaseOrder}/{action=Index}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
