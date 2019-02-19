using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace RobotSharpRun.Robots
{
    abstract class Robot
    {
        protected string folder;

        public static Robot CreateRobot(string folder)
        {
            return SelectRobot(Path.GetExtension(folder)).setFolder(folder);
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

        protected Robot setFolder (string folder)
        {
            this.folder = folder + (folder.EndsWith("\\") ? "" : "\\");
            return this;
        }

        public void Start() // Template Method
        {
            if (Compile())
                foreach (string file in Directory.GetFiles(folder, "*.in"))
                    RunTest(
                        Path.GetFileName(file),
                        Path.GetFileName(file).Replace(".in", ".out"));
            else
                Console.WriteLine(File.ReadAllText(folder + "/compiler.out"));
        }

        protected void RunCommand(string command)
        {
            Console.WriteLine("# " + command);
            Process cmd = new Process();
            cmd.StartInfo.WorkingDirectory = folder;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }

        abstract protected bool Compile();

        abstract protected void RunTest(string inFile, string outFile);
    }
}
