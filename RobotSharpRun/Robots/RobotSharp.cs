namespace RobotSharpRun.Robots
{
    internal sealed class RobotSharp : Robot, IRobot
    {
        private readonly IRobotSharpOptions options;

        public RobotSharp(IRobotSharpOptions options)
        {
            this.options = options;
            this.program = this.options.ProgramFileName;
            this.forbidden = this.options.ForbiddenWords;
        }

        protected override void Compile()
        {
            RunCommand($@"""{this.options.Csc}"" /nologo {program} > compiler.out");
        }

        protected override void RunTest(string inFile, string outFile)
        {
            RunCommand($@"Program.exe < {inFile} > {outFile} 2>&1");
        }
    }
}
