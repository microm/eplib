using System.Diagnostics;

namespace Tool.TSystem.Basis
{
    public static class KeyEventTranslator
    {
        public static bool MessageProc( int msg , int wParam ,int lParam , KeyboardEvent keyEvent )
        {
            keyEvent.Clear();
            
            switch ((API.WindowMessage)msg)
            {
                case API.WindowMessage.Character:
                    keyEvent.Charactor = (char)wParam;
                    return true;
				case API.WindowMessage.SystemKeyDown:
                case API.WindowMessage.KeyDown:
                    keyEvent.State = KeyboardEvent.EventState.Down;
                    keyEvent.Key = (TKey)wParam;

                    AddLockKey( keyEvent );
                    AddControlKey(keyEvent);
            		return true;

                case API.WindowMessage.SystemKeyUp:
                case API.WindowMessage.KeyUp:
                    keyEvent.State = KeyboardEvent.EventState.Up;
                    keyEvent.Key = (TKey)wParam;

                    SubLockKey( keyEvent);
                    SubControlKey(keyEvent);
            		return true;

                case API.WindowMessage.IME_StartComposition:
                    return true;
                case API.WindowMessage.IME_Compostion:
                    if (OnImeComposition(lParam)) return true;
                    break;
                case API.WindowMessage.IME_EndCompostion:

                    break;
                case API.WindowMessage.IME_SetContext:

                    return true;
                case API.WindowMessage.IME_Notify:

                    break;
            }
            return false;
        }

        private static bool OnImeComposition(int param)
        {
            return false;
        }

        private static void SubLockKey( KeyboardEvent keyEvent)
        {
            switch (keyEvent.Key)
            {
                case TKey.CTRL:
                    keyEvent.LockKey &= ~LockKey.Ctrl;
            		break;
                case TKey.ALT:
                    keyEvent.LockKey &= ~LockKey.Alt;
            		break;
                case TKey.SHIFT:
                    keyEvent.LockKey &= ~LockKey.Shift;
            		break;
            }
        }

        private static void AddLockKey( KeyboardEvent keyEvent)
        {
            switch (keyEvent.Key)
            {
                case TKey.CTRL:
                    keyEvent.LockKey |= LockKey.Ctrl;
            		break;
                case TKey.ALT:
                    keyEvent.LockKey |= LockKey.Alt;
            		break;
                case TKey.SHIFT:
                    keyEvent.LockKey |= LockKey.Shift;
            		break;
            }
        }

        private static void SubControlKey(KeyboardEvent keyEvent)
        {
            switch (keyEvent.Key)
            {
                case TKey.W:
                    keyEvent.ControlKey &= ~(ControlKey.Front);
                    break;
                case TKey.Q:
                    keyEvent.ControlKey &= ~(ControlKey.Left);
                    break;
                case TKey.E:
                    keyEvent.ControlKey &= ~(ControlKey.Right);
                    break;
                case TKey.S:
                    keyEvent.ControlKey &= ~(ControlKey.Back);
                    break;
                case TKey.A:
                    keyEvent.ControlKey &= ~(ControlKey.LRotate);
                    break;
                case TKey.D:
                    keyEvent.ControlKey &= ~(ControlKey.RRotate);
                    break;
            }
        }

        private static void AddControlKey(KeyboardEvent keyEvent)
        {
            switch (keyEvent.Key)
            {
                case TKey.W:
                    keyEvent.ControlKey |= ControlKey.Front;
                    break;
                case TKey.Q:
                    keyEvent.ControlKey |= ControlKey.Left;
                    break;
                case TKey.E:
                    keyEvent.ControlKey |= ControlKey.Right;
                    break;
                case TKey.S:
                    keyEvent.ControlKey |= ControlKey.Back;
                    break;
                case TKey.A:
                    keyEvent.ControlKey |= ControlKey.LRotate;
                    break;
                case TKey.D:
                    keyEvent.ControlKey |= ControlKey.RRotate;
                    break;
            }
        }
    }
}