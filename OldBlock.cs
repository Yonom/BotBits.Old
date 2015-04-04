using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotBits.Old
{
    public static class OldBlock
    {
        public static class Gravity
        {
            public static Foreground.Id
                Empty = 0,
                Left = (Foreground.Id)1,
                Up = (Foreground.Id)2,
                Right = (Foreground.Id)3,
                Dot = (Foreground.Id)4;
        }


        public static class Basic
        {
            public static Foreground.Id
                Gray = (Foreground.Id)5,
                Blue = (Foreground.Id)6,
                Pink = (Foreground.Id)7,
                Red = (Foreground.Id)8,
                Yellow = (Foreground.Id)9,
                Green = (Foreground.Id)10,
                LightBlue = (Foreground.Id)11;
        }

        public static class Brick
        {
            public static Foreground.Id

                Brown = (Foreground.Id)12,
                Teal = (Foreground.Id)13,
                Violet = (Foreground.Id)14,
                Green = (Foreground.Id)15;
        }

        public static class Special
        {
            public static Foreground.Id
                Face = (Foreground.Id)16;
            public static Foreground.Id
                FullyBlack = (Foreground.Id)20;
        }

        public static class Metal
        {
            public static Foreground.Id
                White = (Foreground.Id)17,
                Red = (Foreground.Id)18,
                Yellow = (Foreground.Id)19;
        }
    }
}
