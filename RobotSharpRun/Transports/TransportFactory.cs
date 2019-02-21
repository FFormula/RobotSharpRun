namespace RobotSharpRun.Transports
{
    using Application;

    internal sealed class TransportFactory : ITransportFactory
    {
        private readonly IApplicationOptions applicationOptions;
        private readonly IFtpClientOptions ftpClientOptions;

        public TransportFactory(
            IApplicationOptions applicationOptions,
            IFtpClientOptions ftpClientOptions)
        {
            this.applicationOptions = applicationOptions;
            this.ftpClientOptions = ftpClientOptions;
        }

        public ITransport Create(string type)
        {
            switch (type)
            {
                case "Ftp":
                    var driver = new FtpDriver(
                        this.ftpClientOptions.Host,
                        this.ftpClientOptions.User,
                        this.ftpClientOptions.Password);

                    return new Ftp(driver);
                default:
                    return new Disk(this.applicationOptions.DiskRobotData);
            }
        }
    }
}
