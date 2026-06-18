namespace LabWork29
{
    public class FileWathcerService : BackgroundService
    {
        private readonly ILogger<FileWathcerService> _logger;
        private readonly IConfiguration _configuration;

        private string _logsFile;
        private string _watchDir;

        private object _lock = new object();

        public FileWathcerService(ILogger<FileWathcerService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            var data = _configuration.GetSection("Logging:FileWatcherSettings");

            _watchDir = data["WatchDir"] ?? throw new ArgumentNullException(nameof(_watchDir));
            _logsFile = data["LogFile"] ?? throw new ArgumentNullException(nameof(_logsFile));

            if (!Directory.Exists(_watchDir))
                Directory.CreateDirectory(_watchDir);

            if (!File.Exists(_logsFile))
                File.Create(_logsFile);
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
                await Task.Delay(200, stoppingToken);
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
                _logger.LogError(ex.Message);
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
                    "CREATED" => $"СОЗДАН каталог: [{watcherDirPath}]",
                    "CHANGED" => $"ИЗМЕНЁН каталог: [{watcherDirPath}]",
                    "DELETED" => $"УДАЛЁН каталог: [{watcherDirPath}]",
                    "RENAMED" => $"ПЕРЕИМЕНОВАН каталог: [{watcherDirPath}]",

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
