namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;

    class RobotCpp : ARobot
    {
        private readonly string GccExe;

        public RobotCpp(CmdDriver cmd, string RunFolder, string GccExe, string DenyWords)
            : base(cmd, RunFolder,
                  "Program.cpp",
                  "Program.exe",
                  DenyWords)
        {
            this.GccExe = GccExe;
        }

        public override void Compile()
        {
            cmd.Run($@"""{GccExe}"" {SourceFile} -o {ExecFile} 2> {CompilerOut}");
        }

        public override void RunTest(string inFile, string outFile)
        {
            cmd.Run($@"{ExecFile} < {inFile} > {outFile} 2>&1");
        }

    }
}
