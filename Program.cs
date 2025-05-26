using MandiraApi.Data;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build(); // Moved app declaration before its usage  

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage(); // <-- Muy importante  
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
