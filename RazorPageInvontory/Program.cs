using RazorPageInvontory.Modules.UsersSys.DAL;
using RazorPageInvontory.Modules.UsersSys.BLL;
using RazorPageInvontory.Modules.POSSys.DAL;
using RazorPageInvontory.Modules.POSSys.BLL;
using Microsoft.AspNetCore.Mvc;
using RazorPageInvontory.Shared.BLLSharedALL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor(); // لإتاحة الوصول للـ Session
builder.Services.AddSession(); // تفعيل الـ Session

// إضافة خدمات Razor Pages
builder.Services.AddRazorPages();

//builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
//{
//    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
//});



builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

// إعداد HttpClient للتعامل مع API
builder.Services.AddTransient<AuthenticateUserManager>();
builder.Services.AddTransient<AuthenticateUserSer>();
builder.Services.AddTransient<POSManager>();
builder.Services.AddTransient<POSSer>();
builder.Services.AddTransient<BLLSharedServices>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5229/") });




var app = builder.Build();
app.UseCors();

// إضافة إعادة التوجيه للصفحة الافتراضية
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Login");
        return;
    }
    await next();
});


// إعداد المسار ونقاط النهاية
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
// Configure the HTTP request pipeline.

app.UseStaticFiles();
app.UseSession(); // تفعيل الـ Session
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
