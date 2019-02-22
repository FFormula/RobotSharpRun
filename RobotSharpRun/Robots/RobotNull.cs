namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;

    class RobotNull : ARobot
    {
        public RobotNull(CmdDriver cmd) : base(cmd, "", "", "") { }

        public override void Compile()
        {
            cmd.Run("echo 'Error: This Programing Language does not supported' > compiler.out");
        }

        public override void RunTest(string inFile, string outFile) { }
    }
}
