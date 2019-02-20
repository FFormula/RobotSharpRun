namespace RobotSharpRun
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using Transports;
    using Application;
    using Robots;

    internal sealed class Program
    {
        private string WorkFolder; // место где мы размещаем заявки и компилируем их

        private Transport transport;
        private FtpDriver ftp;
        private readonly IApplicationOptions applicationOptions;
        private readonly IFtpClientOptions ftpClientOptions;

        private static void Main()
        {
            Program program = new Program();
            program.Process();
        }

        private Program()
        {
            this.applicationOptions = new ApplicationOptions();
            this.ftpClientOptions = new FtpClientOptions();
            WorkFolder = this.applicationOptions.WorkFolder;

            transport = new Disk(this.applicationOptions.DiskRobotData);

            FtpDriver driver = new FtpDriver(
                this.ftpClientOptions.Host,
                this.ftpClientOptions.User,
                this.ftpClientOptions.Password);
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
            Thread.Sleep(this.applicationOptions.ProcessDelay);
        }
    }
}
