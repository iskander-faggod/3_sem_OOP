using Backups.Tools;
using Serilog;

namespace BackupsExtra.Logger
{
    public class Logger
    {
        private static ILogger _consoleLogger;

        public static void SetLogger(ILogger consoleLogger)
        {
            _consoleLogger = consoleLogger ?? throw new BackupsException("Invalid consoleLogger");
        }

        public static void Log(string text)
        {
           _consoleLogger.Information(text);
        }
    }
}