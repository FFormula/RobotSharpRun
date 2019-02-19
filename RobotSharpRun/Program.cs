namespace RobotSharpRun
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Configuration;
    using System.Collections.Generic;
    using Robots;

    internal sealed class Program
    {
        private string BasePath;
        private const string WaitDirectoryName = @"wait";
        private const string WorkDirectoryName = @"work";
        private const string DoneDirectoryName = @"done";
        private int ProcessDelay;

        private static FTP ftp;

        private static void Main()
        {
            Program program = new Program();
            program.Process();
        }

        private Program()
        { 
            var config = ConfigurationManager.AppSettings;
            BasePath = config["BasePath"];
            ProcessDelay = Convert.ToInt32(config["ProcessDelay"]);
            ftp = new FTP(config["Ftp.Host"], config["Ftp.User"], config["Ftp.Pass"]);
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
            foreach (var folder in GetFolders())
            {
                Console.WriteLine();
                Console.WriteLine($"Working on {folder}");

                MoveFolder(folder, WaitDirectoryName, WorkDirectoryName);
                Robot robot = Robot.CreateRobot(Path.Combine(BasePath, WorkDirectoryName, folder));

                robot.Start();

                MoveFolder(folder, WorkDirectoryName, DoneDirectoryName);
            }
        }

        private void Ping()
        {
            Console.Write(".");
        }

        private void Delay()
        {
            Thread.Sleep(ProcessDelay);
        }

        private IEnumerable<string> GetFolders()
        {
            return Directory.GetDirectories(
                Path.Combine(BasePath, WaitDirectoryName))
                    .Select(Path.GetFileName);
        }

        private void MoveFolder(string folder, string from, string to)
        {
            Directory.Move(
                Path.Combine(BasePath, from, folder), 
                Path.Combine(BasePath, to, folder));
        }
    }
}
