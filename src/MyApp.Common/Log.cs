using System;
using MyApp.Common.Extensions;

namespace MyApp.Common
{
    public class Log
    {
        public static void Debug(string format, params object[] args)
        {
            Write("DBG", format, args);
        }

        public static void Info(string format, params object[] args)
        {
            Write("INF", format, args);
        }

        public static void Warning(string format, params object[] args)
        {
            Write("WRN", format, args);
        }

        public static void Error(string format, params object[] args)
        {
            Write("ERR", format, args);
        }

        public static void Error(Exception ex, bool messagesOnly = false)
        {
            Write("ERR", ex.LogFormat(messagesOnly));
        }

        private static void Write(string logLevel, string format, params object[] args)
        {
            Console.WriteLine("{0} {1} | {2}",
                DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                logLevel,
                string.Format(format, args));
        }
    }
}
