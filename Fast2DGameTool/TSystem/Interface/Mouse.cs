namespace Tool.TSystem.Interface
{
    public enum E_MOUSEEVENT
    {
        MOVE,
        LDOWN,
        LUP,
        RDOWN,
        RUP,
        MDOWN,
        MUP,
        ENTER,
        LEAVE
    };

    public enum E_MOUSEDRAG
    {
        NONE = 0,
        LOCK,
        ON,
        OWN
    };

    public enum E_MOUSEBUTTON
    {
        NO = 0,
        LEFT,
        RIGHT,
        MIDDLE
    };
}
