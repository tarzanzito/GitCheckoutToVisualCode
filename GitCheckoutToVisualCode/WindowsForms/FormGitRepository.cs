using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Candal.Services;
using Candal.Configuration;
using Candal.OperatingSystem;

namespace Candal.WindowsForms
{

    public partial class FormGitRepository : Form
    {
        #region Fields

        private readonly IAppConfig _appConfig;
        private readonly FormGitRepositoryService _service;
        private bool _isCloneExecuted = false;
        private GitBranchesInfo _gitBranchesInfo = null;

        #endregion

        #region Cronstructors

    public FormGitRepository()
        {
            InitializeComponent();

            _appConfig = ConfigFactory.Choose();
            _service = new FormGitRepositoryService();

            //_service.ShellDataEvent += (ShellElementCode code, string data) => { ShellDataEventReceiver(code, data); };
            //_service.RegisterEvents();

            _service.RegisterEvents(ShellDataEventReceiver);

            comboBoxBranches.Items.AddRange(new object[] { "master" });
            comboBoxBranches.Text = comboBoxBranches.Items[0].ToString();
        }

        #endregion

        #region UI Events

        private void FormGitRepository_Load(object sender, EventArgs e)
        {
            try
            {
                LoadConfiguration();
                CheckBoxSSHAjust();
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message);
            }
        }

        private void checkBoxSSH_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxSSHAjust();
        }

        private void buttonGetBranches_Click(object sender, EventArgs e)
        {
            LockUnlockForm(true);

            try
            {
                object[] parms = new object[] { textBoxUrl.Text, textBoxRootFolder.Text };
                backgroundWorkerBranches.RunWorkerAsync(parms);
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message);
            }

            LockUnlockForm(false);
        }

        private void buttonVisualCode_Click(object sender, EventArgs e)
        {
            LockUnlockForm(true);

            try
            {
                object[] parms = new object[] { textBoxUrl.Text, textBoxRootFolder.Text, comboBoxBranches.Text };
                backgroundWorkerVisualCode.RunWorkerAsync(parms);
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message);
            }

            LockUnlockForm(false);
        }

        private void textBoxUrl_Leave(object sender, EventArgs e)
        {
            textBoxUrl.Text = textBoxUrl.Text.Trim();
        }

        private void textBoxUser_Leave(object sender, EventArgs e)
        {
            textBoxUser.Text = textBoxUser.Text.Trim();
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            textBoxPassword.Text = textBoxPassword.Text.Trim();
        }

        private void textBoxRootFolder_Leave(object sender, EventArgs e)
        {
            textBoxRootFolder.Text = textBoxRootFolder.Text.Trim();
        }

        private void buttonClone_Click(object sender, EventArgs e)
        {
            LockUnlockForm(true);

            try
            {
                object[] parms = new object[]
                {
                    textBoxUrl.Text,
                    textBoxRootFolder.Text,
                    textBoxUser.Text,
                    textBoxPassword.Text,
                    checkBoxSSH.Checked
                };
                
                backgroundWorkerClone.RunWorkerAsync(parms);
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message);

            }

            LockUnlockForm(false);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            LockUnlockForm(true);
            Close();
        }

        private void FormGitRepository_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveConfiguration();
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message);
            }
        }

        private void textBoxUrl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;

            System.Diagnostics.Debug.Print("textBoxUrl_DragEnter");
        }

        private void textBoxUrl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                    Console.WriteLine(file);
            }
        }

        private void textBoxUrl_DragLeave(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Print("textBoxUrl_DragLeave");
        }

        private void textBoxUrl_DragOver(object sender, DragEventArgs e)
        {
            ////         System.Windows.Forms.DataObject"
            //         object xx = e.Data.GetFormats();
            //        // System.Diagnostics.Debug.Print("textBoxUrl_DragOver" + e.Data.GetDataPresent);
            //         if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //         {
            //             e.Effect = DragDropEffects.Move;
            //         }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxConsole.Clear();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxConsole.Copy();
            richTextBoxConsole.SelectionLength = 0;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxConsole.SelectAll();
        }

        #endregion

        #region Relation Methods

        private void BranchesActionsRefresh()
        {
            this.comboBoxBranches.Enabled = this._isCloneExecuted;
            this.buttonGetBranches.Enabled = this._isCloneExecuted;
            this.buttonVisualCode.Enabled = this._isCloneExecuted;
        }

        private void LockUnlockForm(bool lockIt)
        {
            this.textBoxUrl.Enabled = !lockIt;
            this.checkBoxSSH.Enabled = !lockIt;
            this.textBoxUser.Enabled = !lockIt;
            this.textBoxPassword.Enabled = !lockIt;
            this.comboBoxBranches.Enabled = !lockIt;
            this.textBoxRootFolder.Enabled = !lockIt;
            this.richTextBoxConsole.Enabled = true;

            this.buttonClone.Enabled = !lockIt;
            this.buttonVisualCode.Enabled = !lockIt;
            this.buttonGetBranches.Enabled = !lockIt;
            this.buttonClose.Enabled = true;

            if (lockIt)
            {
                this.Cursor = Cursors.WaitCursor;
                this.richTextBoxConsole.Cursor = Cursors.AppStarting;
                this.buttonClose.Cursor = Cursors.AppStarting;
            }
            else
            {
                this.Cursor = Cursors.Default;
                this.richTextBoxConsole.Cursor = Cursors.Default;
                this.buttonClose.Cursor = Cursors.Default;
                CheckBoxSSHAjust();
            }

            BranchesActionsRefresh();
            System.Windows.Forms.Application.DoEvents();
        }

        private void CheckBoxSSHAjust()
        {
            this.textBoxUser.Enabled = !this.checkBoxSSH.Checked;
            this.textBoxPassword.Enabled = !this.checkBoxSSH.Checked;
        }

        private void LoadConfiguration()
        {
            _appConfig.Load();
#if DEBUG
            this.textBoxUrl.Text = "https://gitlab.com/tarzanzito/MusicCollection.git";
            this.checkBoxSSH.Checked = false;
            this.textBoxUser.Text = "tarzanzito";
            this.textBoxPassword.Text = "PASS";
            this.textBoxRootFolder.Text = @"D:\GIT - WORK";
#else
            this.textBoxUrl.Text = _appConfig.Data.GitUrl;
            this.checkBoxSSH.Checked = _appConfig.Data.UseSSH;
            this.textBoxUser.Text = _appConfig.Data.UserName;
            this.textBoxPassword.Text = _appConfig.Data.Password;
            this.textBoxRootFolder.Text = _appConfig.Data.RootFolder;
#endif
        }

        private void SaveConfiguration()
        {
            _appConfig.Data.GitUrl = this.textBoxUrl.Text;
            _appConfig.Data.UseSSH = this.checkBoxSSH.Checked;
            _appConfig.Data.UserName = this.textBoxUser.Text;
            _appConfig.Data.Password = this.textBoxPassword.Text;
            _appConfig.Data.RootFolder = this.textBoxRootFolder.Text;

            _appConfig.Save();
        }

        private void AppendExceptionToConsole(string data)
        {
            string tempData = "";

            if (data != null)
                tempData = data.Replace(System.Environment.NewLine, " ");

            AppendDataToConsole(ShellElementCode.ExceptionData, tempData);
        }

        private void AppendDataToConsole(ShellElementCode code, string data)
        {
            try
            {
                Color color = Color.White;
                string tempData = "";

                if (data != null)
                    tempData = data.Replace(System.Environment.NewLine, " ")
                        .Replace(textBoxPassword.Text, "password");

                switch (code)
                {
                    case ShellElementCode.BeginJob:
                        color = Color.LightBlue;
                        tempData = "Begin Job...";
                        break;
                    case ShellElementCode.EndJob:
                        color = Color.LightBlue;
                        tempData = "End Job...";
                        break;
                    case ShellElementCode.BeginCommand:
                        color = Color.Yellow;
                        break;
                    case ShellElementCode.EndCommand:
                        color = Color.Yellow;
                        tempData = "End Command...";
                        break;
                    case ShellElementCode.OutputData:
                        color = Color.White;
                        break;
                    case ShellElementCode.ErrorData:
                        color = Color.OrangeRed;
                        break;
                    case ShellElementCode.ExceptionData:
                        color = Color.Red;
                        break;
                    default:
                        color = Color.Aqua;
                        tempData = "Default case...";
                        break;
                }

                richTextBoxConsole.SelectionStart = richTextBoxConsole.TextLength;
                richTextBoxConsole.SelectionLength = 0;
                richTextBoxConsole.SelectionColor = color;
                richTextBoxConsole.AppendText($"{tempData}{System.Environment.NewLine}");
                richTextBoxConsole.SelectionColor = richTextBoxConsole.ForeColor;

                richTextBoxConsole.SelectionStart = richTextBoxConsole.TextLength;
                richTextBoxConsole.ScrollToCaret();
                System.Windows.Forms.Application.DoEvents();

            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message); //pode entrar em loop infinito !!!
            }
        }

        #endregion

        # region BackgroundWorkers

        private void backgroundWorkerClone_DoWork(object sender, DoWorkEventArgs e)
        {
            //https://stackoverflow.com/questions/12414601/async-await-vs-backgroundworker

            try
            {
                object[] parms = (object[])e.Argument;
                string gitUrl = (string)parms[0];
                string rootFolder = (string)parms[1];
                string userName = (string)parms[2];
                string userPassword = (string)parms[3];
                bool useSSH = (bool)parms[4];

                if (useSSH)
                    _service.Clone(gitUrl, rootFolder);
                else
                    _service.Clone(gitUrl, rootFolder, userName, userPassword);
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message);
            }
        }

        private void backgroundWorkerClone_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _isCloneExecuted = true;
            LockUnlockForm(false);
        }

        private void backgroundWorkerVisualCode_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] parms = (object[])e.Argument;
                string gitUrl = (string)parms[0];
                string rootFolder = (string)parms[1];
                string branch = (string)parms[2];

                _service.VisualCodeStart(gitUrl, rootFolder, branch);
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message);
            }
        }

        private void backgroundWorkerVisualCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LockUnlockForm(false);
        }

        private void backgroundWorkerBranches_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] parms = (object[])e.Argument;
                string gitUrl = (string)parms[0];
                string rootFolder = (string)parms[1];

                _gitBranchesInfo = _service.GetBranches(gitUrl, rootFolder);
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message);
            }
        }

        private void backgroundWorkerBranches_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_gitBranchesInfo != null)
            {
                comboBoxBranches.DataSource = _gitBranchesInfo.List;
                comboBoxBranches.Text = _gitBranchesInfo.Current;
            }

            LockUnlockForm(false);
        }




        #endregion

        #region Delegated Methods

        public void ShellDataEventReceiver(ShellElementCode code, string data)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    ShellDataEventHandler cb = new ShellDataEventHandler(ShellDataEventReceiver);
                    this.Invoke(cb, new object[] { code, data });
                }
                else
                    AppendDataToConsole(code, data);
            }
            catch (Exception ex)
            {
                AppendExceptionToConsole(ex.Message); 

            }
        }

        #endregion

     }
}

