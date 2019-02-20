namespace RobotSharpRun.Application
{
    using System.Configuration;

    internal sealed class FtpClientOptions : IFtpClientOptions
    {
        public string Host => ConfigurationManager.AppSettings["Ftp.Host"];

        public string User => ConfigurationManager.AppSettings["Ftp.User"];

        public string Password => ConfigurationManager.AppSettings["Ftp.Pass"];
    }
}
