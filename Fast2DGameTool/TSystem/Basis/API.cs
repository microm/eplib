using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace Tool.TSystem.Basis
{
	public sealed class API
	{
		#region Window Struct
		public enum WindowMessage : uint
		{
			// Misc messages
			Destroy = 0x0002,
			Close = 0x0010,
			Quit = 0x0012,
			Paint = 0x000F,
			SetCursor = 0x0020,
			ActivateApplication = 0x001C,
			EnterMenuLoop = 0x0211,
			ExitMenuLoop = 0x0212,
			NonClientHitTest = 0x0084,
			PowerBroadcast = 0x0218,
			SystemCommand = 0x0112,
			GetMinMax = 0x0024,

			// Keyboard messages
			KeyDown = 0x0100,
			KeyUp = 0x0101,
			Character = 0x0102,
			SystemKeyDown = 0x0104,
			SystemKeyUp = 0x0105,
			SystemCharacter = 0x0106,

			// Mouse messages
			MouseMove = 0x0200,
			LeftButtonDown = 0x0201,
			LeftButtonUp = 0x0202,
			LeftButtonDoubleClick = 0x0203,
			RightButtonDown = 0x0204,
			RightButtonUp = 0x0205,
			RightButtonDoubleClick = 0x0206,
			MiddleButtonDown = 0x0207,
			MiddleButtonUp = 0x0208,
			MiddleButtonDoubleClick = 0x0209,
			MouseWheel = 0x020a,
			XButtonDown = 0x020B,
			XButtonUp = 0x020c,
			XButtonDoubleClick = 0x020d,
			MouseFirst = LeftButtonDown, // Skip mouse move, it happens a lot and there is another message for that
			MouseLast = XButtonDoubleClick,

            IME_Char = 0x286,
            IME_Compostion = 0x10F,
            IME_Compositionfull = 0x284,
            IME_Control = 0x283,
            IME_EndCompostion = 0x10E,
            IME_Keydown = 0x290,
            IME_Keylast = 0x10F,
            IME_KeyUp = 0x291,
            IME_Notify = 0x282,
            IME_Rrequest = 0x288,
            IME_Select = 0x285,
            IME_SetContext = 0x281,
            IME_StartComposition = 0x10D,

			// Sizing
			EnterSizeMove = 0x0231,
			ExitSizeMove = 0x0232,
			Size = 0x0005,
        }

		[StructLayout( LayoutKind.Sequential )]
		public struct Message
		{
			public IntPtr hWnd;
			public WindowMessage msg;
			public IntPtr wParam;
			public IntPtr lParam;
			public uint time;
			public Point p;
		}

		[StructLayout( LayoutKind.Sequential )]
		public struct MonitorInformation
		{
			public uint Size; // Size of this structure
			public Rectangle MonitorRectangle;
			public Rectangle WorkRectangle;
			public uint Flags; // Possible flags
		}

        public enum PeekMessageFlags
        {
            PM_NOREMOVE = 0x00000000,
            PM_REMOVE = 0x00000001,
            PM_NOYIELD = 0x00000002
        }
		#endregion

		#region Windows API calls
		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "winmm.dll" )]
		public static extern IntPtr timeBeginPeriod(uint period);

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "kernel32" )]
		public static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "kernel32" )]
		public static extern bool QueryPerformanceCounter(ref long PerformanceCount);

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "User32.dll", CharSet = CharSet.Auto )]
		public static extern bool GetMonitorInfo(IntPtr hWnd, ref MonitorInformation info);

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "User32.dll", CharSet = CharSet.Auto )]
		public static extern IntPtr MonitorFromWindow(IntPtr hWnd, uint flags);

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "User32.dll", CharSet = CharSet.Auto )]
		public static extern short GetAsyncKeyState(uint key);

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "User32.dll", CharSet = CharSet.Auto )]
		public static extern IntPtr SetCapture(IntPtr handle);

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "User32.dll", CharSet = CharSet.Auto )]
		public static extern bool ReleaseCapture();

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "User32.dll", CharSet = CharSet.Auto )]
		public static extern int GetCaretBlinkTime();

		[SuppressUnmanagedCodeSecurity] // We won't use this maliciously
		[DllImport( "User32.dll", CharSet = CharSet.Auto )]
		public static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);
		
		[DllImport("kernel32.dll", SetLastError = false)]
		public static extern void CopyMemory(IntPtr dst, IntPtr src, uint len);

		[DllImport("kernel32.dll", SetLastError = false)]
		public static extern void ZeroMemory(IntPtr src, uint len);

		#endregion
	}

	
}
