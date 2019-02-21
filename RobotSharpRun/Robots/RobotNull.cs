namespace RobotSharpRun.Robots
{
    using System.IO;

    internal sealed class RobotNull : IRobot
    {
        public void Run(string runFolder)
        {
            File.WriteAllText(Path.Combine(runFolder, "compiler.out"), "Error: This Programing Language does not supported");
        }
    }
}
