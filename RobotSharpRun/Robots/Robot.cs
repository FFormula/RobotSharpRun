namespace RobotSharpRun.Robots
{
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;

    abstract class Robot
    {
        protected string runFolder;
        protected string program;
        protected string forbidden;

        public static Robot CreateRobot(string workFolder, string apikey)
        {
            return SelectRobot(Path.GetExtension(apikey)).SetRunFolder(workFolder + apikey);
        }

        protected Robot SetRunFolder(string runFolder)
        {
            this.runFolder = runFolder + (runFolder.EndsWith("\\") ? "" : "\\");
            return this;
        }

        protected string GetSettings(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        private static Robot SelectRobot(string langId)
        {
            switch (langId)
            {
                case ".java": return new RobotJava();
                case ".cs": return new RobotSharp();
                default: return new RobotNull();
            }
        }

        public void Start() // Template Method
        {
            Log.get().Debug("Program:\n" + File.ReadAllText(runFolder + program));

            if (HasForbiddenWords(runFolder + program, forbidden))
                File.WriteAllText(runFolder + "/compiler.out",
                    "Your program contains forbidden words: " + forbidden);
            else
            if (Compile())
                foreach (string file in Directory.GetFiles(runFolder, "*.in"))
                    RunTest(
                        Path.GetFileName(file),
                        Path.GetFileName(file).Replace(".in", ".out"));

            Log.get().Debug("Compiler:\n" + File.ReadAllText(runFolder + "/compiler.out"));
        }

        protected void RunCommand(string command)
        {
            Log.get().Info("RunCommand: " + command);
            Process cmd = new Process();
            cmd.StartInfo.WorkingDirectory = runFolder;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
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
            string source = File.ReadAllText(filename);
            foreach (string word in forbidden.Split())
                if (source.Contains(word))
                    return true;
            return false;
        }

        abstract protected bool Compile();

        abstract protected void RunTest(string inFile, string outFile);
    }
}
