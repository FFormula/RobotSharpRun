namespace RobotSharpRun.Transports
{
    interface Transport
    {
        string GetNextRunkey();
        void GetWorkFiles(string runkey, string toFolder);
        void PutDoneFiles(string runkey, string fromFolder);
    }
}
