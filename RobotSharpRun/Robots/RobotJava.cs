namespace RobotSharpRun.Robots
{
    using System.IO;

    class RobotJava : Robot
    {
        private readonly string JavacExe;
        private readonly string JavaExe;

        public RobotJava(string JavacExe, string JavaExe, string DenyWords)
        {
            this.JavacExe = JavacExe;
            this.JavaExe = JavaExe;
            this.DenyWords = DenyWords;
            this.SourceFile = "Program.java";
        }

        protected override bool Compile()
        {
            RunCommand($@"""{JavacExe}"" {SourceFile} 2> compiler.out");
            return new FileInfo(runFolder + "compiler.out").Length == 0;
        }

        protected override void RunTest(string inFile, string outFile)
        {
            RunCommand($@"""{JavaExe}"" -Dfile.encoding=UTF-8 Program < {inFile} > {outFile} 2>&1");
        }
    }
}
