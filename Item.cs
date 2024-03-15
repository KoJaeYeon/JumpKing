using System;

namespace Project_jUMPKING
{
    class Item
    {
        protected int posX, posY;
        protected bool _GetItem;
        protected string[][] sprite = new string[2][];
        private int index;
        private int time = 100;
        private char _tempChar;
        protected char _Char;

        public bool getItem
        {
            get { return _GetItem; }
            set { _GetItem = value; }
        }
        public int get_posX
        {
            get { return posX; }
        }
        public int get_posY
        {
            get { return posY; }
        }
        public char get_Char
        {
            get { return _Char; }
        }
        public char get_tempChar
        {
            get { return _tempChar; }
        }

        public Item(int posX, int posY, char tempChar)
        {
            this.posX = posX;
            this.posY = posY;   
            _GetItem = false;
            _tempChar = tempChar;
        }

        public void PrintItem(int i)
        {
            if (_GetItem == true)
            {
                return;
            }
            time++;
            if(time > 50)
            {
                time += 1 + i;
                index++;
                index %= 2;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(posX - 2, posY - 2);
                Console.Write(sprite[index][0]);
                Console.SetCursorPosition(posX - 2, posY - 1);
                Console.Write(sprite[index][1]);
                Console.SetCursorPosition(posX - 2, posY);
                Console.Write(sprite[index][2]);
                Console.SetCursorPosition(posX - 2, posY + 1);
                Console.Write(sprite[index][3]);
                Console.SetCursorPosition(posX - 2, posY + 2);
                Console.Write(sprite[index][4]);
                time = 0;
                Console.ResetColor();
            }
        }
    }

    class Higher : Item
    {
        public Higher(int posX, int posY, char tempChar) : base(posX, posY, tempChar)
        {
            sprite[0] = sprite1;
            sprite[1] = sprite2;
            _Char = sprite1[2][2];
        }

        private readonly string[] sprite1 =
{
        "*#*#*",
        "#*1*#",
        "*111*",
        "#*1*#",
        "*#*#*",
        };

        private readonly string[] sprite2 =
{
        "#*#*#",
        "*#1#*",
        "#111#",
        "*#1#*",
        "#*#*#"
        };
    }

    class Further : Item
    {
        public Further(int posX, int posY, char tempChar) : base(posX, posY, tempChar)
        {
            sprite[0] = sprite1;
            sprite[1] = sprite2;
            _Char = sprite1[2][2];
        }

        private readonly string[] sprite1 =
{
        "*#*#*",
        "#*2*#",
        "*222*",
        "#*2*#",
        "*#*#*",
        };

        private readonly string[] sprite2 =
{
        "#*#*#",
        "*#2#*",
        "#222#",
        "*#2#*",
        "#*#*#"
        };
    }

    class Longer : Item
    {
        public Longer(int posX, int posY, char tempChar) : base(posX, posY, tempChar)
        {
            sprite[0] = sprite1;
            sprite[1] = sprite2;
            _Char = sprite1[2][2];
        }
        private readonly string[] sprite1 =
{
        "*#*#*",
        "#*3*#",
        "*333*",
        "#*3*#",
        "*#*#*",
        };

        private readonly string[] sprite2 =
{
        "#*#*#",
        "*#3#*",
        "#333#",
        "*#3#*",
        "#*#*#"
        };
    }
}
