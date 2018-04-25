using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository;
using System.Diagnostics;
using log4net.Repository.Hierarchy;
using tNext.Common.Core.Helpers;
using log4net;
using System.Configuration;

namespace tNext.Common.Core.Logging
{
    public class GrayLogAppender
    {
        internal static void CreateGrayLogAppender()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            var graylogAppender = new Gelf4Net.Appender.AsyncGelfUdpAppender();
            graylogAppender.RemoteAddress = System.Net.IPAddress.Parse(ConfigurationHelper.GetConfiguration("GrayLogRemoteAddress"));
            graylogAppender.RemotePort = 12201;
            graylogAppender.Layout = new Gelf4Net.Layout.GelfLayout()
            {
                AdditionalFields = "version:1.0,Level:%level",
                Facility = ConfigurationManager.AppSettings["ApplicationName"].ToString(),
                IncludeLocationInformation = true,
                ConversionPattern = "[%t] %c{1} - %m"
            };

            graylogAppender.Name = "GraylogAppender";
            graylogAppender.ActivateOptions();

            hierarchy.Root.AddAppender(graylogAppender);

            hierarchy.Root.Level = log4net.Core.Level.All;
            hierarchy.Configured = true;
        }

    }

}
