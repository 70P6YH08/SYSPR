using LabWork29;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<FileWathcerService>();

var host = builder.Build();
host.Run();
