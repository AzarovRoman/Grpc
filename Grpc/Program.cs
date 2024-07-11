using Grpc;
using Grpc.Configuration;
using Grpc.Dal;
using Grpc.Dal.Interfaces;
using Grpc.Dal.Repositories;
using Grpc.Services;
using Microsoft.EntityFrameworkCore;

const string _connStringName = "DbConnectionString";

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<Context>(op => op.UseNpgsql(configuration.GetConnectionString(_connStringName)));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddAutoMapper(typeof(GrpcMapper).Assembly);

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ServerLoggerInterceptor>();
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

app.MapGrpcService<StudentService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
