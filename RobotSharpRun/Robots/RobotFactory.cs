namespace RobotSharpRun.Robots
{
    static class RobotFactory
    {

        public static Robot Create(string apikey)
        {
            return SelectByLandId(Path.GetExtension(apikey));
        }

        private static Robot SelectByLandId(string landId)
        {
            switch (langId)
            {
                case ".java": return new RobotJava(
                                            Properties.Settings.Default.RobotJavaJavacExe,
                                            Properties.Settings.Default.RobotJavaJavaExe,
                                            Properties.Settings.Default.RobotJavaDenyWords);
                case ".cs": return new RobotSharp(
                                            Properties.Settings.Default.RobotSharpCscExe,
                                            Properties.Settings.Default.RobotSharpDenyWords);
                default: return new RobotNull();
            }
        }
    }

}
