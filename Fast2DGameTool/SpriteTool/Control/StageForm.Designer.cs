namespace SpriteTool.Control
{
    partial class StageForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkGuide = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.stagePanel = new SpriteTool.Control.StageBox();
            this.createMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreateForm = new System.Windows.Forms.Button();
            this.txtStage = new System.Windows.Forms.TextBox();
            this.btnStageCreate = new System.Windows.Forms.Button();
            this.objPropertyGrid = new SpriteTool.Control.PropertyGridEx(this.components);
            this.cmbStageList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stagePanel)).BeginInit();
            this.createMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCreateForm);
            this.splitContainer1.Panel2.Controls.Add(this.txtStage);
            this.splitContainer1.Panel2.Controls.Add(this.btnStageCreate);
            this.splitContainer1.Panel2.Controls.Add(this.objPropertyGrid);
            this.splitContainer1.Panel2.Controls.Add(this.cmbStageList);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.cmbResolution);
            this.splitContainer1.Size = new System.Drawing.Size(784, 546);
            this.splitContainer1.SplitterDistance = 583;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.stagePanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(583, 546);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkGuide);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 514);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 29);
            this.panel1.TabIndex = 0;
            // 
            // chkGuide
            // 
            this.chkGuide.AutoSize = true;
            this.chkGuide.Checked = true;
            this.chkGuide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGuide.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkGuide.Location = new System.Drawing.Point(0, 0);
            this.chkGuide.Name = "chkGuide";
            this.chkGuide.Size = new System.Drawing.Size(88, 29);
            this.chkGuide.TabIndex = 10;
            this.chkGuide.Text = "가이드 라인";
            this.chkGuide.UseVisualStyleBackColor = true;
            this.chkGuide.CheckedChanged += new System.EventHandler(this.chkGuide_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Location = new System.Drawing.Point(393, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 29);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Location = new System.Drawing.Point(485, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 29);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // stagePanel
            // 
            this.stagePanel.BackColor = System.Drawing.Color.White;
            this.stagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stagePanel.ContainerControl = null;
            this.stagePanel.ContextMenuStrip = this.createMenuStrip;
            this.stagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stagePanel.GuidLine = true;
            this.stagePanel.GuidTabSize = 20;
            this.stagePanel.LayerInfo = null;
            this.stagePanel.Location = new System.Drawing.Point(3, 3);
            this.stagePanel.Name = "stagePanel";
            this.stagePanel.Size = new System.Drawing.Size(577, 505);
            this.stagePanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.stagePanel.TabIndex = 1;
            this.stagePanel.TabStop = false;
            this.stagePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stagePanel_MouseDown);
            this.stagePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.stagePanel_MouseMove);
            this.stagePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stagePanel_MouseUp);
            // 
            // createMenuStrip
            // 
            this.createMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelCreateToolStripMenuItem,
            this.labelCreateToolStripMenuItem,
            this.buttonCreateToolStripMenuItem});
            this.createMenuStrip.Name = "createMenuStrip";
            this.createMenuStrip.Size = new System.Drawing.Size(149, 70);
            // 
            // panelCreateToolStripMenuItem
            // 
            this.panelCreateToolStripMenuItem.Name = "panelCreateToolStripMenuItem";
            this.panelCreateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.panelCreateToolStripMenuItem.Text = "Panel Create";
            this.panelCreateToolStripMenuItem.Click += new System.EventHandler(this.panelCreateToolStripMenuItem_Click);
            // 
            // labelCreateToolStripMenuItem
            // 
            this.labelCreateToolStripMenuItem.Name = "labelCreateToolStripMenuItem";
            this.labelCreateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.labelCreateToolStripMenuItem.Text = "Label Create";
            this.labelCreateToolStripMenuItem.Click += new System.EventHandler(this.labelCreateToolStripMenuItem_Click);
            // 
            // buttonCreateToolStripMenuItem
            // 
            this.buttonCreateToolStripMenuItem.Name = "buttonCreateToolStripMenuItem";
            this.buttonCreateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.buttonCreateToolStripMenuItem.Text = "Button Create";
            this.buttonCreateToolStripMenuItem.Click += new System.EventHandler(this.buttonCreateToolStripMenuItem_Click);
            // 
            // btnCreateForm
            // 
            this.btnCreateForm.Location = new System.Drawing.Point(27, 478);
            this.btnCreateForm.Name = "btnCreateForm";
            this.btnCreateForm.Size = new System.Drawing.Size(143, 30);
            this.btnCreateForm.TabIndex = 17;
            this.btnCreateForm.Text = "폼생성";
            this.btnCreateForm.UseVisualStyleBackColor = true;
            this.btnCreateForm.Click += new System.EventHandler(this.btnCreateForm_Click);
            // 
            // txtStage
            // 
            this.txtStage.Location = new System.Drawing.Point(7, 12);
            this.txtStage.Name = "txtStage";
            this.txtStage.Size = new System.Drawing.Size(120, 21);
            this.txtStage.TabIndex = 16;
            // 
            // btnStageCreate
            // 
            this.btnStageCreate.Location = new System.Drawing.Point(137, 9);
            this.btnStageCreate.Name = "btnStageCreate";
            this.btnStageCreate.Size = new System.Drawing.Size(57, 24);
            this.btnStageCreate.TabIndex = 15;
            this.btnStageCreate.Text = "생성";
            this.btnStageCreate.UseVisualStyleBackColor = true;
            this.btnStageCreate.Click += new System.EventHandler(this.btnStageCreate_Click);
            // 
            // objPropertyGrid
            // 
            this.objPropertyGrid.HelpVisible = false;
            this.objPropertyGrid.Location = new System.Drawing.Point(3, 66);
            this.objPropertyGrid.Name = "objPropertyGrid";
            this.objPropertyGrid.Size = new System.Drawing.Size(191, 406);
            this.objPropertyGrid.TabIndex = 14;
            // 
            // cmbStageList
            // 
            this.cmbStageList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStageList.FormattingEnabled = true;
            this.cmbStageList.Location = new System.Drawing.Point(45, 42);
            this.cmbStageList.Name = "cmbStageList";
            this.cmbStageList.Size = new System.Drawing.Size(149, 20);
            this.cmbStageList.TabIndex = 13;
            this.cmbStageList.SelectedIndexChanged += new System.EventHandler(this.cmbStageList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "Stage:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 520);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "해상도";
            // 
            // cmbResolution
            // 
            this.cmbResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(54, 518);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(140, 20);
            this.cmbResolution.TabIndex = 10;
            this.cmbResolution.SelectedIndexChanged += new System.EventHandler(this.cmbResolution_SelectedIndexChanged);
            // 
            // StageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 546);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StageForm";
            this.Text = "StageForm";
            this.Resize += new System.EventHandler(this.StageForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stagePanel)).EndInit();
            this.createMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkGuide;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private StageBox stagePanel;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.TextBox txtStage;
        private System.Windows.Forms.Button btnStageCreate;
        private PropertyGridEx objPropertyGrid;
        private System.Windows.Forms.ComboBox cmbStageList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip createMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem panelCreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelCreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonCreateToolStripMenuItem;
        private System.Windows.Forms.Button btnCreateForm;


    }
}