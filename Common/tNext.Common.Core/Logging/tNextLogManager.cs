using log4net;
using System;
using System.Configuration;
using tNext.Common.Core.Logging;

namespace tNext
{
    public class tNextLogManager
    {
        //TODO: Implemen IOC container
        static ILog _logger;

        static tNextLogManager()
        {
            var appender = new log4net.Appender.DebugAppender();
            appender.Name = "DebugLogger";
            log4net.Config.BasicConfigurator.Configure(appender);
            _logger = log4net.LogManager.GetLogger(appender.Name);
        }


        internal static void SetGrayLog()
        {
            //TODO: logger kapatıldı. Bunun replace edilmesi gerek. Graylog düzgün çalışmıyor. 

            return;
            GrayLogAppender.CreateGrayLogAppender();
            _logger = log4net.LogManager.GetLogger("GraylogAppender");
        }

        public static void LogError(Exception ex)
        {
            return;
            Debug(ex.Message);
            _logger.Info(ex);
        }

        public static void Log(string logMessage)
        {
            return;
            Debug(logMessage);
            _logger.Info(logMessage);
        }

        public static void Debug(string logMessage)
        {
            return;
            System.Diagnostics.Debug.WriteLine($"--{ConfigurationManager.AppSettings["ApplicationName"]} : {logMessage}");
            _logger.Debug(logMessage);
        }
    }
}
