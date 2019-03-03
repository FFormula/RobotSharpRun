namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;
    using System.IO;

    static class RobotFactory
    {
        public static ARobot Create(CmdDriver cmd, string runkey, string RunFolder)
        {
            return SelectByLangId(cmd, Path.GetExtension(runkey), RunFolder);
        }

        private static ARobot SelectByLangId(CmdDriver cmd, string langId, string RunFolder)
        {
            switch (langId)
            {
                case ".java": return new RobotJava(cmd, RunFolder,
                                    Properties.Settings.Default.RobotJavaJavacExe,
                                    Properties.Settings.Default.RobotJavaJavaExe,
                                    Properties.Settings.Default.RobotJavaDenyWords);
                case ".cs":
                    return new RobotSharp(cmd, RunFolder,
                                    Properties.Settings.Default.RobotSharpCscExe,
                                    Properties.Settings.Default.RobotSharpDenyWords);
                case ".cpp":
                    return new RobotCpp(cmd, RunFolder,
                                    Properties.Settings.Default.RobotCppGccExe,
                                    Properties.Settings.Default.RobotCppDenyWords);
                case ".pas":
                    return new RobotPascal(cmd, RunFolder,
                                    Properties.Settings.Default.RobotPascalFpcExe,
                                    Properties.Settings.Default.RobotPascalDenyWords);
                default: return new RobotNull(cmd);
            }
        }
    }

}
