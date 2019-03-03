namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;

    class RobotSharp : ARobot
    {
        private readonly string CscExe;

        public RobotSharp(CmdDriver cmd, string RunFolder, string CscExe, string DenyWords)
            : base (cmd, RunFolder,
                  "Program.cs", 
                  "Program.exe",
                  DenyWords)
        {
            this.CscExe = CscExe;
        }

        public override void Compile()
        {
            cmd.Run($@"""{CscExe}"" /nologo {SourceFile} > {CompilerOut}", "chcp 855");
        }

        public override void RunTest(string inFile, string outFile)
        {
            cmd.Run($@"{ExecFile} < {inFile} > {outFile} 2>&1", "chcp 65001");
        }
    }
}
