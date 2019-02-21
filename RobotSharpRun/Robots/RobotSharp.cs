using System.IO;

namespace RobotSharpRun.Robots
{
    class RobotSharp : Robot
    {
        string CSC;

        public RobotSharp()
        {
            CSC = GetSettings("RobotSharp.CSC");
            program = "Program.cs";
            forbidden = GetSettings("RobotSharp.Forbidden");
        }

        protected override bool Compile()
        {
            RunCommand($@"""{CSC}"" /nologo {program} > compiler.out");
            return new FileInfo(runFolder + "compiler.out").Length == 0;
        }

        protected override void RunTest(string inFile, string outFile)
        {
            RunCommand($@"Program.exe < {inFile} > {outFile} 2>&1");
        }
    }
}
