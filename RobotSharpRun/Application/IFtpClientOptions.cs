namespace RobotSharpRun.Application
{
    internal interface IFtpClientOptions
    {
        string Host { get; }

        string User { get; }

        string Password { get; }
    }
}
