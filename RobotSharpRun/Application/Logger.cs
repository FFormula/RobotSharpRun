namespace RobotSharpRun.Application
{
    using System;

    internal sealed class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.Write(message);
        }
    }
}
