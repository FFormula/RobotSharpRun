namespace RobotSharpRun.Robots
{
    internal interface IRobotJavaOptions
    {
        string JavaC { get; }

        string Java { get; }

        string ProgramFileName { get; }

        string ForbiddenWords { get; }
    }
}
