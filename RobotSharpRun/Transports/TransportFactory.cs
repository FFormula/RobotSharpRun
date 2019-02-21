namespace RobotSharpRun.Transports
{
    using System.Configuration;

    static class TransportFactory
    {
        public static ITransport Create()
        {
            var config = ConfigurationManager.AppSettings;
            switch (config["transport"])
            {
                case "Ftp":
                            FtpDriver driver = new FtpDriver(
                                config["Ftp.Host"],
                                config["Ftp.User"],
                                config["Ftp.Pass"]);
                            return new Ftp(driver);
                case "Disk":
                default:
                            return new Disk(config["Disk.RobotTasks"]);
            }
        }
    }
}
