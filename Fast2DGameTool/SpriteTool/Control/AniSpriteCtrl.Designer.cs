
namespace SpriteTool.Control
{
    partial class AniSpriteCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AniSpriteCtrl));
            this.SpriteGroup = new System.Windows.Forms.GroupBox();
            this.chkParts = new System.Windows.Forms.CheckBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.chkCircle = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.colorKeyPanel = new System.Windows.Forms.Panel();
            this.chkColorKey = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrame = new System.Windows.Forms.TextBox();
            this.btnSpriteCreate = new System.Windows.Forms.Button();
            this.txtSprite = new System.Windows.Forms.TextBox();
            this.spriteTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnActorCreate = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.pivotPic = new SpriteTool.Control.PivotPictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SpriteGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotPic)).BeginInit();
            this.SuspendLayout();
            // 
            // SpriteGroup
            // 
            this.SpriteGroup.Controls.Add(this.chkParts);
            this.SpriteGroup.Controls.Add(this.btnRename);
            this.SpriteGroup.Controls.Add(this.chkCircle);
            this.SpriteGroup.Controls.Add(this.btnDelete);
            this.SpriteGroup.Controls.Add(this.colorKeyPanel);
            this.SpriteGroup.Controls.Add(this.chkColorKey);
            this.SpriteGroup.Controls.Add(this.label1);
            this.SpriteGroup.Controls.Add(this.txtFrame);
            this.SpriteGroup.Controls.Add(this.btnSpriteCreate);
            this.SpriteGroup.Controls.Add(this.txtSprite);
            this.SpriteGroup.Controls.Add(this.spriteTreeView);
            this.SpriteGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.SpriteGroup.Location = new System.Drawing.Point(0, 0);
            this.SpriteGroup.Margin = new System.Windows.Forms.Padding(5);
            this.SpriteGroup.Name = "SpriteGroup";
            this.SpriteGroup.Size = new System.Drawing.Size(209, 405);
            this.SpriteGroup.TabIndex = 0;
            this.SpriteGroup.TabStop = false;
            this.SpriteGroup.Text = "Sprite";
            // 
            // chkParts
            // 
            this.chkParts.AutoSize = true;
            this.chkParts.Location = new System.Drawing.Point(6, 375);
            this.chkParts.Name = "chkParts";
            this.chkParts.Size = new System.Drawing.Size(115, 16);
            this.chkParts.TabIndex = 11;
            this.chkParts.Text = "Parts Transform";
            this.chkParts.UseVisualStyleBackColor = true;
            this.chkParts.CheckedChanged += new System.EventHandler(this.chkParts_CheckedChanged);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(109, 21);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(47, 20);
            this.btnRename.TabIndex = 10;
            this.btnRename.Text = "변경";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // chkCircle
            // 
            this.chkCircle.AutoSize = true;
            this.chkCircle.Location = new System.Drawing.Point(6, 353);
            this.chkCircle.Name = "chkCircle";
            this.chkCircle.Size = new System.Drawing.Size(57, 16);
            this.chkCircle.TabIndex = 9;
            this.chkCircle.Text = "Circle";
            this.chkCircle.UseVisualStyleBackColor = true;
            this.chkCircle.CheckedChanged += new System.EventHandler(this.chkCircle_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(145, 326);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(58, 20);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // colorKeyPanel
            // 
            this.colorKeyPanel.BackColor = System.Drawing.Color.White;
            this.colorKeyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorKeyPanel.Location = new System.Drawing.Point(88, 329);
            this.colorKeyPanel.Name = "colorKeyPanel";
            this.colorKeyPanel.Size = new System.Drawing.Size(29, 18);
            this.colorKeyPanel.TabIndex = 7;
            // 
            // chkColorKey
            // 
            this.chkColorKey.AutoSize = true;
            this.chkColorKey.Location = new System.Drawing.Point(6, 331);
            this.chkColorKey.Name = "chkColorKey";
            this.chkColorKey.Size = new System.Drawing.Size(76, 16);
            this.chkColorKey.TabIndex = 6;
            this.chkColorKey.Text = "ColorKey";
            this.chkColorKey.UseVisualStyleBackColor = true;
            this.chkColorKey.CheckedChanged += new System.EventHandler(this.chkColorKey_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 355);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "갱신속도";
            // 
            // txtFrame
            // 
            this.txtFrame.Location = new System.Drawing.Point(127, 352);
            this.txtFrame.Name = "txtFrame";
            this.txtFrame.Size = new System.Drawing.Size(76, 21);
            this.txtFrame.TabIndex = 3;
            this.txtFrame.Text = "5";
            this.txtFrame.TextChanged += new System.EventHandler(this.txtFrame_TextChanged);
            // 
            // btnSpriteCreate
            // 
            this.btnSpriteCreate.Location = new System.Drawing.Point(158, 21);
            this.btnSpriteCreate.Name = "btnSpriteCreate";
            this.btnSpriteCreate.Size = new System.Drawing.Size(45, 20);
            this.btnSpriteCreate.TabIndex = 2;
            this.btnSpriteCreate.Text = "생성";
            this.btnSpriteCreate.UseVisualStyleBackColor = true;
            this.btnSpriteCreate.Click += new System.EventHandler(this.btnSpriteCreate_Click);
            // 
            // txtSprite
            // 
            this.txtSprite.Location = new System.Drawing.Point(6, 20);
            this.txtSprite.Name = "txtSprite";
            this.txtSprite.Size = new System.Drawing.Size(97, 21);
            this.txtSprite.TabIndex = 1;
            // 
            // spriteTreeView
            // 
            this.spriteTreeView.ImageIndex = 2;
            this.spriteTreeView.ImageList = this.imageList1;
            this.spriteTreeView.Location = new System.Drawing.Point(5, 46);
            this.spriteTreeView.Name = "spriteTreeView";
            this.spriteTreeView.SelectedImageIndex = 0;
            this.spriteTreeView.Size = new System.Drawing.Size(198, 279);
            this.spriteTreeView.TabIndex = 0;
            this.spriteTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.spriteTreeView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "74(1).gif");
            this.imageList1.Images.SetKeyName(1, "97(2).gif");
            this.imageList1.Images.SetKeyName(2, "98(2).gif");
            this.imageList1.Images.SetKeyName(3, "icon_xls.gif");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnActorCreate);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnPlay);
            this.groupBox1.Controls.Add(this.pivotPic);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 405);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 218);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // btnActorCreate
            // 
            this.btnActorCreate.Location = new System.Drawing.Point(3, 176);
            this.btnActorCreate.Name = "btnActorCreate";
            this.btnActorCreate.Size = new System.Drawing.Size(68, 26);
            this.btnActorCreate.TabIndex = 3;
            this.btnActorCreate.Text = "액터생성";
            this.btnActorCreate.UseVisualStyleBackColor = true;
            this.btnActorCreate.Click += new System.EventHandler(this.btnActorCreate_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(155, 176);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 26);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(106, 176);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(43, 26);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // pivotPic
            // 
            this.pivotPic.BackColor = System.Drawing.Color.White;
            this.pivotPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pivotPic.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pivotPic.Index = 0;
            this.pivotPic.Location = new System.Drawing.Point(3, 17);
            this.pivotPic.Name = "pivotPic";
            this.pivotPic.Play = false;
            this.pivotPic.Size = new System.Drawing.Size(200, 153);
            this.pivotPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pivotPic.TabIndex = 0;
            this.pivotPic.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AniSpriteCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SpriteGroup);
            this.Name = "AniSpriteCtrl";
            this.Size = new System.Drawing.Size(209, 688);
            this.SpriteGroup.ResumeLayout(false);
            this.SpriteGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SpriteGroup;
        private System.Windows.Forms.Button btnSpriteCreate;
        private System.Windows.Forms.TextBox txtSprite;
        private System.Windows.Forms.TreeView spriteTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFrame;
        private PivotPictureBox pivotPic;
        private System.Windows.Forms.CheckBox chkColorKey;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Panel colorKeyPanel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkCircle;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.CheckBox chkParts;
        private System.Windows.Forms.Button btnActorCreate;
    }
}
