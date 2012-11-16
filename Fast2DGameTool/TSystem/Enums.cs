using System;

namespace Tool.TSystem
{         
    public enum BlendType
    {
        None = 0,
        Add,
        Modulate,
        MultyAdd,
    }
    
    public enum AlphaType
    {
        Normal = 0,
        FullAlpha,
        ColorKey,
        Max,
    }

    public enum DxPixelFormat
    {
        A8R8G8B8,
        A4R4G4B4,
        Dxt1,
        Dxt3,
        Dxt5,
        A1R5G5B5,
        A16B16G16R16F,
        R32F,
        Max,
    }

    public enum Edge
    {
        Up = 0,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
        Max,
    }

    [Flags]
    public enum AnchorType
    {
        LEFT = 1,
        RIGHT = 2,
        TOP = 4,
        BOTTOM = 8,
        LT = 5,
        RT = 6,
        LB = 9,
        RB = 10,
        MOVE = 11,
    };

    [Flags]
    public enum FlagPosition
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        Left = 4,
        Right = 8,
    }

    public enum E_Axis
    {
        X,
        Y,
        Z
    }

    public enum E_TextAlign
    {
        Left = 0,
        Center,
        Right,
        Top,
        TopLeft,
        TopCenter,
        Max,
    }

    public enum E_EqualSize
    {
        Equal = 0,
        Width,
        Height,
    }

    public enum E_MapRotate
    {
        Left = 0,
        Right,
    }

    public enum E_AxisX
    {
        Left = 0,
        Center,
        Right,
        Inverse,
        Center_Per,
        Right_Per,
        Inverse_Per,
        DOCK,
    }

    public enum E_AxisY
    {
        Top = 0,
        Center,
        Bottom,
        Inverse,
        Center_Per,
        Bottom_Per,
        Inverse_Per,
        DOCK_LEFT,
        DOCK_RIGHT,
        DOCK_TOP,
        DOCK_BOTTOM,
    }
    
    [FlagsAttribute]
    public enum AxisFlag : short
    {
        None = 0,
        X = 1,
        Y = 2,
        Z = 4,
        All = X | Y | Z
    }
    
    [FlagsAttribute]
    public enum FlagDir : int
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        All = Up | Down | Left | Right
    }

    public enum IODataType
    {
        Root,
        Script,
        Sound,
        Image,
        Effect,
        Max
    }
    
    #region Key Enums

    [FlagsAttribute]
    public enum LockKey : int
    {
        None = 0,
        Ctrl = 1,
        Shift = 2,
        Alt = 4,
    }

    [FlagsAttribute]
    public enum ControlKey : int
    {
        None = 0,
        LRotate = 1,
        RRotate = 2,
        Front = 4,
        Back = 8,
        Left = 16,
        Right = 32,

    }


    public enum TKey
    {
        NONE = 0,
        LEFTMOUSE = 1,
        RIGHTMOUSE = 2,
        CANCEL = 3,
        MIDDLEMOUSE = 4,
        XBUTTON1 = 5,
        XBUTTON2 = 6,
        UNKNOWN07 = 7,
        BACKSPACE = 8,
        TAB = 9,
        LINEFEED = 10,
        UNKNOWN0B = 11,
        CLEAR = 12,
        ENTER = 13,
        UNKNOWN0E = 14,
        UNKNOWN0F = 15,
        SHIFT = 16,
        CTRL = 17,
        ALT = 18,
        PAUSE = 19,
        CAPSLOCK = 20,
        HANGULMODE = 21,
        UNKNOWN16 = 22,
        JUNJAMODE = 23,
        FINALMODE = 24,
        HANJAMODE = 25,
        UNKNOWN1A = 26,
        ESCAPE = 27,
        IMECONVERT = 28,
        IMENONCONVERT = 29,
        IMEACEEPT = 30,
        IMEMODECHANGE = 31,
        SPACE = 32,
        PAGEUP = 33,
        PAGEDOWN = 34,
        END = 35,
        HOME = 36,
        LEFT = 37,
        UP = 38,
        RIGHT = 39,
        DOWN = 40,
        SELECT = 41,
        PRINT = 42,
        EXECUTE = 43,
        PRINTSCRN = 44,
        INSERT = 45,
        DELETE = 46,
        HELP = 47,
        NUM0 = 48,
        NUM1 = 49,
        NUM2 = 50,
        NUM3 = 51,
        NUM4 = 52,
        NUM5 = 53,
        NUM6 = 54,
        NUM7 = 55,
        NUM8 = 56,
        NUM9 = 57,
        UNKNOWN3A = 58,
        UNKNOWN3B = 59,
        UNKNOWN3C = 60,
        UNKNOWN3D = 61,
        UNKNOWN3E = 62,
        UNKNOWN3F = 63,
        UNKNOWN40 = 64,
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,
        LWIN = 91,
        RWIN = 92,
        APPS = 93,
        UNKNOWN5E = 94,
        SLEEP = 95,
        NUMPAD0 = 96,
        NUMPAD1 = 97,
        NUMPAD2 = 98,
        NUMPAD3 = 99,
        NUMPAD4 = 100,
        NUMPAD5 = 101,
        NUMPAD6 = 102,
        NUMPAD7 = 103,
        NUMPAD8 = 104,
        NUMPAD9 = 105,
        MULTIPLY = 106,
        ADD = 107,
        SEPARATOR = 108,
        SUBTRACT = 109,
        DECIMAL = 110,
        DIVIDE = 111,
        F1 = 112,
        F2 = 113,
        F3 = 114,
        F4 = 115,
        F5 = 116,
        F6 = 117,
        F7 = 118,
        F8 = 119,
        F9 = 120,
        F10 = 121,
        F11 = 122,
        F12 = 123,
        F13 = 124,
        F14 = 125,
        F15 = 126,
        F16 = 127,
        F17 = 128,
        F18 = 129,
        F19 = 130,
        F20 = 131,
        F21 = 132,
        F22 = 133,
        F23 = 134,
        F24 = 135,
        UNKNOWN88 = 136,
        UNKNOWN89 = 137,
        UNKNOWN8A = 138,
        UNKNOWN8B = 139,
        UNKNOWN8C = 140,
        UNKNOWN8D = 141,
        UNKNOWN8E = 142,
        UNKNOWN8F = 143,
        NUMLOCK = 144,
        SCROLLLOCK = 145,
        UNKNOWN92 = 146,
        UNKNOWN93 = 147,
        UNKNOWN94 = 148,
        UNKNOWN95 = 149,
        UNKNOWN96 = 150,
        UNKNOWN97 = 151,
        UNKNOWN98 = 152,
        UNKNOWN99 = 153,
        UNKNOWN9A = 154,
        UNKNOWN9B = 155,
        UNKNOWN9C = 156,
        UNKNOWN9D = 157,
        UNKNOWN9E = 158,
        UNKNOWN9F = 159,
        LSHIFT = 160,
        RSHIFT = 161,
        LCONTROL = 162,
        RCONTROL = 163,
        UNKNOWNA4 = 164,
        UNKNOWNA5 = 165,
        UNKNOWNA6 = 166,
        UNKNOWNA7 = 167,
        UNKNOWNA8 = 168,
        UNKNOWNA9 = 169,
        UNKNOWNAA = 170,
        UNKNOWNAB = 171,
        UNKNOWNAC = 172,
        UNKNOWNAD = 173,
        UNKNOWNAE = 174,
        UNKNOWNAF = 175,
        UNKNOWNB0 = 176,
        UNKNOWNB1 = 177,
        UNKNOWNB2 = 178,
        UNKNOWNB3 = 179,
        UNKNOWNB4 = 180,
        UNKNOWNB5 = 181,
        UNKNOWNB6 = 182,
        UNKNOWNB7 = 183,
        UNKNOWNB8 = 184,
        UNKNOWNB9 = 185,
        SEMICOLON = 186,
        EQUALS = 187,
        COMMA = 188,
        MINUS = 189,
        PERIOD = 190,
        SLASH = 191,
        TILDE = 192,
        UNKNOWNC1 = 193,
        UNKNOWNC2 = 194,
        UNKNOWNC3 = 195,
        UNKNOWNC4 = 196,
        UNKNOWNC5 = 197,
        UNKNOWNC6 = 198,
        UNKNOWNC7 = 199,
        JOY1 = 200,
        JOY2 = 201,
        JOY3 = 202,
        JOY4 = 203,
        JOY5 = 204,
        JOY6 = 205,
        JOY7 = 206,
        JOY8 = 207,
        JOY9 = 208,
        JOY10 = 209,
        JOY11 = 210,
        JOY12 = 211,
        JOY13 = 212,
        JOY14 = 213,
        JOY15 = 214,
        JOY16 = 215,
        UNKNOWND8 = 216,
        UNKNOWND9 = 217,
        UNKNOWNDA = 218,
        LEFTBRACKET = 219,
        BACKSLASH = 220,
        RIGHTBRACKET = 221,
        SINGLEQUOTE = 222,
        UNKNOWNDF = 223,
        JOYX = 224,
        JOYY = 225,
        JOYZ = 226,
        JOYR = 227,
        MOUSEX = 228,
        MOUSEY = 229,
        MOUSEZ = 230,
        MOUSEW = 231,
        JOYU = 232,
        JOYV = 233,
        UNKNOWNEA = 234,
        UNKNOWNEB = 235,
        MOUSEWHEELUP = 236,
        MOUSEWHEELDOWN = 237,
        UNKNOWN10E = 238,
        UNKNOWN10F = 239,
        JOYPOVUP = 240,
        JOYPOVDOWN = 241,
        JOYPOVLEFT = 242,
        JOYPOVRIGHT = 243,
        UNKNOWNF4 = 244,
        UNKNOWNF5 = 245,
        ATTN = 246,
        CRSEL = 247,
        EXSEL = 248,
        EREOF = 249,
        PLAY = 250,
        ZOOM = 251,
        NONAME = 252,
        PA1 = 253,
        OEMCLEAR = 254,
        Max = 255,
    };
    #endregion
}
