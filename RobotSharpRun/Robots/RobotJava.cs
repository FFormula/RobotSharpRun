using System.IO;

namespace RobotSharpRun.Robots
{
    class RobotJava : Robot
    {
        string JAVAC;
        string JAVA;

        public RobotJava()
        {
            JAVAC = GetSettings("RobotJava.JAVAC");
            JAVA = GetSettings("RobotJava.JAVA");
            program = "Program.java";
            forbidden = GetSettings("RobotJava.Forbidden");
        }

        protected override bool Compile()
        {
            RunCommand($@"""{JAVAC}"" {program} 2> compiler.out");
            return new FileInfo(runFolder + "compiler.out").Length == 0;
        }

        protected override void RunTest(string inFile, string outFile)
        {
            RunCommand($@"""{JAVA}"" -Dfile.encoding=UTF-8 Program < {inFile} > {outFile} 2>&1");
        }
    }
}
