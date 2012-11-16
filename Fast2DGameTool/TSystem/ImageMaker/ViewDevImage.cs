using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tool.TSystem.ImageMaker
{
    public class ViewDevImage
    {
        private DevImage m_dImage;
        private Size m_ImageReSize;
        
        public DevImage DImage
        {
            get { return m_dImage; }
        }
        
        public Size ImageSize
        {
            get { return m_ImageReSize; }
            set { m_ImageReSize = value; }
        }
        
        public int Width
        {
            get { return GetBitmap().Width; }
        }

        public int Height
        {
            get { return GetBitmap().Height; }
        }
        
        public ViewDevImage(Size imagesize)
        {
            m_ImageReSize = imagesize;
            m_dImage = new DevImage(m_ImageReSize.Width, m_ImageReSize.Height);
        }

        public ViewDevImage(Int32 nWidth, Int32 nHeight)
        {
            m_ImageReSize.Width = nWidth;
            m_ImageReSize.Height = nHeight;

            m_dImage = new DevImage(m_ImageReSize.Width, m_ImageReSize.Height);
        }
        public ViewDevImage(Bitmap bmp)
        {
            m_dImage = new DevImage(bmp);

            m_ImageReSize.Width = m_dImage.Width;
            m_ImageReSize.Height = m_dImage.Height;
        }
        public void ReSizeToRate(float fRate)
        {
            m_ImageReSize.Width = (Int32)((float)m_ImageReSize.Width * fRate);
            m_ImageReSize.Height = (Int32)((float)m_ImageReSize.Height * fRate);
            Update();
        }
        public void ReSize(int width, int height)
        {
            m_ImageReSize.Width = width;
            m_ImageReSize.Height = height;
            m_dImage.ReSize(m_ImageReSize.Width, m_ImageReSize.Height);
        }

        public bool SaveFile(string path)
        {
            m_dImage.Save(path);
            return true;
        }

        public bool FlipSaveFile(string path)
        {
            m_dImage.FlipFilter();
            m_dImage.Save(path);
            return true;
        }

        public bool LoadFile(string path)
        {
            if(m_dImage == null ) return false;

            if (m_dImage.Load(path))
            {
                if (m_ImageReSize.Width == 1 && m_ImageReSize.Height == 1)
                {
                    m_ImageReSize.Width = m_dImage.Width;
                    m_ImageReSize.Height = m_dImage.Height;
                    Update();
                }
                return true;
            }
            return false;
        }
        public Bitmap GetBitmap()
        {
            if (m_dImage == null) return null;

            return m_dImage.BmpImage;
        }

        public void Update()
        {
            if (m_dImage == null) return;

            m_dImage.ReSize(m_ImageReSize.Width, m_ImageReSize.Height);
        }

        public void CopyDevImage(int _x, int _y, ViewDevImage image)
        {
            if (m_dImage == null) return;

            m_dImage.CopyImg(_x, _y, image.DImage);
        }

        static public void CopyBitmap(int _x, int _y, Bitmap rootimage, Bitmap childimage)
        {
            Rectangle Rectsrc = new Rectangle(0, 0, childimage.Width, childimage.Height);
            Rectangle RectTaget = new Rectangle(_x, _y, childimage.Width, childimage.Height);
            
            Graphics g = Graphics.FromImage(rootimage);
            g.DrawImage(childimage, RectTaget, Rectsrc, GraphicsUnit.Pixel);
        }
    }
}
