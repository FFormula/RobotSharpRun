namespace RobotSharpRun.Robots
{
    using RobotSharpRun.Services;
    using System.IO;

    static class RobotFactory
    {
        public static ARobot Create(CmdDriver cmd, string runkey)
        {
            return SelectByLangId(cmd, Path.GetExtension(runkey));
        }

        private static ARobot SelectByLangId(CmdDriver cmd, string langId)
        {
            switch (langId)
            {
                case ".java": return new RobotJava(cmd,
                                            Properties.Settings.Default.RobotJavaJavacExe,
                                            Properties.Settings.Default.RobotJavaJavaExe,
                                            Properties.Settings.Default.RobotJavaDenyWords);
                case ".cs": return new RobotSharp(cmd,
                                            Properties.Settings.Default.RobotSharpCscExe,
                                            Properties.Settings.Default.RobotSharpDenyWords);
                default: return new RobotNull(cmd);
            }
        }
    }

}
