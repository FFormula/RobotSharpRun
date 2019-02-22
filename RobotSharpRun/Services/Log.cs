namespace RobotSharpRun.Services
{
    using NLog;

    public class Log
    {
        private static Logger log = null;

        public static Logger get()
        {
            if (log == null)
                log = LogManager.GetCurrentClassLogger();
            return log;
        }
    }
}
