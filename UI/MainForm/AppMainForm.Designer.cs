namespace GuardianVault
{
    partial class AppMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppMainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.masterPasswordToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.treeFolder = new System.Windows.Forms.TreeView();
            this.treeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lstFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.treeContextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterPasswordToolStripButton,
            this.settingsToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1256, 54);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // masterPasswordToolStripButton
            // 
            this.masterPasswordToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("masterPasswordToolStripButton.Image")));
            this.masterPasswordToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.masterPasswordToolStripButton.Name = "masterPasswordToolStripButton";
            this.masterPasswordToolStripButton.Size = new System.Drawing.Size(100, 51);
            this.masterPasswordToolStripButton.Text = "Master Password";
            this.masterPasswordToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.masterPasswordToolStripButton.Click += new System.EventHandler(this.masterPasswordToolStripButton_Click);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripButton.Image")));
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(78, 51);
            this.settingsToolStripButton.Text = "App Settings";
            this.settingsToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.settingsToolStripButton.Click += new System.EventHandler(this.settingsToolStripButton_Click);
            // 
            // treeFolder
            // 
            this.treeFolder.ContextMenuStrip = this.treeContextMenu;
            this.treeFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeFolder.ImageIndex = 0;
            this.treeFolder.ImageList = this.imageList1;
            this.treeFolder.Location = new System.Drawing.Point(0, 78);
            this.treeFolder.Margin = new System.Windows.Forms.Padding(4);
            this.treeFolder.Name = "treeFolder";
            this.treeFolder.SelectedImageIndex = 0;
            this.treeFolder.ShowLines = false;
            this.treeFolder.Size = new System.Drawing.Size(355, 616);
            this.treeFolder.TabIndex = 1;
            this.treeFolder.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFolder_NodeMouseClick);
            this.treeFolder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeFolder_MouseUp);
            // 
            // treeContextMenu
            // 
            this.treeContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewFolderToolStripMenuItem,
            this.downloadFolderToolStripMenuItem,
            this.toolStripSeparator3,
            this.deleteFolderToolStripMenuItem,
            this.toolStripSeparator2,
            this.addFilesToolStripMenuItem,
            this.openToolStripMenuItem,
            this.downloadFileToolStripMenuItem,
            this.toolStripSeparator4,
            this.deleteFileToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.treeContextMenu.Name = "treeContextMenu";
            this.treeContextMenu.Size = new System.Drawing.Size(181, 326);
            this.treeContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.treeContextMenu_Opening);
            // 
            // addNewFolderToolStripMenuItem
            // 
            this.addNewFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewFolderToolStripMenuItem.Image")));
            this.addNewFolderToolStripMenuItem.Name = "addNewFolderToolStripMenuItem";
            this.addNewFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 38);
            this.addNewFolderToolStripMenuItem.Text = "Add New Folder";
            // 
            // downloadFolderToolStripMenuItem
            // 
            this.downloadFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("downloadFolderToolStripMenuItem.Image")));
            this.downloadFolderToolStripMenuItem.Name = "downloadFolderToolStripMenuItem";
            this.downloadFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 38);
            this.downloadFolderToolStripMenuItem.Text = "Download Folder";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // deleteFolderToolStripMenuItem
            // 
            this.deleteFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteFolderToolStripMenuItem.Image")));
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 38);
            this.deleteFolderToolStripMenuItem.Text = "Delete Folder";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(180, 38);
            this.addFilesToolStripMenuItem.Text = "Add Files...";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 38);
            this.openToolStripMenuItem.Text = "Open/Edit File...";
            // 
            // downloadFileToolStripMenuItem
            // 
            this.downloadFileToolStripMenuItem.Name = "downloadFileToolStripMenuItem";
            this.downloadFileToolStripMenuItem.Size = new System.Drawing.Size(180, 38);
            this.downloadFileToolStripMenuItem.Text = "Download File";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteFileToolStripMenuItem.Image")));
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size(180, 38);
            this.deleteFileToolStripMenuItem.Text = "Delete File";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(180, 38);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "FLDRNEW.GIF");
            this.imageList1.Images.SetKeyName(1, "doclibrary.gif");
            this.imageList1.Images.SetKeyName(2, "menuprofile.gif");
            this.imageList1.Images.SetKeyName(3, "OpenFolder.gif");
            this.imageList1.Images.SetKeyName(4, "lock_2_32.ico");
            this.imageList1.Images.SetKeyName(5, "plasticxp_mailicons_file_32.png");
            // 
            // listView1
            // 
            this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFiles.HideSelection = false;
            this.lstFiles.Location = new System.Drawing.Point(355, 78);
            this.lstFiles.Margin = new System.Windows.Forms.Padding(4);
            this.lstFiles.Name = "listView1";
            this.lstFiles.Size = new System.Drawing.Size(901, 616);
            this.lstFiles.TabIndex = 2;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Date Modified";
            this.columnHeader2.Width = 246;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 135;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            this.columnHeader4.Width = 141;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(355, 78);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 616);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 694);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1256, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1256, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToolStripMenuItem1,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addFilesToolStripMenuItem1
            // 
            this.addFilesToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("addFilesToolStripMenuItem1.Image")));
            this.addFilesToolStripMenuItem1.Name = "addFilesToolStripMenuItem1";
            this.addFilesToolStripMenuItem1.Size = new System.Drawing.Size(147, 38);
            this.addFilesToolStripMenuItem1.Text = "Add Files...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(147, 38);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // AppMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 716);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.treeFolder);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AppMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guardian Vault Applicaiton";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.treeContextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
        private System.Windows.Forms.TreeView treeFolder;
        private System.Windows.Forms.ListView lstFiles;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ContextMenuStrip treeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addNewFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton masterPasswordToolStripButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

