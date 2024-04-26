using API.Extensions;
using API.Middleware;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Handle 500s errors
app.UseMiddleware<ExceptionMiddleware>();
 
// Regenerate object response when errors occur to unify the output
app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

await app.InitializeDbAsync<Program>();

app.Run();