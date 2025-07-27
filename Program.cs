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
            new Degree { Name = "BSc", CreationTime = DateTime.Now },
            new Degree { Name = "MSc", CreationTime = DateTime.Now },
            new Degree { Name = "PhD", CreationTime = DateTime.Now },
            new Degree { Name = "TEI", CreationTime = DateTime.Now },
            new Degree { Name = "IEK", CreationTime = DateTime.Now }
        );

        context.SaveChanges();
    }
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Candidates.Any())
    {
        context.Candidates.AddRange(
            new Candidate
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Mobile = "6971234567",
                DegreeId = 1, // BSc
                CvFilePath = "/uploads/john_doe_cv.pdf",
                CreationTime = DateTime.Now
            },
            new Candidate
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Mobile = "6987654321",
                DegreeId = 2, // MSc
                CvFilePath = "/uploads/jane_smith_cv.docx",
                CreationTime = DateTime.Now
            },
            new Candidate
            {
                Id = 3,
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice.johnson@example.com",
                Mobile = null, // optional
                DegreeId = 3, // PhD
                CvFilePath = null,
                CreationTime = DateTime.Now
            },
            new Candidate
            {
                Id = 4,
                FirstName = "Bob",
                LastName = "Anderson",
                Email = "bob.anderson@example.com",
                Mobile = "6941122334",
                DegreeId = 2, // MSc
                CvFilePath = "/uploads/bob_cv.pdf",
                CreationTime = DateTime.Now
            },
            new Candidate
            {
                Id = 5,
                FirstName = "Emily",
                LastName = "Brown",
                Email = "emily.brown@example.com",
                Mobile = "6956677889",
                DegreeId = 1, // BSc
                CvFilePath = null,
                CreationTime = DateTime.Now
            }
        );

        context.SaveChanges();
    }
}

app.Run();
