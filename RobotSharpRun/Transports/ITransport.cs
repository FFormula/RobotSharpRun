namespace RobotSharpRun.Transports
{
    interface ITransport
    {
        string GetNextRunkey();
        void GetWorkFiles(string runkey, string toFolder);
        void PutDoneFiles(string runkey, string fromFolder);
    }
}
