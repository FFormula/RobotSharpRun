namespace RobotSharpRun.Transports
{
    internal interface ITransportFactory
    {
        ITransport Create(string type);
    }
}
