using Backups.Tools;
using Serilog;

namespace BackupsExtra.Logger
{
    public class Logger
    {
        private static ILogger _logger;

        public static void SetLogger(ILogger logger)
        {
            _logger = logger ?? throw new BackupsException("Invalid logger");
        }

        public static void Log(string text)
        {
           _logger.Information(text);
        }
    }
}