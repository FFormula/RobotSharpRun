namespace RobotSharpRun.Robots
{
    using System.Configuration;

    internal sealed class RobotSharpOptions : IRobotSharpOptions
    {
        public string Csc => ConfigurationManager.AppSettings["RobotSharp.CSC"];

        public string ProgramFileName => "Program.cs";

        public string ForbiddenWords => ConfigurationManager.AppSettings["RobotSharp.Forbidden"];
    }
}
