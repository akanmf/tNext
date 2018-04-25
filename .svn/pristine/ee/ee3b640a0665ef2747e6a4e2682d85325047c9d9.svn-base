using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Core
{
    public class Constants
    {
        public const string IS_STOCK_CONTROL = "1";
        public const string IS_PRICE_CONTROL = "0";

        internal string Log4NetDefaultConfiguration = @"
                          <?xml version=""1.0"" encoding=""utf-8"" ?>
                          <log4net>
                            <root>
                              <level value=""ERROR"" />
                              <level value = ""ALL"" />
                              < appender -ref ref=""GelfUdpAppender"" />
                            </root>
                            <appender name=""AspNetTraceAppender"" type=""log4net.Appender.AspNetTraceAppender"" >
                                <layout type = ""log4net.Layout.PatternLayout"" >
                                    < conversionPattern value=""%date [%thread] %-5level %logger [%property{NDC}] - %message%newline"" />
                                </layout>
                            </appender>
                          </log4net>";
      
    }
}
