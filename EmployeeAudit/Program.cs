using Audit.Core;
using EmployeeAudit.Data;
using EmployeeAudit.Infrastructure.IRepository;
using EmployeeAudit.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

try
{
  Audit.Core.Configuration.Setup()
      .UseSqlServer(config => config
          .ConnectionString(builder.Configuration.GetConnectionString("DefaultConnectionString"))
          .Schema("dbo")
          .TableName("Events")
          .IdColumnName("EventId")
          .JsonColumnName("JsonData")
          .LastUpdatedColumnName("LastUpdatedDate")
          .CustomColumn("EventType", ev => ev.EventType)
          .CustomColumn("User", ev => ev.Environment.UserName)
      );
}
catch (Exception ex)
{
  Console.WriteLine(ex.ToString());
}

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

app.UseRouting();

// Add CORS policy
app.UseCors(builder =>
{
  builder.WithOrigins("http://localhost:5173") // Replace with your React app's URL
         .AllowAnyHeader()
         .AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default0",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default1",
    pattern: "{controller=AuditLog}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default2",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
