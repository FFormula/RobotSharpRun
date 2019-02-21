namespace RobotSharpRun
{
    using System;
    using System.Threading;
    using System.Configuration;
    using Transports;
    using Robots;

    internal sealed class Program
    {
        private string WorkFolder; // место где мы размещаем заявки и компилируем их
        private int ProcessDelay;
        private Transport transport;

        private static void Main()
        {
            Log.get().Info("Start");
            Program program = new Program();
            program.Process();
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

        private Program()
        { 
            var config = ConfigurationManager.AppSettings;
            WorkFolder = config["WorkFolder"];
            ProcessDelay = Convert.ToInt32(config["ProcessDelay"]);
            Log.get().Info("WorkFolder: " + WorkFolder);
            Log.get().Info("ProcessDelay: " + ProcessDelay);
            CreateTransport();
        }

        private void CreateTransport()
        {
            var config = ConfigurationManager.AppSettings;
            //transport = new Disk(config["Disk.RobotData"]);
            FtpDriver driver = new FtpDriver(
                config["Ftp.Host"],
                config["Ftp.User"],
                config["Ftp.Pass"]);
            transport = new Ftp(driver);
            Log.get().Info("Transport: FTP on " + driver.host);
        }

        private void Work()
        {
            string runkey = null;
            try
            {
                runkey = transport.GetNextRunkey();

                if (runkey == null) return;
                Console.WriteLine();
                Log.get().Info("Runkey: " + runkey);

                // переместить папку runkey из сервера в рабочую директорию
                transport.GetWorkFiles(runkey, WorkFolder);

                Robot robot = Robot.CreateRobot(WorkFolder, runkey);
                robot.Start();

                // переместить файлы из рабочей директории обратно на сервер
                transport.PutDoneFiles(runkey, WorkFolder);
            }
            catch (Exception ex)
            {
                Log.get().Error(ex, ex.Message + "\n" + ex.StackTrace);
                CreateTransport();
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
    }
}
