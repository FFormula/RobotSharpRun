namespace RobotSharpRun.Robots
{
    using System.IO;

    class RobotSharp : Robot
    {
        private readonly string CscExe;

        public RobotSharp(string CscExe, string DenyWords)
        {
            this.CscExe = CscExe;
            SourceFile = "Program.cs";
            this.DenyWords = DenyWords;
        }

        protected override bool Compile()
        {
            RunCommand($@"""{CscExe}"" /nologo {SourceFile} > compiler.out");
            return new FileInfo(runFolder + "compiler.out").Length == 0;
        }

        protected override void RunTest(string inFile, string outFile)
        {
            RunCommand($@"Program.exe < {inFile} > {outFile} 2>&1");
        }
    }
}
