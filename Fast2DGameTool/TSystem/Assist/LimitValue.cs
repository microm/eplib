using System.ComponentModel;

namespace Tool.TSystem.Assist
{
    [TypeConverter(typeof(ExpandableObjectConverter)), Description("Label Information")]
    public class LimitValue
    {
        private float m_max;
		private float m_min;
        private bool m_enable=false;

		public LimitValue(float _min, float _max, bool _enable)
        {
            m_max = _max;
            m_min = _min;
            m_enable = _enable;
        }

		public float Max
        {
            get { return m_max; }
            set { m_max = value; }
        }

		public float Min
        {
            get { return m_min; }
            set { m_min = value; }
        }

        public bool Enable
        {
            get { return m_enable; }
            set { m_enable = value; }
        }

		public bool IsIn(float value)
        {
            if (value > m_min && value < m_max)
            {
                return true;
            }
            return false;
        }

        public bool IsEqual( LimitValue another )
        {
            return (m_max == another.Max && m_min == another.Min && m_enable == another.Enable);
        }

        public override string  ToString()
        {
            return string.Format("[{0},{1}]", m_min, m_max);
        }
    }
}
