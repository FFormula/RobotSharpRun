using NLog;

namespace RobotSharpRun
{
    public class Log
    {
        private static NLog.Logger log = null;

        public static NLog.Logger get()
        {
            if (log == null)
                log = LogManager.GetCurrentClassLogger();
            return log;
        }
    }
}
