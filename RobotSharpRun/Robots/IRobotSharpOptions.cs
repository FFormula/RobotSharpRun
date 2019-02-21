namespace RobotSharpRun.Robots
{
    internal interface IRobotSharpOptions
    {
        string Csc { get; }

        string ProgramFileName { get; }

        string ForbiddenWords { get; }
    }
}
