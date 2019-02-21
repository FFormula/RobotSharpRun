namespace RobotSharpRun
{
    using System;
    using System.Threading;
    using Transports;
    using Robots;

    internal sealed class Program
    {
        private readonly string WorkFolder; // место где мы размещаем заявки и компилируем их
        private readonly int RequestInterval;
        private readonly ITransport transport;

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
            WorkFolder = Properties.Settings.Default.AppWorkFolder;             Log.get().Info("WorkFolder: " + WorkFolder);
            RequestInterval = Properties.Settings.Default.AppRequestInterval;   Log.get().Info("RequestInterval: " + RequestInterval);
            transport = TransportFactory.Create();                              Log.get().Info("Transport: " + transport);
        }

        private void Work()
        {
            string runkey = null;
            try
            {
                runkey = transport.GetNextRunkey();

                if (runkey == null) return;
                Console.WriteLine();                                    Log.get().Info("Runkey: " + runkey);

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
            }
        }

        private void Ping()
        {
            Console.Write(".");
        }

        private void Delay()
        {
            Thread.Sleep(RequestInterval);
        }
    }
}
