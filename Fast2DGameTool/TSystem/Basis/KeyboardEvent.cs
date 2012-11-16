using System.Diagnostics;

namespace Tool.TSystem.Basis
{
	public class KeyboardEvent
	{
        public enum EventState
        {
            Up = 0,
            Down,
        }

	    private EventState m_state;
		private TKey m_key;
	    private char m_char;
	    private LockKey m_lockKey;
	    private ControlKey m_controlKey;

        public KeyboardEvent()
        {
        }

		public KeyboardEvent(EventState state, TKey key, LockKey lockKey)
		{
			m_state = state;
			m_key = key;
			m_lockKey = lockKey;
		}

		public char Charactor
		{
            get { return m_char; }
            set { m_char = value;}
		}

	    public EventState State
	    {
	        get { return m_state; }
	        set { m_state = value; }
	    }

	    public TKey Key
	    {
	        get { return m_key; }
	        set { m_key = value; }
	    }

	    public LockKey LockKey
	    {
	        get { return m_lockKey; }
	        set { m_lockKey = value; }
	    }

        public bool IsCharactor
        {
            get { return (m_char >= 32 && m_char <= 126); }
        }

		public bool ShiftPressed
		{
			get { return (m_lockKey & LockKey.Shift) == LockKey.Shift; }
		}

		public bool ControlPressed
		{
			get { return (m_lockKey & LockKey.Ctrl) == LockKey.Ctrl; }
		}

	    public bool OnlyControlPressed
	    {
            get { return m_lockKey == LockKey.Ctrl; }
	    }

        public bool OnlyShiftPressed
        {
            get { return m_lockKey == LockKey.Shift; }
        }

        public bool OnlyAltPressed
        {
            get { return m_lockKey == LockKey.Alt; }
        }

		public bool AltPressed
		{
			get { return (m_lockKey & LockKey.Alt) == LockKey.Alt; }
		}

        public static bool LRotatePressed(ControlKey key)
        {
            return (key & ControlKey.LRotate) == ControlKey.LRotate; 
        }

        public static bool RRotatePressed(ControlKey key)
        {
            return (key & ControlKey.RRotate) == ControlKey.RRotate; 
        }

	    public ControlKey ControlKey
	    {
	        get { return m_controlKey; }
	        set { m_controlKey = value; }
	    }

	    public void Clear()
	    {
	        m_key = TKey.NONE;
	        m_char = '\0';
	    }

        public void ClearLockKey()
        {
            m_lockKey = 0;
        }
	}
}