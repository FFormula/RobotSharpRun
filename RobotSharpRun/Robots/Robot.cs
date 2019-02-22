namespace RobotSharpRun.Robots
{
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;

    abstract class Robot
    {
        protected readonly string RunFolder;
        protected readonly string SourceFile;
        protected readonly string DenyWords;

        protected Robot SetRunFolder(string runFolder)
        {
            this.runFolder = runFolder + (runFolder.EndsWith("\\") ? "" : "\\");
            return this;
        }

        public void Start() // Template Method
        {
            Log.get().Debug("Source:\n" + File.ReadAllText(runFolder + SourceFile));

            if (HasForbiddenWords(runFolder + SourceFile, DenyWords))
                File.WriteAllText(runFolder + "/compiler.out",
                    "Your program contains forbidden words: " + DenyWords);
            else
            if (Compile())
                foreach (string file in Directory.GetFiles(runFolder, "*.in"))
                    RunTest(
                        Path.GetFileName(file),
                        Path.GetFileName(file).Replace(".in", ".out"));

            Log.get().Debug("Compiler:\n" + File.ReadAllText(runFolder + "/compiler.out"));
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
