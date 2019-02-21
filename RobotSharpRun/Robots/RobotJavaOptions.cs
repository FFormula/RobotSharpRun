namespace RobotSharpRun.Robots
{
    using System.Configuration;

    internal sealed class RobotJavaOptions : IRobotJavaOptions
    {
        public string JavaC => ConfigurationManager.AppSettings["RobotJava.JAVAC"];

        public string Java => ConfigurationManager.AppSettings["RobotJava.JAVA"];

        public string ProgramFileName => "Program.java";

        public string ForbiddenWords => ConfigurationManager.AppSettings["RobotJava.Forbidden"];
    }
}
