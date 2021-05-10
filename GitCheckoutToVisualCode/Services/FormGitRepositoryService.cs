using System;
using System.Collections.Generic;
using Candal.OperatingSystem;


namespace Candal.Services
{
    public class GitBranchesInfo
    {
        public string Current { get; set; }
        public List<string> List{ get; set; }
    }

    public class FormGitRepositoryService
    {
        #region Events

        private event ShellDataEventHandler ShellDataEvent;

        #endregion

        #region Fields

        private readonly Candal.OperatingSystem.ShellCommand _shellCmd;
        private readonly Candal.OperatingSystem.GitCommands _gitCmds;

        #endregion

        #region Constructors

        public FormGitRepositoryService()
        {
            _shellCmd = new Candal.OperatingSystem.ShellCommand();
            _gitCmds = new Candal.OperatingSystem.GitCommands(_shellCmd);
        }

        #endregion

        #region Public Methods

        //public void RegisterEvents()
        //{
        //    _shellCmd.RegisterEvents(ShellDataEvent);
        //}

        public void RegisterEvents(ShellDataEventHandler shellDataEventHandle)
        {
            ShellDataEvent += shellDataEventHandle;
            _shellCmd.RegisterEvents(shellDataEventHandle);
        }

        public void VisualCodeStart(string gitURL, string rootFolder, string branch)
        {
            ShellDataEvent(ShellElementCode.BeginJob, null);

            string repositoryName = _gitCmds.RepositoryName(gitURL);
            string fullFolderMame = $"{rootFolder}\\{repositoryName}";

            if (!System.IO.Directory.Exists(fullFolderMame))
                throw new Exception($"Folder [{fullFolderMame}] not found.");

            ShellData output = _gitCmds.Checkout(rootFolder, repositoryName, branch);

            if (output.ExitCode == 0)
                output = _gitCmds.VisualCodeStart(fullFolderMame);

            ShellDataEvent(ShellElementCode.EndJob, null);
        }

        public GitBranchesInfo GetBranches(string gitURL, string rootFolder)
        {
            ShellDataEvent(ShellElementCode.BeginJob, null);

            string repositoryName = _gitCmds.RepositoryName(gitURL);
            List<string> branchList = _gitCmds.ListBranches(rootFolder, repositoryName);

            GitBranchesInfo gitBranchesInfo = new GitBranchesInfo();
            gitBranchesInfo.Current = branchList[0];
            branchList.RemoveAt(0);
            gitBranchesInfo.List = branchList;

            ShellDataEvent(ShellElementCode.EndJob, null);

            return gitBranchesInfo;
        }

        public void Clone(string gitURL, string rootFolder)
        {
            Clone(gitURL, rootFolder, null, null);
        }

        //D:\aa>git clone "https://tarzanzito:PASS@gitlab.com/tarzanzito/MusicCollection.git"
        //Cloning into 'MusicCollection'...
        //remote: Enumerating objects: 542, done.
        //remote: Counting objects: 100% (542/542), done.
        //remote: Compressing objects: 100% (264/264), done.
        //Rremote: Total 1042 (delta 230), reused 475 (delta 185), pack-reused 500        Receiving objects:  96% (1001/1042)
        //Receiving objects: 100% (1042/1042), 1.92 MiB | 12.86 MiB/s, done.
        //Resolving deltas: 100% (418/418), done.

        public void Clone(string gitURL, string rootFolder, string userName, string password)
        {
            ShellDataEvent(ShellElementCode.BeginJob, null);

            string repositoryName = _gitCmds.RepositoryName(gitURL);

            ShellData output = _gitCmds.CreateLocalRootFolder(rootFolder);

            if (output.ExitCode == 0)
                output = _gitCmds.RemoveLocalRepository(rootFolder, repositoryName);

            if (output.ExitCode == 0)
            {
                string gitRepositoryUrl;
                if (userName == null)
                    gitRepositoryUrl = gitURL;
                else
                {
                    int pos = gitURL.IndexOf("://");
                    gitRepositoryUrl = gitURL.Insert(pos + 3, $"{userName}:{password}@");
                }

                output = _gitCmds.Clone(gitRepositoryUrl, rootFolder);
            }

            ShellDataEvent(ShellElementCode.EndJob, null);
        }

        #endregion
    }
}
