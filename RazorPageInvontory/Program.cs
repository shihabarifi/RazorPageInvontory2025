using RazorPageInvontory.Modules.UsersSys.DAL;
using RazorPageInvontory.Modules.UsersSys.BLL;
using RazorPageInvontory.ServicesLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor(); // لإتاحة الوصول للـ Session
builder.Services.AddSession(); // تفعيل الـ Session
// إضافة خدمات Razor Pages
//builder.Services.AddRazorPages();
builder.Services.AddHttpClient<AuthenticateUserSer>((provider, client) =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var token = httpContextAccessor.HttpContext?.Session.GetString("Token");

    if (!string.IsNullOrEmpty(token))
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
});




// تسجيل الخدمات في نظام حقن التبعية
//builder.Services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();
//builder.Services.AddScoped<ISalesInvoiceRepository, SalesInvoiceRepository>();

// إعداد HttpClient للتعامل مع API
builder.Services.AddTransient<AuthenticateUserManager>();
builder.Services.AddTransient<AuthenticateUserSer>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5229/") });

//builder.Services.AddHttpClient("SalesAPI", client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["APISettings:BaseUrl"]);
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// إضافة إعادة التوجيه للصفحة الافتراضية
//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/")
//    {
//        context.Response.Redirect("/Login", permanent: false);
//        return;
//    }
//    await next();
//});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseSession(); // تفعيل الـ Session
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
