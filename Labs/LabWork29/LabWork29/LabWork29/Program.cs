using LabWork29;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<FileWatcherService>();
builder.Services.AddHostedService<LogArchiveService>();

var host = builder.Build();
host.Run();
