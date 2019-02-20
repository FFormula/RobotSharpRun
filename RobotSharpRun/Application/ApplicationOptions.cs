namespace RobotSharpRun.Application
{
    using System;
    using System.Configuration;

    internal sealed class ApplicationOptions : IApplicationOptions
    {
        public int ProcessDelay => Convert.ToInt32(ConfigurationManager.AppSettings["ProcessDelay"]);

        public string WorkFolder => ConfigurationManager.AppSettings["WorkFolder"];

        public string DiskRobotData => ConfigurationManager.AppSettings["Disk.RobotData"];
    }
}
