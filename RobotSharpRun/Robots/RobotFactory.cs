namespace RobotSharpRun.Robots
{
    internal sealed class RobotFactory : IRobotFactory
    {
        private readonly IRobotSharpOptions robotSharpOptions;
        private readonly IRobotJavaOptions robotJavaOptions;

        public RobotFactory(
            IRobotSharpOptions robotSharpOptions,
            IRobotJavaOptions robotJavaOptions)
        {
            this.robotSharpOptions = robotSharpOptions;
            this.robotJavaOptions = robotJavaOptions;
        }

        public IRobot Create(string language)
        {
            switch (language)
            {
                case ".java":
                    return new RobotJava(this.robotJavaOptions);
                case ".cs":
                    return new RobotSharp(this.robotSharpOptions);
                default:
                    return new RobotNull();
            }
        }
    }
}
