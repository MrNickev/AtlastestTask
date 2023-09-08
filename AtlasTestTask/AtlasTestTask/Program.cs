using AtlasTestTask.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPaymentScheduleService, PaymentScheduleService>();
builder.Services.AddControllersWithViews();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculate}/{action=Index}/{id?}");

app.Run();