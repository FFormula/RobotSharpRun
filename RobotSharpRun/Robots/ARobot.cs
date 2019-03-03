namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;

    abstract class ARobot
    {
        protected readonly CmdDriver cmd;
        public readonly string RunFolder;
        public readonly string CompilerOut = "compiler.out";
        public readonly string SourceFile;
        public readonly string ExecFile;
        public readonly string DenyWords;

        public ARobot(CmdDriver cmd, string RunFolder, string SourceFile, string ExecFile, string DenyWords)
        {
            this.cmd = cmd;
            this.RunFolder = RunFolder;
            this.SourceFile = SourceFile;
            this.ExecFile = ExecFile;
            this.DenyWords = DenyWords;
        }

        abstract public void Compile();
        abstract public void RunTest(string inFile, string outFile);
    }
}
