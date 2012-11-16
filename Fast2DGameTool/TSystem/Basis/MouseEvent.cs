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
			public Point position;
			public int delta;

			public MouseInfo(bool leftButton, bool rightButton, Point position)
				: this(leftButton, false, rightButton, position, 0)
			{
			}

            public MouseInfo(bool leftButton,bool middleButton, bool rightButton, Point position)
				: this(leftButton, middleButton, rightButton, position, 0)
            {
            }

			public MouseInfo(bool leftButton, bool middleButton, bool rightButton, Point position, int delta)
			{
				this.leftButton = leftButton;
				this.middleButton = middleButton;
				this.rightButton = rightButton;
				this.position = position;
				this.delta = delta;
			}
		}

		private MouseInfo m_curInfo = new MouseInfo();
        private Point m_previousPosition = new Point(0,0);
		private EventState m_state;

		public MouseEvent(EventState eventState, MouseInfo mouseInfo, Point previousPosition)
		{
			m_curInfo = mouseInfo;
			m_state = eventState;
			m_previousPosition = previousPosition;
		}

		public MouseEvent(EventState eventState, MouseInfo mouseInfo)
			: this(eventState, mouseInfo, new Point(0, 0))
		{
		}

        public MouseEvent()
			: this(EventState.Move, new MouseInfo(false, false, new Point(0, 0)), new Point(0, 0))
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

	    public Point PreviousPosition
	    {
	        get { return m_previousPosition; }
	    }

	    public void Update() // Prev Pos Save
	    {
            m_previousPosition = m_curInfo.position;
	    }
	}
}