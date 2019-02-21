namespace RobotSharpRun.Application
{
    internal interface IApplicationOptions
    {
        int ProcessDelay { get; }

        string WorkFolder { get; }

        string DiskRobotData { get; }

        string TransportType { get; }
    }
}
