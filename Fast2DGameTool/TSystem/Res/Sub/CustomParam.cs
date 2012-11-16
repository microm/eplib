using System.ComponentModel;
using System.Drawing;
using Tool.TSystem.Primitive;

namespace Tool.TSystem.Res
{
    
    [TypeConverter(typeof(ExpandableObjectConverter)), Description("Custom Param"), Browsable(false)]
    public class CustomParam
    {
        private EParamType m_type;
        private int m_count;
        private string m_paramName;
        private string m_texName;

        private int m_intValue;
        private float m_floatValue;
        private Color m_colorValue;

        public CustomParam( EParamType eType , string paramName )
        {
            m_type = eType;
            m_paramName = paramName;
        }

        [Category("Value"), Description("Type")]
        public EParamType Type
        {
            get { return m_type; }
        }

        [Category("Info"), Description("ParamName")]
        public string ParamName
        {
            get { return m_paramName; }
            set { m_paramName = value; }
        }

        [Category("Info"), Description("TexName")]
        public string TexName
        {
            get { return m_texName; }
            set { m_texName = value; }
        }

        [Category("Info"), Description("Count")]
        public int Count
        {
            get { return m_count; }
            set { m_count = value; }
        }

        [Category("Value"), Description("Int")]
        public int IntValue
        {
            get { return m_intValue; }
            set { m_intValue = value; }
        }

        [Category("Value"), Description("Float")]
        public float FloatValue
        {
            get { return m_floatValue; }
            set { m_floatValue = value; }
        }

        [Category("Value"), Description("Color")]
        public Color ColorValue
        {
            get { return m_colorValue; }
            set { m_colorValue = value; }
        }

        public override string ToString()
        {
            return m_paramName;
        }
    }
}