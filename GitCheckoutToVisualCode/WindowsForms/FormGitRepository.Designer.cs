namespace Candal.WindowsForms
{
    partial class FormGitRepository
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.comboBoxBranches = new System.Windows.Forms.ComboBox();
            this.buttonClone = new System.Windows.Forms.Button();
            this.buttonVisualCode = new System.Windows.Forms.Button();
            this.checkBoxSSH = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxRootFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonGetBranches = new System.Windows.Forms.Button();
            this.backgroundWorkerClone = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerVisualCode = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerBranches = new System.ComponentModel.BackgroundWorker();
            this.richTextBoxConsole = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripConsole = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URl Git Repo.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Branch.:";
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.AllowDrop = true;
            this.textBoxUrl.Location = new System.Drawing.Point(109, 18);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(557, 20);
            this.textBoxUrl.TabIndex = 2;
            this.textBoxUrl.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxUrl_DragDrop);
            this.textBoxUrl.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxUrl_DragEnter);
            this.textBoxUrl.DragOver += new System.Windows.Forms.DragEventHandler(this.textBoxUrl_DragOver);
            this.textBoxUrl.DragLeave += new System.EventHandler(this.textBoxUrl_DragLeave);
            this.textBoxUrl.Leave += new System.EventHandler(this.textBoxUrl_Leave);
            // 
            // comboBoxBranches
            // 
            this.comboBoxBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBranches.Enabled = false;
            this.comboBoxBranches.FormattingEnabled = true;
            this.comboBoxBranches.Location = new System.Drawing.Point(109, 152);
            this.comboBoxBranches.Name = "comboBoxBranches";
            this.comboBoxBranches.Size = new System.Drawing.Size(340, 21);
            this.comboBoxBranches.TabIndex = 3;
            // 
            // buttonClone
            // 
            this.buttonClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClone.Location = new System.Drawing.Point(672, 18);
            this.buttonClone.Name = "buttonClone";
            this.buttonClone.Size = new System.Drawing.Size(90, 22);
            this.buttonClone.TabIndex = 4;
            this.buttonClone.Text = "Clone";
            this.buttonClone.UseVisualStyleBackColor = true;
            this.buttonClone.Click += new System.EventHandler(this.buttonClone_Click);
            // 
            // buttonVisualCode
            // 
            this.buttonVisualCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVisualCode.Enabled = false;
            this.buttonVisualCode.Location = new System.Drawing.Point(672, 151);
            this.buttonVisualCode.Name = "buttonVisualCode";
            this.buttonVisualCode.Size = new System.Drawing.Size(90, 22);
            this.buttonVisualCode.TabIndex = 5;
            this.buttonVisualCode.Text = "Visual Code";
            this.buttonVisualCode.UseVisualStyleBackColor = true;
            this.buttonVisualCode.Click += new System.EventHandler(this.buttonVisualCode_Click);
            // 
            // checkBoxSSH
            // 
            this.checkBoxSSH.AutoSize = true;
            this.checkBoxSSH.Checked = true;
            this.checkBoxSSH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSSH.Location = new System.Drawing.Point(109, 44);
            this.checkBoxSSH.Name = "checkBoxSSH";
            this.checkBoxSSH.Size = new System.Drawing.Size(73, 17);
            this.checkBoxSSH.TabIndex = 6;
            this.checkBoxSSH.Text = "With SSH";
            this.checkBoxSSH.UseVisualStyleBackColor = true;
            this.checkBoxSSH.CheckedChanged += new System.EventHandler(this.checkBoxSSH_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "User Name.:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Password.:";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(109, 67);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(191, 20);
            this.textBoxUser.TabIndex = 9;
            this.textBoxUser.Leave += new System.EventHandler(this.textBoxUser_Leave);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(109, 93);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(191, 20);
            this.textBoxPassword.TabIndex = 10;
            this.textBoxPassword.Leave += new System.EventHandler(this.textBoxPassword_Leave);
            // 
            // textBoxRootFolder
            // 
            this.textBoxRootFolder.Location = new System.Drawing.Point(109, 126);
            this.textBoxRootFolder.Name = "textBoxRootFolder";
            this.textBoxRootFolder.Size = new System.Drawing.Size(557, 20);
            this.textBoxRootFolder.TabIndex = 11;
            this.textBoxRootFolder.Leave += new System.EventHandler(this.textBoxRootFolder_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Local Root Folder.:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(672, 365);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(90, 22);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonGetBranches
            // 
            this.buttonGetBranches.Enabled = false;
            this.buttonGetBranches.Location = new System.Drawing.Point(455, 151);
            this.buttonGetBranches.Name = "buttonGetBranches";
            this.buttonGetBranches.Size = new System.Drawing.Size(62, 22);
            this.buttonGetBranches.TabIndex = 14;
            this.buttonGetBranches.Text = "Get";
            this.buttonGetBranches.UseVisualStyleBackColor = true;
            this.buttonGetBranches.Click += new System.EventHandler(this.buttonGetBranches_Click);
            // 
            // backgroundWorkerClone
            // 
            this.backgroundWorkerClone.WorkerReportsProgress = true;
            this.backgroundWorkerClone.WorkerSupportsCancellation = true;
            this.backgroundWorkerClone.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerClone_DoWork);
            this.backgroundWorkerClone.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerClone_RunWorkerCompleted);
            // 
            // backgroundWorkerVisualCode
            // 
            this.backgroundWorkerVisualCode.WorkerReportsProgress = true;
            this.backgroundWorkerVisualCode.WorkerSupportsCancellation = true;
            this.backgroundWorkerVisualCode.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerVisualCode_DoWork);
            this.backgroundWorkerVisualCode.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerVisualCode_RunWorkerCompleted);
            // 
            // backgroundWorkerBranches
            // 
            this.backgroundWorkerBranches.WorkerReportsProgress = true;
            this.backgroundWorkerBranches.WorkerSupportsCancellation = true;
            this.backgroundWorkerBranches.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerBranches_DoWork);
            this.backgroundWorkerBranches.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerBranches_RunWorkerCompleted);
            // 
            // richTextBoxConsole
            // 
            this.richTextBoxConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxConsole.BackColor = System.Drawing.SystemColors.WindowText;
            this.richTextBoxConsole.ContextMenuStrip = this.contextMenuStripConsole;
            this.richTextBoxConsole.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxConsole.ForeColor = System.Drawing.SystemColors.Window;
            this.richTextBoxConsole.Location = new System.Drawing.Point(12, 191);
            this.richTextBoxConsole.Name = "richTextBoxConsole";
            this.richTextBoxConsole.ReadOnly = true;
            this.richTextBoxConsole.Size = new System.Drawing.Size(651, 196);
            this.richTextBoxConsole.TabIndex = 16;
            this.richTextBoxConsole.Text = "";
            this.richTextBoxConsole.WordWrap = false;
            // 
            // contextMenuStripConsole
            // 
            this.contextMenuStripConsole.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.contextMenuStripConsole.Name = "contextMenuStripConsole";
            this.contextMenuStripConsole.Size = new System.Drawing.Size(165, 70);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // FormGitRepository
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 399);
            this.Controls.Add(this.richTextBoxConsole);
            this.Controls.Add(this.buttonGetBranches);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxRootFolder);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxSSH);
            this.Controls.Add(this.buttonVisualCode);
            this.Controls.Add(this.buttonClone);
            this.Controls.Add(this.comboBoxBranches);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormGitRepository";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Git Checkout to Visual Code";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGitRepository_FormClosing);
            this.Load += new System.EventHandler(this.FormGitRepository_Load);
            this.contextMenuStripConsole.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.ComboBox comboBoxBranches;
        private System.Windows.Forms.Button buttonClone;
        private System.Windows.Forms.Button buttonVisualCode;
        private System.Windows.Forms.CheckBox checkBoxSSH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxRootFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonGetBranches;
        private System.ComponentModel.BackgroundWorker backgroundWorkerClone;
        private System.ComponentModel.BackgroundWorker backgroundWorkerVisualCode;
        private System.ComponentModel.BackgroundWorker backgroundWorkerBranches;
        private System.Windows.Forms.RichTextBox richTextBoxConsole;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripConsole;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    }
}