using System.Windows.Forms;
using Tool.TSystem.Primitive;

namespace Tool.TSystem.Basis
{
	public class MouseEventTranslator
	{
		private MouseEvent m_previousEvent;

		public MouseEventTranslator()
		{
			m_previousEvent = new MouseEvent();
		}

		public MouseEvent MouseMove(MouseEventArgs e)
		{
			MouseEvent.MouseInfo mouseInfo = new MouseEvent.MouseInfo(false, false, new TPoint(e.X, e.Y));
			InheritButtonStatesFromPreviousMouseEvent(ref mouseInfo);

			MouseEvent mouseEvent = new MouseEvent(MouseEvent.EventState.Move, mouseInfo, m_previousEvent.Info.position);
			m_previousEvent = mouseEvent;
			return mouseEvent;
		}
		
		public MouseEvent MouseDown(MouseEventArgs e)
		{
			MouseEvent.MouseInfo mouseInfo = new MouseEvent.MouseInfo(false, false, new TPoint(e.X, e.Y));
			InheritButtonStatesFromPreviousMouseEvent(ref mouseInfo);

			if (e.Button == MouseButtons.Left)
			{
				mouseInfo.leftButton = true;
				MouseEvent mouseEvent = new MouseEvent(MouseEvent.EventState.LDown, mouseInfo, mouseInfo.position);
				m_previousEvent = mouseEvent;
				return mouseEvent;
			}
			else if (e.Button == MouseButtons.Middle)
			{
				mouseInfo.middleButton = true;
				MouseEvent mouseEvent = new MouseEvent(MouseEvent.EventState.MDown, mouseInfo, mouseInfo.position);
				m_previousEvent = mouseEvent;
				return mouseEvent;
			}
			else if (e.Button == MouseButtons.Right)
			{
				mouseInfo.rightButton = true;
				MouseEvent mouseEvent = new MouseEvent(MouseEvent.EventState.RDown, mouseInfo, mouseInfo.position);
				m_previousEvent = mouseEvent;
				return mouseEvent;
			}

			return null;
		}

		private void InheritButtonStatesFromPreviousMouseEvent(ref MouseEvent.MouseInfo mouseInfo)
		{
			if (m_previousEvent != null)
			{
				mouseInfo.leftButton = m_previousEvent.Info.leftButton;
				mouseInfo.middleButton = m_previousEvent.Info.middleButton;
				mouseInfo.rightButton = m_previousEvent.Info.rightButton;
			}
		}

		public MouseEvent MouseUp(MouseEventArgs e)
		{
			MouseEvent.MouseInfo mouseInfo = new MouseEvent.MouseInfo(false, false, new TPoint(e.X, e.Y));
			InheritButtonStatesFromPreviousMouseEvent(ref mouseInfo);

			if (e.Button == MouseButtons.Left)
			{
				mouseInfo.leftButton = false;
				MouseEvent mouseEvent = new MouseEvent(MouseEvent.EventState.LUp, mouseInfo, m_previousEvent.Info.position);
				m_previousEvent = mouseEvent;
				return mouseEvent;
			}
			else if (e.Button == MouseButtons.Middle)
			{
				mouseInfo.middleButton = false;
				MouseEvent mouseEvent = new MouseEvent(MouseEvent.EventState.MUp, mouseInfo, m_previousEvent.Info.position);
				m_previousEvent = mouseEvent;
				return mouseEvent;
			}
			else if (e.Button == MouseButtons.Right)
			{
				mouseInfo.rightButton = false;
				MouseEvent mouseEvent = new MouseEvent(MouseEvent.EventState.RUp, mouseInfo, m_previousEvent.Info.position);
				m_previousEvent = mouseEvent;
				return mouseEvent;
			}

			return null;
		}

		public MouseEvent MouseWheel(MouseEventArgs e)
		{
			MouseEvent.MouseInfo mouseInfo = new MouseEvent.MouseInfo(false, false, false, new TPoint(e.X, e.Y), e.Delta);
			InheritButtonStatesFromPreviousMouseEvent(ref mouseInfo);
			MouseEvent mouseEvent = new MouseEvent(MouseEvent.EventState.Wheel, mouseInfo);
			m_previousEvent = mouseEvent;

			return mouseEvent;
		}
	}
}