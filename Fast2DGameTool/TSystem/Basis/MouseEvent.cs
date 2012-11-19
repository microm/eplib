using Tool.TSystem.Primitive;

namespace Tool.TSystem.Basis
{
	public class MouseEvent
	{
        public enum Buttons
        {
            None = 0,
            Left,
            Right,
            Middle,
        }

		public enum EventState
		{
			Move = 0,
			LDown,
			LUp,
			MDown,
			MUp,
			RDown,
			RUp,
			Wheel,
		}

		public struct MouseInfo
		{
			public bool leftButton;
			public bool rightButton;
		    public bool middleButton;
			public TPoint position;
			public int delta;

			public MouseInfo(bool leftButton, bool rightButton, TPoint position)
				: this(leftButton, false, rightButton, position, 0)
			{
			}

            public MouseInfo(bool leftButton,bool middleButton, bool rightButton, TPoint position)
				: this(leftButton, middleButton, rightButton, position, 0)
            {
            }

			public MouseInfo(bool leftButton, bool middleButton, bool rightButton, TPoint position, int delta)
			{
				this.leftButton = leftButton;
				this.middleButton = middleButton;
				this.rightButton = rightButton;
				this.position = position;
				this.delta = delta;
			}
		}

		private MouseInfo m_curInfo = new MouseInfo();
        private TPoint m_previousPosition = new TPoint(0,0);
		private EventState m_state;

		public MouseEvent(EventState eventState, MouseInfo mouseInfo, TPoint previousPosition)
		{
			m_curInfo = mouseInfo;
			m_state = eventState;
			m_previousPosition = previousPosition;
		}

		public MouseEvent(EventState eventState, MouseInfo mouseInfo)
			: this(eventState, mouseInfo, new TPoint(0, 0))
		{
		}

        public MouseEvent()
			: this(EventState.Move, new MouseInfo(false, false, new TPoint(0, 0)), new TPoint(0, 0))
        {
        }

	    public MouseInfo Info
		{
			get { return m_curInfo; }
            set { m_curInfo = value; }
		}

		public EventState State
		{
			get { return m_state; }
			set { m_state = value; }
		}

	    public TPoint PreviousPosition
	    {
	        get { return m_previousPosition; }
	    }

	    public void Update() // Prev Pos Save
	    {
            m_previousPosition = m_curInfo.position;
	    }
	}
}