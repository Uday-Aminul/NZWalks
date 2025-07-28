using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NZWalks.Data;
using NZWalks.Mappings;
using NZWalks.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider =new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath="/Images"
});
app.MapControllers();

app.Run();
