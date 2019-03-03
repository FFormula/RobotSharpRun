namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;

    class RobotJava : ARobot
    {
        private readonly string JavacExe;
        private readonly string JavaExe;

        public RobotJava(CmdDriver cmd, string JavacExe, string JavaExe, string DenyWords)
            : base (cmd, 
                  "Program.java", 
                  "Program.class", 
                  DenyWords)
        {
            this.JavacExe = JavacExe;
            this.JavaExe = JavaExe;
        }

        public override void Compile()
        {
            cmd.Run($@"""{JavacExe}"" {SourceFile} 2> {CompilerOut}", "chcp 65001");
        }

        public override void RunTest(string inFile, string outFile)
        {
            cmd.Run($@"""{JavaExe}"" -Dfile.encoding=UTF-8 Program < {inFile} > {outFile} 2>&1", "chcp 65001");
        }

    }
}
