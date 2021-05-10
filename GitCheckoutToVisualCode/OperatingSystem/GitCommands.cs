using System.Collections.Generic;

namespace Candal.OperatingSystem
{
    class GitCommands
    {
        #region Fields

        private readonly Candal.OperatingSystem.ShellCommand _shellCmd;

        #endregion

        #region Constructors

        public GitCommands(Candal.OperatingSystem.ShellCommand shellCommand)
        {
            _shellCmd = shellCommand;
        }

        #endregion

        #region Public Methods
        
        //public void RegisterEvents(ShellDataEventHandler shellDataEventHandler)
        //{
        //    _shellCmd.RegisterEvents(shellDataEventHandler);
        //}

        public string RepositoryName(string gitURL)
        {
            string repoName = "";

            if (gitURL != "")
            {
                int posI = gitURL.LastIndexOf("/") + 1;
                int posE = gitURL.LastIndexOf(".git");
                if (posI > 0 && posE > 0 && posI < posE)
                    repoName = gitURL.Substring(posI, posE - posI);
            }

            return repoName;
        }

        public List<string> ListBranches(string rootFolder, string repositoryName)
        {
            List<string> branchList = new List<string>();

            string fullFolderName = $"{rootFolder}\\{repositoryName}";

            string command = @"git branch --list --remote";
            ShellData output = _shellCmd.ExecuteCommand(command, fullFolderName, true);

            if (output.ExitCode != 0)
                return branchList;

            foreach (ShellDataElement element in output.ElementList)
            {
                if (element.Code == ShellElementCode.OutputData && element.Data != null)
                {
                    string temp = element.Data.Replace("origin/", "").Replace("HEAD", "").Replace("->", "").Trim();
                    branchList.Add(temp);
                }
            }

            return branchList;
        }

        public ShellData Clone(string giRepositorytURL, string rootFolder)
        {
            string command = $"git clone \"{giRepositorytURL}\"";
            ShellData output = _shellCmd.ExecuteCommand(command, rootFolder);

            return output;
        }

        public ShellData Checkout(string rootFolder, string repositoryName, string branch)
        {
            ShellData output;

            string repositoryFolder = $"{rootFolder}\\{repositoryName}";

            if (System.IO.Directory.Exists(repositoryFolder))
            {
                string command = $"git checkout \"{branch}\"";
                output = _shellCmd.ExecuteCommand(command, repositoryFolder);
            }
            else
                output = GenerateProcessDataReceived(ShellElementCode.ErrorData, "Repository Folder not exists.");

            return output;
        }

        public ShellData RemoveLocalRepository(string rootFolder, string repositoryName)
        {
            ShellData output = null;

            string repositoryFolder = $"{rootFolder}\\{repositoryName}";

            if (System.IO.Directory.Exists(repositoryFolder))
            {
                string command = $"RMDIR  /S /Q \"{repositoryFolder}\"";
                output = _shellCmd.ExecuteCommand(command, rootFolder);
            }
            else
                output = GenerateProcessDataReceived(ShellElementCode.OutputData, "Repository Folder not exists.");

            return output;
        }

        public ShellData CreateLocalRootFolder(string rootFolder)
        {
            ShellData output = null;

            if (!System.IO.Directory.Exists(rootFolder))
            {
                string command = $"MKDIR \"{rootFolder}\"";
                output = _shellCmd.ExecuteCommand(command);
            }
            else
                output = GenerateProcessDataReceived(ShellElementCode.OutputData, "Root Folder exists.");

            return output;
        }

        public ShellData VisualCodeStart(string folder)
        {
            ShellData output;

            if (System.IO.Directory.Exists(folder))
            {
                string command = $"code \"{folder}\"";
                output = _shellCmd.ExecuteCommand(command);
            }
            else
                output = GenerateProcessDataReceived(ShellElementCode.ErrorData, "Folder not exists.");

            return output;
        }

        #endregion

        #region Private Methods

        private ShellData GenerateProcessDataReceived(ShellElementCode code, string data)
        {
            ShellData output = new ShellData();
            output.Add(code, data);

            return output;
        }

        #endregion
    }
}
