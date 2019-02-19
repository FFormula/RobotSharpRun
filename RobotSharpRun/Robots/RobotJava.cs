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
        }

        protected override bool Compile()
        {
            RunCommand(@"""{JAVAC}"" Program.java 2> compiler.out");
            return new FileInfo(folder + "compiler.out").Length == 0;
        }

        protected override void RunTest(string inFile, string outFile)
        {
            RunCommand(@"""{JAVA}"" Program < " + inFile + " > " + outFile + " 2>&1");
        }
    }
}
