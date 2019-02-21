namespace RobotSharpRun.Transports
{
    using System.IO;

    class Ftp : ITransport
    {
        FtpDriver driver;

        public Ftp(FtpDriver driver)
        {
            this.driver = driver;
        }

        public string GetNextRunkey()
        {
            foreach (string folder in driver.Dir("wait/"))
                if (folder[0] != '.')
                    return folder;
            return null;
        }

        public void GetWorkFiles(string runkey, string toFolder)
        {
            Directory.CreateDirectory(toFolder + runkey);
            driver.DownloadFolder("wait/" + runkey + "/", toFolder + runkey + "\\");
            driver.Rename("wait/" + runkey, "../work/" + runkey);
        }

        public void PutDoneFiles(string runkey, string fromFolder)
        {
            foreach (string fileOut in Directory.GetFiles(fromFolder + runkey, "*.out"))
                driver.Put(fileOut, "work/" + runkey + "/" + Path.GetFileName(fileOut));
            driver.Rename("work/" + runkey, "../done/" + runkey);
        }

        public override string ToString()
        {
            return "Ftp at " + driver.host;
        }
    }
}
