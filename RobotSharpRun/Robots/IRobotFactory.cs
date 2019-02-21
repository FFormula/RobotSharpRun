namespace RobotSharpRun.Robots
{
    internal interface IRobotFactory
    {
        IRobot Create(string language);
    }
}
