namespace RobotSharpRun.Application
{
    using System;
    using System.IO;
    using System.Threading;
    using Robots;
    using Transports;

    internal sealed class ApplicationService
    {
        private static ApplicationService _instance;

        private bool running;

        private readonly ILogger logger;
        private readonly ITransport transport;
        private readonly IApplicationOptions applicationOptions;
        private readonly IRobotFactory robotFactory;

        public static ApplicationService Create(
            ILogger logger,
            ITransportFactory transportFactory,
            IApplicationOptions applicationOptions,
            IFtpClientOptions ftpClientOptions,
            IRobotFactory robotFactory)
        {
            if (_instance == null)
            {
                _instance = new ApplicationService(
                    logger,
                    transportFactory,
                    applicationOptions,
                    robotFactory);
            }

            return _instance;
        }

        private ApplicationService(
            ILogger logger,
            ITransportFactory transportFactory,
            IApplicationOptions applicationOptions,
            IRobotFactory robotFactory)
        {
            this.logger = logger;
            this.applicationOptions = applicationOptions;
            this.robotFactory = robotFactory;

            this.transport = transportFactory
                .Create(this.applicationOptions.TransportType);

            Log.get().Info("WorkFolder: " + this.applicationOptions.WorkFolder);
            Log.get().Info("ProcessDelay: " + this.applicationOptions.ProcessDelay);
        }

        public void Run()
        {
            if (this.running)
            {
                return;
            }

            this.running = true;

            while (true)
            {
                Ping();
                Work();
                Delay();
            }
        }

        private void Work()
        {
            string runkey = null;

            try
            {
                runkey = this.transport
                    .GetNextRunkey();

                if (runkey == null)
                {
                    return;
                }

                this.logger.Log($"\nWorking on {runkey}");

                // переместить папку runkey из сервера в рабочую директорию
                this.transport.GetWorkFiles(runkey, Path.Combine(this.applicationOptions.DiskRobotData, this.applicationOptions.WorkFolder));

                var language = Path.GetExtension(runkey);
                var robot = this.robotFactory.Create(language);
                var runFolder = Path.Combine(this.applicationOptions.DiskRobotData, this.applicationOptions.WorkFolder, runkey); // TODO: refactor 'work' folder

                robot.Run(runFolder);

                // переместить файлы из рабочей директории обратно на сервер
                this.transport.PutDoneFiles(runkey, Path.Combine(this.applicationOptions.DiskRobotData, this.applicationOptions.WorkFolder));
            }
            catch (Exception ex)
            {
                Log.get().Error(ex, ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Ping()
        {
            this.logger.Log(".");
        }

        private void Delay()
        {
            Thread.Sleep(this.applicationOptions.ProcessDelay);
        }
    }
}
