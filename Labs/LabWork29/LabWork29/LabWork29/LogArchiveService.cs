using Cronos;
using System.IO.Compression;

namespace LabWork29
{
    public class LogArchiveService : BackgroundService
    {
        private object _lock = new object();

        private readonly ILogger<LogArchiveService> _logger;
        private readonly IConfiguration _configuration;

        private string _logsFile;
        private string _archiveDir;

        public LogArchiveService(ILogger<LogArchiveService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var data = _configuration.GetSection("Logging:FileArchive");

            _archiveDir = data["ArchiveDir"] ?? Path.Combine(AppContext.BaseDirectory, "ArchiveFolder");
            _logsFile = data["LogsFile"] ?? Path.Combine(AppContext.BaseDirectory, "LogFolder", "logsFile.log");

            if (!Directory.Exists(_archiveDir))
                Directory.CreateDirectory(_archiveDir);

            if (!File.Exists(_logsFile))
                File.Create(_logsFile);

            //"*/5 * * * *",
            //"* 8 */1 * *",
            //"* 18 * * FRI",
            //"* * */10 * *",

            string currentCronExpression = data["Schedule"] ?? "*/5 * * * *";
            var cronExpression = CronExpression.Parse(currentCronExpression);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var nextOccurrence = cronExpression.GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Local);

                    if (nextOccurrence.HasValue)
                    {
                        var delay = nextOccurrence.Value - DateTime.UtcNow;
                        if (delay > TimeSpan.Zero)
                        {
                            await Task.Delay(delay, stoppingToken); 
                        }
                        await ArchiveLogFileAsync();
                    }
                    else
                        await Task.Delay(10000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                await Task.Delay(10000, stoppingToken);
            }
            await Task.CompletedTask;
        }

        private async Task ArchiveLogFileAsync()
        {
            try
            {
                if (!File.Exists(_logsFile))
                {
                    _logger.LogWarning($"Файл не найден!");
                    return;
                }

                string newZipFilePath = Path.Combine(_archiveDir, $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}.zip");

                using (var zip = ZipFile.Open(newZipFilePath, ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(_logsFile, Path.GetFileName(_logsFile));
                }
                await File.WriteAllTextAsync(_logsFile, string.Empty);

                _logger.LogInformation($"Логи успешно заархивированы в {newZipFilePath}!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
