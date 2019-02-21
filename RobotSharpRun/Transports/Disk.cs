namespace RobotSharpRun.Transports
{
    using System.IO;
    using System.Linq;

    class Disk : ITransport
    {
        public readonly string TasksFolder;

        public Disk(string TasksFolder)
        {
            this.TasksFolder = TasksFolder;
        }

        public string GetNextRunkey()
        {
            try
            {
                return Directory.GetDirectories(
                    Path.Combine(TasksFolder, "wait"))
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
                Path.Combine(TasksFolder, "wait", runkey),
                Path.Combine(toFolder, runkey));
        }

        public void PutDoneFiles(string runkey, string fromFolder)
        {
            Directory.Move(
                Path.Combine(fromFolder, runkey),
                Path.Combine(TasksFolder, "done", runkey));
        }

        public override string ToString()
        {
            return "Disk at " + TasksFolder;
        }
    }
}
