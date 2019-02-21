namespace RobotSharpRun.Transports
{
    using System.IO;
    using System.Linq;

    class Disk : ITransport
    {
        public readonly string RobotTasks;

        public Disk(string RobotTasks)
        {
            this.RobotTasks = RobotTasks;
        }

        public string GetNextRunkey()
        {
            try
            {
                return Directory.GetDirectories(
                    Path.Combine(RobotTasks, "wait"))
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
                Path.Combine(RobotTasks, "wait", runkey),
                Path.Combine(toFolder, runkey));
        }

        public void PutDoneFiles(string runkey, string fromFolder)
        {
            Directory.Move(
                Path.Combine(fromFolder, runkey),
                Path.Combine(RobotTasks, "done", runkey));
        }

        public override string ToString()
        {
            return "Disk at " + RobotTasks;
        }
    }
}
