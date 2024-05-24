using Comment_Service.Database;
using Comment_Service.Repositories;
using Comment_Service.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CommentDatabaseSettings>(builder.Configuration.GetSection("CommentDatabase"));
builder.Services.AddSingleton<ICommentDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CommentDatabaseSettings>>().Value);

builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
builder.Services.AddSingleton<ICommentService, CommentService>();

builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetValue<string>("CommentDatabase:ConnectionString")));

var app = builder.Build();

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

// Make the implicit Program class public so test projects can access it
public partial class Program { }