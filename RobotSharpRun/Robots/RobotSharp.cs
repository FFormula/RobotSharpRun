using System.IO;

namespace RobotSharpRun.Robots
{
    class RobotSharp : Robot
    {
        string CSC;

        public RobotSharp()
        {
            CSC = GetSettings("RobotSharp.CSC");
        }

        protected override bool Compile()
        {
            RunCommand(@"""{CSC}"" /nologo Program.cs > compiler.out");
            return new FileInfo(folder + "compiler.out").Length == 0;
        }

        protected override void RunTest(string inFile, string outFile)
        {
            RunCommand(@"Program.exe < " + inFile + " > " + outFile + " 2>&1");
        }
    }
}
