using System.Collections.Generic;
using System.ComponentModel;

namespace Tool.TSystem.Res.Sub
{
    [TypeConverter(typeof(ExpandableObjectConverter)), Description("Style Properties"), Browsable(false)]
    public class SubStyle
    {
        private List<CustomParam> m_listParam = new List<CustomParam>();

        private string m_materialName;
        private string m_effectName;
        private float m_alpha;
        private bool m_enableAlpha;
        private int m_polygonCount;
        private bool m_isVisible;

        public SubStyle( int polygon)
        {
            m_polygonCount = polygon;
        }

        [Category("Behavior"), DisplayName("Params"), Description("Custom Param")]
        public CustomParam[] Params
        {
            get { return m_listParam.ToArray(); }
        }

        [Category("Behavior"), Description("Custom Param")]
        public List<CustomParam> ListParam
        {
            get { return m_listParam; }
        }

        [Category("Apperance"), Description("Material Name")]
        public string MaterialName
        {
            get { return m_materialName; }
            set { m_materialName = value; }
        }

        [Category("Apperance"), Description("Effect Name")]
        public string EffectName
        {
            get { return m_effectName; }
            set { m_effectName = value; }
        }

        [Category("Behavior"), Description("Alpha Value")]
        public float Alpha
        {
            get { return m_alpha; }
            set { m_alpha = value; }
        }

        [Category("Behavior"), Description("EnableAlpha")]
        public bool EnableAlpha
        {
            get { return m_enableAlpha; }
            set { m_enableAlpha = value; }
        }

        [Category("Apperance"), Description("Custom Param")]
        public int PolygonCount
        {
            get { return m_polygonCount; }
        }

        [Category("Behavior"), Description("Visible Mesh")]
        public bool IsVisible
        {
            get { return m_isVisible; }
            set { m_isVisible = value; }
        }

        public void Add( CustomParam param )
        {
            m_listParam.Add( param );    
        }

        public override string ToString()
        {
            return m_materialName;
        }
    }
}