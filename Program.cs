using Microsoft.EntityFrameworkCore;
using ResumeManager.Data;
using ResumeManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/* Register the DbContext with an in-memory database */
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ResumeManagerDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Degrees.Any())
    {
        context.Degrees.AddRange(
            new Degree { Name = "BSc" },
            new Degree { Name = "MSc" },
            new Degree { Name = "PhD" }
        );

        context.SaveChanges();
    }
}


app.Run();
