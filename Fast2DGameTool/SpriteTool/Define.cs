using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpriteTool
{
    public class Define
    {
        public readonly static int Interval = 1;
        public readonly static int AnchorSize = 6;
        public readonly static int AnchorOffset = Interval + AnchorSize;
        public readonly static int ControlOffset = 5;
        public readonly static Color SelectedColor = Color.Red;
        public readonly static Color ControllerColor = Color.Purple;
        public readonly static int Width = 200;
        public readonly static int Height = 200;
        public readonly static Color LineColor = Color.Black;
        public readonly static Color DragLineColor = Color.Gray;
    }
}
