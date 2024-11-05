using TextAnalysisMicroservice.Services.Interfaces;
using TextAnalysisMicroservice.Services;
using System.Net;
using TextAnalysisMicroservice.Models.Responses;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITextAnalysisService, TextAnalysisService>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new ApiResponse<string>
        {
            Success = false,
            Message = "An unexpected error occurred.",
            ErrorDetails = "An error occurred while processing your request."
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    });
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
