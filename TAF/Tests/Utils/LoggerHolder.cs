using NLog;

namespace Tests.Utils
{
    internal class LoggerHolder
    {
        private static ILogger _instance;

        public static ILogger Logger
        {
            get
            {
                if (_instance is null)
                {
                    _instance = LogManager.GetCurrentClassLogger();
                }

                return _instance;
            }
        }
    }
}
