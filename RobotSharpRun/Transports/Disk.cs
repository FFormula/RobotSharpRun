namespace RobotSharpRun.Transports
{
    using System.IO;
    using System.Linq;

    internal sealed class Disk : ITransport
    {
        private string RobotData;

        public Disk(string RobotData)
        {
            this.RobotData = RobotData;
        }

        public string GetNextRunkey()
        {
            try
            {
                return Directory.GetDirectories(
                    Path.Combine(RobotData, "wait"))
                        .Select(Path.GetFileName)
                        .First();
            }
            catch
            {
                return null;
            }
        }

        public void GetWorkFiles(string runkey, string toFolder)
        {
            Directory.Move(
                Path.Combine(RobotData, "wait", runkey),
                Path.Combine(toFolder, runkey));
        }

        public void PutDoneFiles(string runkey, string fromFolder)
        {
            Directory.Move(
                Path.Combine(fromFolder, runkey),
                Path.Combine(RobotData, "done", runkey));
        }
    }
}
