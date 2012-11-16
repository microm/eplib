namespace SpriteTool.Control
{
    partial class ActorForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddImg = new System.Windows.Forms.Button();
            this.listRegion = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listActor = new System.Windows.Forms.ListBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnPlay = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkGuide = new System.Windows.Forms.CheckBox();
            this.prevPictrue = new SpriteTool.Control.ActorPictureBox();
            this.anchorPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prevPictrue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.anchorPropertyGrid);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnAddImg);
            this.groupBox1.Controls.Add(this.listRegion);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.listActor);
            this.groupBox1.Location = new System.Drawing.Point(20, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(152, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 22);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddImg
            // 
            this.btnAddImg.Font = new System.Drawing.Font("Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnAddImg.Location = new System.Drawing.Point(189, 246);
            this.btnAddImg.Name = "btnAddImg";
            this.btnAddImg.Size = new System.Drawing.Size(34, 88);
            this.btnAddImg.TabIndex = 4;
            this.btnAddImg.Text = ">>";
            this.btnAddImg.UseVisualStyleBackColor = true;
            this.btnAddImg.Click += new System.EventHandler(this.btnAddImg_Click);
            // 
            // listRegion
            // 
            this.listRegion.FormattingEnabled = true;
            this.listRegion.ItemHeight = 12;
            this.listRegion.Location = new System.Drawing.Point(6, 246);
            this.listRegion.Name = "listRegion";
            this.listRegion.Size = new System.Drawing.Size(177, 88);
            this.listRegion.TabIndex = 3;
            this.listRegion.SelectedIndexChanged += new System.EventHandler(this.listRegion_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(78, 20);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(71, 22);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "삭제";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 22);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listActor
            // 
            this.listActor.FormattingEnabled = true;
            this.listActor.ItemHeight = 12;
            this.listActor.Location = new System.Drawing.Point(6, 44);
            this.listActor.Name = "listActor";
            this.listActor.Size = new System.Drawing.Size(217, 196);
            this.listActor.TabIndex = 0;
            this.listActor.SelectedIndexChanged += new System.EventHandler(this.listActor_SelectedIndexChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(268, 20);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(340, 28);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(624, 20);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(71, 27);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 507);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(722, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(121, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // chkGuide
            // 
            this.chkGuide.AutoSize = true;
            this.chkGuide.Checked = true;
            this.chkGuide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGuide.Location = new System.Drawing.Point(607, 474);
            this.chkGuide.Name = "chkGuide";
            this.chkGuide.Size = new System.Drawing.Size(88, 16);
            this.chkGuide.TabIndex = 6;
            this.chkGuide.Text = "가이드 라인";
            this.chkGuide.UseVisualStyleBackColor = true;
            this.chkGuide.CheckedChanged += new System.EventHandler(this.chkGuide_CheckedChanged);
            // 
            // prevPictrue
            // 
            this.prevPictrue.BackColor = System.Drawing.Color.White;
            this.prevPictrue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prevPictrue.GuidLine = true;
            this.prevPictrue.Location = new System.Drawing.Point(268, 54);
            this.prevPictrue.Name = "prevPictrue";
            this.prevPictrue.Play = false;
            this.prevPictrue.Size = new System.Drawing.Size(427, 414);
            this.prevPictrue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.prevPictrue.TabIndex = 1;
            this.prevPictrue.TabStop = false;
            this.prevPictrue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.prevPictrue_MouseMove);
            // 
            // anchorPropertyGrid
            // 
            this.anchorPropertyGrid.HelpVisible = false;
            this.anchorPropertyGrid.Location = new System.Drawing.Point(6, 340);
            this.anchorPropertyGrid.Name = "anchorPropertyGrid";
            this.anchorPropertyGrid.Size = new System.Drawing.Size(220, 130);
            this.anchorPropertyGrid.TabIndex = 6;
            this.anchorPropertyGrid.ToolbarVisible = false;
            this.anchorPropertyGrid.Click += new System.EventHandler(this.anchorPropertyGrid_Click);
            // 
            // ActorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 529);
            this.Controls.Add(this.chkGuide);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.prevPictrue);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActorForm";
            this.Text = "Actor Infomation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActorForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prevPictrue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox listActor;
        private ActorPictureBox prevPictrue;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListBox listRegion;
        private System.Windows.Forms.Button btnAddImg;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkGuide;
        private System.Windows.Forms.PropertyGrid anchorPropertyGrid;
    }
}