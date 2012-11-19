using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Tool.TSystem.IO;
using SpriteTool.Data;
using System.Xml;
using Tool.TSystem;
using System.Drawing;
using Tool.TSystem.ImageMaker;

namespace SpriteTool
{
    public class Main
    {
        private readonly MainForm m_form;
        private readonly IBrowser m_browser;

        private int m_selectIndex = -1;
        private SpriteMap m_spriteMap;
        private SpriteInfo m_selectSprite = null;
        private ActorList m_actorList;

        private Pen m_linePen = new Pen(Brushes.Black);
        private List<Bitmap> m_imageList = new List<Bitmap>();

        private string m_spriteFileName = "spritemap.xml";
        private string m_actorFileName = "actorlist.xml";

        private DevImage m_devImage;

        public DevImage DevImage
        {
            get { return m_devImage; }
        }

        public SpriteTool.Data.SpriteMap SpriteMap
        {
            get { return m_spriteMap; }
        }

        public IBrowser Browser
        {
            get { return m_browser; }
        }

        public MainForm Form
        {
            get { return m_form; }
        }

        public Pen LinePen
        {
            get { return m_linePen; }
        }

        public List<Bitmap> ImageList
        {
            get { return m_imageList; }
        }

        public int SelectIndex
        {
            get { return m_selectIndex; }
            set { m_selectIndex = value; }
        }
        
        public SpriteMap SpritesMap
        {
            get { return m_spriteMap; }
        }

        public ActorList Actors
        {
            get { return m_actorList; }
        }

        public SpriteInfo SelectSprite
        {
            get { return m_selectSprite; }
            set { m_selectSprite = value;
                if (m_selectSprite == null)
                {
                    m_selectIndex = -1;
                    UpdateSprite();
                }
            }
        }

        public Main(MainForm form)
        {
            m_form = form;
            m_devImage = new DevImage(100, 100);
                       
            m_browser = new Browser();
            m_linePen.Color = Form.LineColor;
            InitData();            
        }
        
        private void InitData()
        {
            m_spriteMap = new SpriteMap();
            Stream stream = m_browser.Read(IODataType.Script, m_spriteFileName);
            m_spriteMap.Read(stream);
            stream.Close();

            m_actorList = new ActorList();
            stream = m_browser.Read(IODataType.Script, m_actorFileName);
            m_actorList.Read(stream);
            stream.Close();
        }

        internal void ImageLoad(string path)
        {
            string filePath = Path.GetFileName(path);
            m_form.BasePicture.LoadPath(filePath);
        }

        internal void SaveFile(string filepath)
        {
            Stream stream = m_browser.Write(IODataType.Script, filepath);
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;           
           
            m_spriteMap.Write(writer);        
            
            writer.Flush();
            stream.Close();
        }
        
        internal void Save()
        {
            SaveFile(m_spriteFileName);
        }

        internal void SaveActor()
        {
            SaveActorFile(m_actorFileName);
        }

        public void SaveActorFile(string path)
        {
            Stream stream = Browser.Write(IODataType.Script, path);
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;

            m_actorList.Write(writer);            

            writer.Flush();
            stream.Close();
        }

        internal void UpdateSprite( int ignoreID = 0 )
        {
            if (ignoreID != 1)
            {
                Form.BasePicture.UpdateSprite();
            }
            if (ignoreID != 2)
            {
                Form.SpriteCtrl.UpdateSprite();
            }
            if (ignoreID != 3)
            {
                Form.ListPicturePanel.UpdateSprite();
            }

        }
    }
}
