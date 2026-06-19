namespace LabWork29
{
    public class FileWatcherService : BackgroundService
    {
        private readonly ILogger<FileWatcherService> _logger;
        private readonly IConfiguration _configuration;

        private string _logsFile;
        private string _watchDir;

        private object _lock = new object();

        public FileWatcherService(ILogger<FileWatcherService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            var data = _configuration.GetSection("Logging:FileWatcher");

            _watchDir = data["WatchDir"] ?? Path.Combine(AppContext.BaseDirectory, "WatchFolder");
            _logsFile = data["LogsFile"] ?? Path.Combine(AppContext.BaseDirectory, "LogFolder", "watcher.log");

            if (!Directory.Exists(_watchDir))
                Directory.CreateDirectory(_watchDir);

            if (!File.Exists(_logsFile))
                File.WriteAllText(_logsFile, string.Empty);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var watcher = new FileSystemWatcher(_watchDir);

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Created;
            watcher.Deleted += Watcher_Deleted;
            watcher.Renamed += Watcher_Renamed;
            watcher.Error += Watcher_Error;

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(2000, stoppingToken);
            }
            await Task.CompletedTask;
        }

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            try
            {
                _logger.LogError("Ошибка какая-то");
                lock (_lock)
                {
                    File.AppendAllText(_logsFile, $"[{DateTime.UtcNow}]: [{e.GetType}] - [{e.GetException}]\n");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "ОШИБКА РАБОТЫ FileWatcherService");
            }
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            WatcherAction("RENAMED", e.FullPath);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            WatcherAction("DELETED", e.FullPath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            WatcherAction("CREATED", e.FullPath);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            WatcherAction("CHANGED", e.FullPath);
        }


        public void WatcherAction(string typeName, string watcherDirPath)
        {
            try
            {
                string message = typeName switch
                {
                    "CREATED" => $"СОЗДАН объект: [{watcherDirPath}]",
                    "CHANGED" => $"ИЗМЕНЁН объект: [{watcherDirPath}]",
                    "DELETED" => $"УДАЛЁН объект: [{watcherDirPath}]",
                    "RENAMED" => $"ПЕРЕИМЕНОВАН объект: [{watcherDirPath}]",

                    _ => throw new Exception("Неизвестная ои")
                };

                _logger.LogInformation(message);
                lock (_lock)
                {
                    File.AppendAllText(_logsFile, $"[{DateTime.UtcNow}]: [{typeName}] - [{watcherDirPath}]\n");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
