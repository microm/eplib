using SpriteTool.Control;

namespace SpriteTool
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripImageInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageCleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guidLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lineColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnSave = new System.Windows.Forms.ToolStripButton();
            this.actorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnLineColor = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.SplitPic = new SpriteTool.Control.BasePictureBox();
            this.ListPanel = new SpriteTool.Control.ListPicPanel();
            this.RightCtrl = new SpriteTool.Control.AniSpriteCtrl();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStripStageButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitPic)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripImageInfo,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 704);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(986, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripImageInfo
            // 
            this.toolStripImageInfo.Name = "toolStripImageInfo";
            this.toolStripImageInfo.Size = new System.Drawing.Size(43, 17);
            this.toolStripImageInfo.Text = "x:0 y:0";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(250, 16);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(986, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageOpenToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // imageOpenToolStripMenuItem
            // 
            this.imageOpenToolStripMenuItem.Name = "imageOpenToolStripMenuItem";
            this.imageOpenToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.imageOpenToolStripMenuItem.Text = "&Image Open ...";
            this.imageOpenToolStripMenuItem.Click += new System.EventHandler(this.imageOpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.saveToolStripMenuItem.Text = "&Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifyToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectRegionToolStripMenuItem,
            this.AutoImageToolStripMenuItem,
            this.imageCleanToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.modifyToolStripMenuItem.Text = "&Modify";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(163, 6);
            // 
            // selectRegionToolStripMenuItem
            // 
            this.selectRegionToolStripMenuItem.Name = "selectRegionToolStripMenuItem";
            this.selectRegionToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.selectRegionToolStripMenuItem.Text = "SelectRegion";
            this.selectRegionToolStripMenuItem.Click += new System.EventHandler(this.selectRegionToolStripMenuItem_Click);
            // 
            // AutoImageToolStripMenuItem
            // 
            this.AutoImageToolStripMenuItem.Name = "AutoImageToolStripMenuItem";
            this.AutoImageToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.AutoImageToolStripMenuItem.Text = "자동 이미지 등록";
            this.AutoImageToolStripMenuItem.Click += new System.EventHandler(this.AutoImageToolStripMenuItem_Click);
            // 
            // imageCleanToolStripMenuItem
            // 
            this.imageCleanToolStripMenuItem.Name = "imageCleanToolStripMenuItem";
            this.imageCleanToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.imageCleanToolStripMenuItem.Text = "이미지 정리";
            this.imageCleanToolStripMenuItem.Click += new System.EventHandler(this.imageCleanToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guidLineToolStripMenuItem,
            this.toolStripSeparator3,
            this.lineColorToolStripMenuItem,
            this.backColorToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // guidLineToolStripMenuItem
            // 
            this.guidLineToolStripMenuItem.Checked = true;
            this.guidLineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.guidLineToolStripMenuItem.Name = "guidLineToolStripMenuItem";
            this.guidLineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.guidLineToolStripMenuItem.Text = "Guide Line";
            this.guidLineToolStripMenuItem.Click += new System.EventHandler(this.guidLineToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // lineColorToolStripMenuItem
            // 
            this.lineColorToolStripMenuItem.Name = "lineColorToolStripMenuItem";
            this.lineColorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lineColorToolStripMenuItem.Text = "LineColor";
            this.lineColorToolStripMenuItem.Click += new System.EventHandler(this.lineColorToolStripMenuItem_Click);
            // 
            // backColorToolStripMenuItem
            // 
            this.backColorToolStripMenuItem.Name = "backColorToolStripMenuItem";
            this.backColorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.backColorToolStripMenuItem.Text = "BackColor";
            this.backColorToolStripMenuItem.Click += new System.EventHandler(this.backColorToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnImage,
            this.toolStripBtnSave,
            this.actorButton,
            this.toolStripStageButton,
            this.toolStripBtnLineColor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(986, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnImage
            // 
            this.toolStripBtnImage.Image = global::SpriteTool.Resource3.open;
            this.toolStripBtnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnImage.Name = "toolStripBtnImage";
            this.toolStripBtnImage.Size = new System.Drawing.Size(60, 22);
            this.toolStripBtnImage.Text = "Image";
            this.toolStripBtnImage.Click += new System.EventHandler(this.imageOpenToolStripMenuItem_Click);
            // 
            // toolStripBtnSave
            // 
            this.toolStripBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnSave.Image = global::SpriteTool.Resource3.save;
            this.toolStripBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnSave.Name = "toolStripBtnSave";
            this.toolStripBtnSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnSave.Text = "Save";
            this.toolStripBtnSave.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // actorButton
            // 
            this.actorButton.Image = ((System.Drawing.Image)(resources.GetObject("actorButton.Image")));
            this.actorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actorButton.Name = "actorButton";
            this.actorButton.Size = new System.Drawing.Size(56, 22);
            this.actorButton.Text = "Actor";
            this.actorButton.Click += new System.EventHandler(this.actorButton_Click);
            // 
            // toolStripBtnLineColor
            // 
            this.toolStripBtnLineColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnLineColor.Image = global::SpriteTool.Resource3.table_borders_16x16;
            this.toolStripBtnLineColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnLineColor.Name = "toolStripBtnLineColor";
            this.toolStripBtnLineColor.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnLineColor.Click += new System.EventHandler(this.toolStripBtnLineColor_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.RightCtrl);
            this.splitContainer1.Size = new System.Drawing.Size(986, 655);
            this.splitContainer1.SplitterDistance = 769;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainer2.Panel1.Controls.Add(this.SplitPic);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer2.Panel2.Controls.Add(this.ListPanel);
            this.splitContainer2.Size = new System.Drawing.Size(769, 655);
            this.splitContainer2.SplitterDistance = 509;
            this.splitContainer2.TabIndex = 0;
            // 
            // SplitPic
            // 
            this.SplitPic.BackColor = System.Drawing.Color.Transparent;
            this.SplitPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SplitPic.Cursor = System.Windows.Forms.Cursors.Cross;
            this.SplitPic.ImageSize = new System.Drawing.Size(0, 0);
            this.SplitPic.Location = new System.Drawing.Point(0, 0);
            this.SplitPic.Name = "SplitPic";
            this.SplitPic.Size = new System.Drawing.Size(200, 200);
            this.SplitPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.SplitPic.TabIndex = 0;
            this.SplitPic.TabStop = false;
            this.SplitPic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SplitPic_MouseDown);
            this.SplitPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SplitPic_MouseMove);
            this.SplitPic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SplitPic_MouseUp);
            // 
            // ListPanel
            // 
            this.ListPanel.AutoScroll = true;
            this.ListPanel.AutoScrollMargin = new System.Drawing.Size(0, 100);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(0, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(769, 142);
            this.ListPanel.TabIndex = 0;
            this.ListPanel.WrapContents = false;
            // 
            // RightCtrl
            // 
            this.RightCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightCtrl.Location = new System.Drawing.Point(0, 0);
            this.RightCtrl.Name = "RightCtrl";
            this.RightCtrl.Size = new System.Drawing.Size(213, 655);
            this.RightCtrl.TabIndex = 0;
            // 
            // toolStripStageButton
            // 
            this.toolStripStageButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStageButton.Image")));
            this.toolStripStageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripStageButton.Name = "toolStripStageButton";
            this.toolStripStageButton.Size = new System.Drawing.Size(57, 22);
            this.toolStripStageButton.Text = "Stage";
            this.toolStripStageButton.Click += new System.EventHandler(this.toolStripStageButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 726);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SpriteTool";
            this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnImage;
        private System.Windows.Forms.ToolStripButton toolStripBtnSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripImageInfo;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private BasePictureBox SplitPic;
        private AniSpriteCtrl RightCtrl;
        private System.Windows.Forms.ToolStripMenuItem imageOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guidLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripBtnLineColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private ListPicPanel ListPanel;
        private System.Windows.Forms.ToolStripMenuItem backColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem AutoImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton actorButton;
        private System.Windows.Forms.ToolStripMenuItem imageCleanToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripStageButton;
    }
}

