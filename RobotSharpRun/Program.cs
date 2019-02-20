namespace RobotSharpRun
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Configuration;
    using System.Collections.Generic;
    using Transports;
    using Robots;

    internal sealed class Program
    {
        private string WorkFolder; // место где мы размещаем заявки и компилируем их
        private int ProcessDelay;
        private Transport transport;
        private FtpDriver ftp;

        private static void Main()
        {
            Program program = new Program();
            program.Process();
        }

        private Program()
        { 
            var config = ConfigurationManager.AppSettings;
            WorkFolder = config["WorkFolder"];
            ProcessDelay = Convert.ToInt32(config["ProcessDelay"]);
            transport = new Disk(config["Disk.RobotData"]);

            FtpDriver driver = new FtpDriver(
                config["Ftp.Host"],
                config["Ftp.User"],
                config["Ftp.Pass"]);
            transport = new Ftp(driver);
        }

        private void Process()
        {
            while (true)
            {
                Ping();
                Work();
                Delay();
            }
        }

        private void Work()
        {
            string runkey = transport.GetNextRunkey();
            if (runkey == null) return;
            Console.WriteLine($"\nWorking on {runkey}");

            // переместить папку runkey из сервера в рабочую директорию
            transport.GetWorkFiles(runkey, WorkFolder);

            Robot robot = Robot.CreateRobot(WorkFolder, runkey);
            robot.Start();

            // переместить файлы из рабочей директории обратно на сервер
            transport.PutDoneFiles(runkey, WorkFolder);
        }

        private void Ping()
        {
            Console.Write(".");
        }

        private void Delay()
        {
            Thread.Sleep(ProcessDelay);
        }
    }
}
