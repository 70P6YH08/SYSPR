using LabWork27;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");

app.MapGet("/", () => "Hello World!");

app.Run();
