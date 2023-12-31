using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OgrenciNotlariDL.ContextInfo;
using OgrenciNotlariDL.ImplementationofRepos;
using OgrenciNotlariDL.InterfaceofRepos;
using OgrenciNotlariBL.ImplementationofManagers;
using OgrenciNotlariBL.InterfaceofManagers;
using OgrenciNotlariEL.IdentityModels;
using OgrenciNotlariEL.Mappings;
using OgrenciNotlariPL.CreateDefaultData;
using Serilog;
using OgrenciNotlariBL.EmailSenderProcess;

var builder = WebApplication.CreateBuilder(args);

// serilog logger ayarlari

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//contexti ayarliyoruz.
builder.Services.AddDbContext<MyContext>(options =>
{
    //klasik mvcde connection string web configte yer alir.
    //core mvcde connection string appsetting.json dosyasindan alinir.
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyLocal"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});

//appuser ve approle identity ayari
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireDigit = true;
    opt.User.RequireUniqueEmail = true;
    //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+&%";

}).AddDefaultTokenProviders().AddEntityFrameworkStores<MyContext>();


//automapper ayari 
builder.Services.AddAutoMapper(a =>
{
    a.AddExpressionMapping();
    a.AddProfile(typeof(Maps));
});
builder.Services.AddScoped<IEmailManager, EmailManager>();
builder.Services.AddScoped<ILessonRepo, LessonRepo>();
builder.Services.AddScoped<ILessonManager, LessonManager>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<IStudentManager, StudentManager>();
builder.Services.AddScoped<IStudentLessonScoreRepo, StudentLessonScoreRepo>();
builder.Services.AddScoped<IStudentLessonScoreManager, StudentLessonScoreManager>();
builder.Services.AddScoped<IUserForgotPasswordsHistoricalRepo, UserForgotPasswordsHistoricalRepo>();
builder.Services.AddScoped<IUserForgotPasswordsHistoricalManager, UserForgotPasswordsHistoricalManager>();

builder.Services.AddScoped<IUserForgotPasswordTokensRepo, UserForgotPasswordTokensRepo>();
builder.Services.AddScoped<IUserForgotPasswordTokensManager, UserForgotPasswordTokensManager>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //login logout
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

       var roleManager = serviceProvider.
    GetRequiredService<RoleManager<AppRole>>();

    //CreateData c = new CreateData(logger);
    //c.CreateRoles(serviceProvider);

}


app.Run();
