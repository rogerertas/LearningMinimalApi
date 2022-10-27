using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
const string MyAllowedOrigins = "*";
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<InMemDb>(opt => opt.UseInMemoryDatabase("InMemDb"));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowedOrigins,
        builder =>
        {
            builder.AllowAnyHeader();
            builder.WithOrigins(MyAllowedOrigins);
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(MyAllowedOrigins);
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    c.RoutePrefix = "";
});

app.MapPost("/TicTacPost", (InMemDb inMem, [FromBody] string[] body) => {
    inMem.BoardState = body;
    return body;
});

app.Run();