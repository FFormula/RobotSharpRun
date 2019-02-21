namespace RobotSharpRun.Transports
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    class FtpDriver
    {
        string host;
        string user;
        string pass;

        FtpWebRequest request;

        public FtpDriver(string host, string user, string pass)
        {
            this.host = host;
            this.user = user;
            this.pass = pass;
        }

        private void connect(string filename, string method)
        {
            request = (FtpWebRequest)WebRequest.Create(host + filename);
            request.Credentials = new NetworkCredential(user, pass);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true;
            request.Method = method;
        }

        public List<string> Dir(string folder)
        {
            connect(folder, WebRequestMethods.Ftp.ListDirectory);
            List<string> folders = new List<string>();
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                while (!reader.EndOfStream)
                    folders.Add(reader.ReadLine());
            return folders;
        }

        public void Get(string fromFtpFilename, string toLocalFilename)
        {
            connect(fromFtpFilename, WebRequestMethods.Ftp.DownloadFile);
            using (Stream reader = request.GetResponse().GetResponseStream())
            using (Stream writer = File.Create(toLocalFilename))
                reader.CopyTo(writer);
        }

        public void Put(string fromLocalFilename, string toFtpFilename)
        {
            try
            {
                connect(toFtpFilename, WebRequestMethods.Ftp.UploadFile);
                using (Stream reader = File.OpenRead(fromLocalFilename))
                using (Stream writer = request.GetRequestStream())
                    reader.CopyTo(writer);
            } catch { }
        }

        public void MkDir(string folder)
        {
            connect(folder, WebRequestMethods.Ftp.MakeDirectory);
            using ((FtpWebResponse)request.GetResponse())
                return;
        }

        public void Del(string filename)
        {
            connect(filename, WebRequestMethods.Ftp.DeleteFile);
            using ((FtpWebResponse)request.GetResponse())
                return;
        }

        public void RmDir(string folder)
        {
            connect(folder, WebRequestMethods.Ftp.RemoveDirectory);
            using ((FtpWebResponse)request.GetResponse())
                return;
        }

        public void Rename(string oldFilename, string newFilename)
        {
            connect(oldFilename, WebRequestMethods.Ftp.Rename);
            request.RenameTo = newFilename;
            using ((FtpWebResponse)request.GetResponse())
                return;
        }

        public void DownloadFolder(string folder, string to)
        {
            foreach (string file in Dir(folder))
            {
                if (file[0] == '.') continue;
                Get(folder + file, to + file);
            }
        }

    }
}
