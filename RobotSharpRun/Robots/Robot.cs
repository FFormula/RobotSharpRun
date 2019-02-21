namespace RobotSharpRun.Robots
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    internal abstract class Robot
    {
        private string runFolder;

        protected string program;
        protected string forbidden;

        public void Run(string runFolder) // Template Method
        {
            this.runFolder = runFolder;

            if (HasForbiddenWords(Path.Combine(runFolder, program), forbidden))
            {
                File.WriteAllText(Path.Combine(runFolder, "compiler.out"), $"Your program contains forbidden words: {forbidden}");
            }
            else
            {
                Compile();

                if (new FileInfo(Path.Combine(runFolder, "compiler.out")).Length == 0)
                {
                    foreach (var file in Directory.GetFiles(runFolder, "*.in"))
                    {
                        Log.get().Debug("Program:\n" + File.ReadAllText(runFolder + program));

                        RunTest(
                            Path.GetFileName(file),
                            Path.GetFileName(file).Replace(".in", ".out"));
                    }
                }
            }

        }

        protected void RunCommand(string command)
        {
            Log.get().Info("RunCommand: " + command);

            var cmd = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = this.runFolder,
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            cmd.Start();

            cmd.StandardInput.WriteLine("chcp 65001");
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();

            if (!cmd.WaitForExit(3000))
            {
                Log.get().Info("Timeout");
                cmd.Kill();
            }
        }

        protected bool HasForbiddenWords(string filename, string forbidden)
        {
            var source = File.ReadAllText(filename);

            return forbidden.Split()
                .Any(word => source.Contains(word));
        }

        protected abstract void Compile();

        protected abstract void RunTest(string inFile, string outFile);
    }
}
