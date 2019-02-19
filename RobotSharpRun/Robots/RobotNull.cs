using System.IO;

namespace RobotSharpRun.Robots
{
    class RobotNull : Robot
    {
        protected override bool Compile()
        {
            File.WriteAllText(folder + "compiler.out", "Error: This Programing Language does not supported");
            return false;
        }

        protected override void RunTest(string inFile, string outFile)
        {
            return;
        }
    }
}
