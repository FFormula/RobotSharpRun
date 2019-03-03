namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;
    using System.IO;

    class RobotPascal : ARobot
    {
        private readonly string FpcExe;

        public RobotPascal(CmdDriver cmd, string RunFolder, string FpcExe, string DenyWords)
            : base(cmd, RunFolder,
                  "Program.pas",
                  "Program.exe",
                  DenyWords)
        {
            this.FpcExe = FpcExe;
        }

        public override void Compile()
        {
            cmd.Run($@"""{FpcExe}"" {SourceFile} > {CompilerOut}");
            if (!File.ReadAllText(RunFolder + CompilerOut).Contains("error"))
                File.WriteAllText(RunFolder + CompilerOut, "");
        }

        public override void RunTest(string inFile, string outFile)
        {
            cmd.Run($@"""{ExecFile}"" < {inFile} > {outFile} 2>&1");
        }

    }
}
