using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tool.TSystem.Primitive;

namespace SpriteTool.Control
{
    public partial class SelectRegionForm : Form
    {
        private Main m_main;

        public SelectRegionForm()
        {
            InitializeComponent();
        }

        internal void Init(Main main)
        {
            m_main = main;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            TPoint start = TPoint.Parse(txtStart.Text);
            TPoint size = TPoint.Parse(txtSize.Text);
            TPoint offset = TPoint.Parse(txtOffset.Text);

            int Cols = int.Parse(txtCol.Text);            
            int Rows = int.Parse(txtRow.Text);

            m_main.Form.BasePicture.Regions.Clear();

            for (int y = 0; y < Rows; ++y)
            {
                for (int x = 0; x < Cols; ++x)
                {
                    int XPos = start.X + (size.X + offset.X) * x;
                    int YPos = start.Y + (size.Y + offset.Y) * y;
                    Rectangle rect = new Rectangle(XPos, YPos, size.X, size.Y);
                    m_main.Form.BasePicture.Regions.Add(rect);
                }
            }

            m_main.Form.BasePicture.Invalidate();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            TPoint start = TPoint.Parse(txtStart.Text);
            TPoint size = TPoint.Parse(txtSize.Text);
            TPoint offset = TPoint.Parse(txtOffset.Text);

            int Cols = int.Parse(txtCol.Text);
            int Rows = int.Parse(txtRow.Text);

            for (int y = 0; y < Rows; ++y)
            {
                for (int x = 0; x < Cols; ++x)
                {
                    int XPos = start.X + (size.X + offset.X) * x;
                    int YPos = start.Y + (size.Y + offset.Y) * y;
                    Point startPos = new Point(XPos, YPos);
                    Point endPos = new Point(XPos + size.X,YPos + size.Y);

                    m_main.SelectSprite.AddRegion(startPos, endPos);             
                }
            }
            m_main.Form.SpriteCtrl.UpdateData();
            m_main.UpdateSprite();
        }

        private void SelectRegionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_main.Form.BasePicture.Invalidate();
        }
    }
}
