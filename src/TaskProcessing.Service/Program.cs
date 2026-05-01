using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using TaskProcessing.Service.Application;
using TaskProcessing.Service.Infrastructure.Repositories;
using NLogLogLevel = NLog.LogLevel;

var nlogConfig = new LoggingConfiguration();
var consoleTarget = new ConsoleTarget("console")
{
    Layout = "${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=tostring}"
};
nlogConfig.AddRule(NLogLogLevel.Info, NLogLogLevel.Fatal, consoleTarget);
LogManager.Configuration = nlogConfig;
var Logger = LogManager.GetCurrentClassLogger();
Logger.Info("Program started");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

Logger.Info("Di registration started");
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
builder.Services.AddScoped<TaskService>();

Logger.Info("Application created");
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

Logger.Info("Application started");
app.Run();
