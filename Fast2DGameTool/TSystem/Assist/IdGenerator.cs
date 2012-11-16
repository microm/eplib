namespace Tool.TSystem.Assist
{
    public class IdGenerator
    {
    	private BitTrain m_ids;

    	public IdGenerator(int size)
    	{
    		m_ids = new BitTrain(size);
    	}

		public uint GetId()
		{
			int id = m_ids.FirstUnUsedIndex();
			m_ids.Set(id, true);

			return (uint) id;
		}

		public void RemoveId(uint id)
		{
			m_ids.Set((int)id, false);
		}

		public void SetId(uint id)
		{
			m_ids.Set((int)id, true);
		}

		public int GetIdCount()
		{
			return m_ids.UsedCount;
		}

		public void Clear()
		{
			int size = m_ids.TotalSeatCount;
			m_ids = new BitTrain(size);
		}
    }
}
