﻿namespace GuardianVault
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
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.masterPasswordToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.treeFiles = new System.Windows.Forms.TreeView();
            this.treeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exploreFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lstFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstFilesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.encryptFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openEditFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exploreFolderFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.operationStatusStrip = new System.Windows.Forms.StatusStrip();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mainToolStrip.SuspendLayout();
            this.treeContextMenu.SuspendLayout();
            this.lstFilesContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterPasswordToolStripButton,
            this.settingsToolStripButton,
            this.helpToolStripButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1256, 54);
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
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
            this.settingsToolStripButton.Size = new System.Drawing.Size(79, 51);
            this.settingsToolStripButton.Text = "User Settings";
            this.settingsToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.settingsToolStripButton.Click += new System.EventHandler(this.settingsToolStripButton_Click);
            // 
            // treeFiles
            // 
            this.treeFiles.ContextMenuStrip = this.treeContextMenu;
            this.treeFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeFiles.ImageIndex = 0;
            this.treeFiles.ImageList = this.imageList1;
            this.treeFiles.Location = new System.Drawing.Point(0, 54);
            this.treeFiles.Margin = new System.Windows.Forms.Padding(4);
            this.treeFiles.Name = "treeFiles";
            this.treeFiles.SelectedImageIndex = 0;
            this.treeFiles.ShowLines = false;
            this.treeFiles.Size = new System.Drawing.Size(355, 640);
            this.treeFiles.TabIndex = 1;
            this.treeFiles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFolder_NodeMouseClick);
            this.treeFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeFolder_MouseUp);
            // 
            // treeContextMenu
            // 
            this.treeContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToolStripMenuItem,
            this.addNewFolderToolStripMenuItem,
            this.toolStripSeparator5,
            this.deleteFolderToolStripMenuItem,
            this.toolStripSeparator2,
            this.exploreFolderToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.treeContextMenu.Name = "treeContextMenu";
            this.treeContextMenu.Size = new System.Drawing.Size(219, 206);
            this.treeContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.treeContextMenu_Opening);
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(218, 38);
            this.addFilesToolStripMenuItem.Text = "Add Files...";
            this.addFilesToolStripMenuItem.Click += new System.EventHandler(this.addFilesMenuItem_Click);
            // 
            // addNewFolderToolStripMenuItem
            // 
            this.addNewFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewFolderToolStripMenuItem.Image")));
            this.addNewFolderToolStripMenuItem.Name = "addNewFolderToolStripMenuItem";
            this.addNewFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addNewFolderToolStripMenuItem.Size = new System.Drawing.Size(218, 38);
            this.addNewFolderToolStripMenuItem.Text = "Add New Folder";
            this.addNewFolderToolStripMenuItem.Click += new System.EventHandler(this.addNewFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(215, 6);
            // 
            // deleteFolderToolStripMenuItem
            // 
            this.deleteFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteFolderToolStripMenuItem.Image")));
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(218, 38);
            this.deleteFolderToolStripMenuItem.Text = "Delete Folder";
            this.deleteFolderToolStripMenuItem.Click += new System.EventHandler(this.deleteFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
            // 
            // exploreFolderToolStripMenuItem
            // 
            this.exploreFolderToolStripMenuItem.Name = "exploreFolderToolStripMenuItem";
            this.exploreFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exploreFolderToolStripMenuItem.Size = new System.Drawing.Size(218, 38);
            this.exploreFolderToolStripMenuItem.Text = "Explore Folder";
            this.exploreFolderToolStripMenuItem.Click += new System.EventHandler(this.exploreFolderToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripMenuItem.Image")));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(218, 38);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
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
            // lstFiles
            // 
            this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lstFiles.ContextMenuStrip = this.lstFilesContextMenu;
            this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFiles.HideSelection = false;
            this.lstFiles.Location = new System.Drawing.Point(355, 54);
            this.lstFiles.Margin = new System.Windows.Forms.Padding(4);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(901, 640);
            this.lstFiles.SmallImageList = this.imageList2;
            this.lstFiles.TabIndex = 2;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            this.lstFiles.DoubleClick += new System.EventHandler(this.lstFiles_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 314;
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
            // lstFilesContextMenu
            // 
            this.lstFilesContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.lstFilesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesMenuItem,
            this.toolStripSeparator6,
            this.encryptFileToolStripMenuItem,
            this.openEditFileMenuItem,
            this.downloadFileMenuItem,
            this.toolStripSeparator7,
            this.deleteFileMenuItem,
            this.toolStripSeparator4,
            this.exploreFolderFileToolStripMenuItem,
            this.refreshFilesMenuItem});
            this.lstFilesContextMenu.Name = "treeContextMenu";
            this.lstFilesContextMenu.Size = new System.Drawing.Size(218, 288);
            this.lstFilesContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.lstFilesContextMenu_Opening);
            // 
            // addFilesMenuItem
            // 
            this.addFilesMenuItem.Name = "addFilesMenuItem";
            this.addFilesMenuItem.Size = new System.Drawing.Size(217, 38);
            this.addFilesMenuItem.Text = "Add Files...";
            this.addFilesMenuItem.Click += new System.EventHandler(this.addFilesMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(214, 6);
            // 
            // encryptFileToolStripMenuItem
            // 
            this.encryptFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("encryptFileToolStripMenuItem.Image")));
            this.encryptFileToolStripMenuItem.Name = "encryptFileToolStripMenuItem";
            this.encryptFileToolStripMenuItem.Size = new System.Drawing.Size(217, 38);
            this.encryptFileToolStripMenuItem.Text = "Encrypt Files";
            this.encryptFileToolStripMenuItem.Click += new System.EventHandler(this.encryptFileToolStripMenuItem_Click);
            // 
            // openEditFileMenuItem
            // 
            this.openEditFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openEditFileMenuItem.Image")));
            this.openEditFileMenuItem.Name = "openEditFileMenuItem";
            this.openEditFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openEditFileMenuItem.Size = new System.Drawing.Size(217, 38);
            this.openEditFileMenuItem.Text = "Open/Edit File...";
            this.openEditFileMenuItem.Click += new System.EventHandler(this.openEditFileMenuItem_Click);
            // 
            // downloadFileMenuItem
            // 
            this.downloadFileMenuItem.Name = "downloadFileMenuItem";
            this.downloadFileMenuItem.Size = new System.Drawing.Size(217, 38);
            this.downloadFileMenuItem.Text = "Download Files";
            this.downloadFileMenuItem.Click += new System.EventHandler(this.downloadFileMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(214, 6);
            // 
            // deleteFileMenuItem
            // 
            this.deleteFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteFileMenuItem.Image")));
            this.deleteFileMenuItem.Name = "deleteFileMenuItem";
            this.deleteFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deleteFileMenuItem.Size = new System.Drawing.Size(217, 38);
            this.deleteFileMenuItem.Text = "Delete Files";
            this.deleteFileMenuItem.Click += new System.EventHandler(this.deleteFileMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(214, 6);
            // 
            // exploreFolderFileToolStripMenuItem
            // 
            this.exploreFolderFileToolStripMenuItem.Name = "exploreFolderFileToolStripMenuItem";
            this.exploreFolderFileToolStripMenuItem.Size = new System.Drawing.Size(217, 38);
            this.exploreFolderFileToolStripMenuItem.Text = "Explore Folder";
            this.exploreFolderFileToolStripMenuItem.Click += new System.EventHandler(this.exploreFolderFileToolStripMenuItem_Click);
            // 
            // refreshFilesMenuItem
            // 
            this.refreshFilesMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshFilesMenuItem.Image")));
            this.refreshFilesMenuItem.Name = "refreshFilesMenuItem";
            this.refreshFilesMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.refreshFilesMenuItem.Size = new System.Drawing.Size(217, 38);
            this.refreshFilesMenuItem.Text = "Refresh";
            this.refreshFilesMenuItem.Click += new System.EventHandler(this.refreshFilesMenuItem_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "permissions16.png");
            this.imageList2.Images.SetKeyName(1, "WARN16.GIF");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(355, 54);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 640);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // operationStatusStrip
            // 
            this.operationStatusStrip.Location = new System.Drawing.Point(0, 694);
            this.operationStatusStrip.Name = "operationStatusStrip";
            this.operationStatusStrip.Size = new System.Drawing.Size(1256, 22);
            this.operationStatusStrip.TabIndex = 4;
            this.operationStatusStrip.Text = "statusStrip1";
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(36, 51);
            this.helpToolStripButton.Text = "Help";
            this.helpToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // AppMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 716);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.treeFiles);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.operationStatusStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AppMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guardian Vault Applicaiton";
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.treeContextMenu.ResumeLayout(false);
            this.lstFilesContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
        private System.Windows.Forms.TreeView treeFiles;
        private System.Windows.Forms.ListView lstFiles;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.StatusStrip operationStatusStrip;
        private System.Windows.Forms.ContextMenuStrip treeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addNewFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton masterPasswordToolStripButton;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ContextMenuStrip lstFilesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addFilesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openEditFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem deleteFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem refreshFilesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem encryptFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exploreFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exploreFolderFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
    }
}

