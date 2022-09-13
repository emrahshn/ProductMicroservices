using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Review.Comments.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "ReviewDatabase"));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Review.Comment", Version = "v1" });
});
var app = builder.Build();

using (var provider = builder.Services.BuildServiceProvider())
{
    DataGenerator.Initialize(provider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Review.Comment v1"));

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
