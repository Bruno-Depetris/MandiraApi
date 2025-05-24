using Microsoft.EntityFrameworkCore;
using MandiraApi.Data; // Asegurate que el namespace sea correcto

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<MandiraDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MandiraDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MandiraDb"))
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
