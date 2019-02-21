namespace RobotSharpRun.Transports
{
    static class TransportFactory
    {
        public static ITransport Create()
        {
            switch (Properties.Settings.Default.TransportClass)
            {
                case "Ftp":
                            return new Ftp(new FtpDriver(
                                    Properties.Settings.Default.TransportFtpHost,
                                    Properties.Settings.Default.TransportFtpUser,
                                    Properties.Settings.Default.TransportFtpPass));
                case "Disk":
                default:
                            return new Disk(Properties.Settings.Default.TransportDiskTasksFolder);
            }
        }
    }
}
