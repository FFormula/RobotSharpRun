namespace RobotSharpRun.Robots
{
    internal sealed class RobotJava : Robot, IRobot
    {
        private readonly IRobotJavaOptions options;

        public RobotJava(IRobotJavaOptions options)
        {
            this.options = options;
            this.program = this.options.ProgramFileName;
            this.forbidden = this.options.ForbiddenWords;
        }

        protected override void Compile()
        {
            RunCommand($@"""{this.options.JavaC}"" {program} 2> compiler.out");
        }

        protected override void RunTest(string inFile, string outFile)
        {
            RunCommand($@"""{this.options.Java}"" -Dfile.encoding=UTF-8 Program < {inFile} > {outFile} 2>&1");
        }
    }
}
