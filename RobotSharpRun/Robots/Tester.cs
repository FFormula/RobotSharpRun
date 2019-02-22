namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;
    using System.IO;

    public class Tester
    {
        private readonly string RunFolder;
        private readonly ARobot Robot;

        public Tester(string runkey, string WorkFolder)
        {
            RunFolder = WorkFolder + runkey + "\\";
            CmdDriver cmd = new CmdDriver(RunFolder, Properties.Settings.Default.RobotRunTestTimeout);
            Robot = RobotFactory.Create(cmd, runkey);
        }

        public void Start()
        {
            Log.get().Debug("Source:\n" + File.ReadAllText(RunFolder + Robot.SourceFile));

            if (File.Exists(RunFolder + Robot.CompilerOut))
                File.Delete(RunFolder + Robot.CompilerOut);

            if (!CheckDenyWords())
                Robot.Compile();

            if (!IsCompileError() && IsExecFile())
                foreach (string file in Directory.GetFiles(RunFolder, "*.in"))
                    Robot.RunTest(
                        Path.GetFileName(file),
                        Path.GetFileName(file).Replace(".in", ".out"));

            Log.get().Debug("Compiler:\n" + File.ReadAllText(RunFolder + Robot.CompilerOut));
        }

        private bool IsCompileError()
        {
            return 0 < new FileInfo(RunFolder + Robot.CompilerOut).Length;
        }

        private bool IsExecFile()
        {
            return File.Exists(RunFolder + Robot.ExecFile);
        }

        private bool CheckDenyWords()
        {
            bool has = HasDenyWords();
            if (has)
                File.WriteAllText(RunFolder + Robot.CompilerOut,
                    "Your source code contains forbidden words: " + Robot.DenyWords);
            return has;
        }

        private bool HasDenyWords()
        {
            string source = File.ReadAllText(RunFolder + Robot.SourceFile);
            foreach (string word in Robot.DenyWords.Split())
                if (source.Contains(word))
                    return true;
            return false;
        }

    }
}
