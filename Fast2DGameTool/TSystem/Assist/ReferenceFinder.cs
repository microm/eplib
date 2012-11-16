namespace Tool.TSystem.Assist
{
	public class ReferenceFinder
	{
		private object m_obj;
		public ReferenceFinder(object obj)
		{
			m_obj = obj;
		}

		public bool Find(object other)
		{
			return ReferenceEquals(m_obj, other);
		}
	}
}
