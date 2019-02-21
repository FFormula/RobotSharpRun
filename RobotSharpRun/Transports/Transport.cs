namespace RobotSharpRun.Transports
{
    internal interface ITransport
    {
        string GetNextRunkey();

        void GetWorkFiles(string runkey, string toFolder);

        void PutDoneFiles(string runkey, string fromFolder);
    }
}
