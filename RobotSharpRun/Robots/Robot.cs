using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace RobotSharpRun.Robots
{
    abstract class Robot
    {
        protected string runFolder;

        public static Robot CreateRobot(string workFolder, string apikey)
        {
            return SelectRobot(Path.GetExtension(apikey)).setRunFolder(workFolder + apikey);
        }

        protected Robot setRunFolder(string runFolder)
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
            if (Compile())
                foreach (string file in Directory.GetFiles(runFolder, "*.in"))
                    RunTest(
                        Path.GetFileName(file),
                        Path.GetFileName(file).Replace(".in", ".out"));
            else
                Console.WriteLine(File.ReadAllText(runFolder + "/compiler.out"));
        }

        protected void RunCommand(string command)
        {
            Console.WriteLine("# " + command);
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
            cmd.WaitForExit();
        }

        abstract protected bool Compile();

        abstract protected void RunTest(string inFile, string outFile);
    }
}
