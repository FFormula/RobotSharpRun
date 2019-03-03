namespace RobotSharpRun.Services
{
    using System.Diagnostics;

    class CmdDriver
    {
        private readonly string RunFolder;
        private readonly int RobotRunTimeout;

        public CmdDriver(string RunFolder, int RobotRunTimeout)
        {
            this.RunFolder = RunFolder;
            this.RobotRunTimeout = RobotRunTimeout;
        }

        public void Run(string command, string preCommand = "")
        {
            Log.get().Info("Run: " + RunFolder + "\\" + command);
            Process cmd = new Process();
            cmd.StartInfo.WorkingDirectory = RunFolder;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            if (preCommand != "")
                cmd.StandardInput.WriteLine(preCommand);
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();

            if (!cmd.WaitForExit(RobotRunTimeout))
            {
                Log.get().Info("Timeout");
                cmd.Kill();
            }
        }
    }
}
