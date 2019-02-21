namespace RobotSharpRun
{
    using Application;
    using Robots;
    using Transports;

    internal sealed class Program
    {
        private static void Main()
        {
            Log.get().Info("Start");

            var service = ApplicationService.Create(
                new Logger(),
                new TransportFactory(
                    new ApplicationOptions(),
                    new FtpClientOptions()),
                new ApplicationOptions(),
                new FtpClientOptions(),
                new RobotFactory(
                    new RobotSharpOptions(),
                    new RobotJavaOptions()));

            service.Run();
        }
    }
}
