using Microsoft.EntityFrameworkCore;
using QRCodeApp.Application.Services;
using QRCodeApp.Core.Abstractions.Interfaces;
using QRCodeApp.DataAccess;
using QRCodeApp.DataAccess.Repositories;
using QRCodeApp.Infrastructures.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<QrInfoDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(QrInfoDbContext)));
    });

builder.Services.AddScoped<IQrCodesService, QrCodesService>();
builder.Services.AddScoped<IQrInfosRepository, QrInfosRepository>();
builder.Services.AddScoped<IQrCodeGenerator, QrCodeGenerator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<QrInfoDbContext>();
    dbContext.Database.Migrate(); // <-- автоматически применяет миграции
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
