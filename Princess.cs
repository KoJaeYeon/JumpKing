namespace Project_jUMPKING
{
    internal class Princess
    {
        private int x, y;
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }

        public Princess(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void moveleft()
        {
            x -= 1;
        }

        public string[] Text()
        { return text; }
        
        public void Print_Princess()
        {

            Console.SetCursorPosition(x-2, y-2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ◇  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x-2, y-1);
            Console.Write("▨");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("^^");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("▧  ");
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("▩  ");
            Console.SetCursorPosition(x- 2, y+1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("▨▥▧  ");
            Console.SetCursorPosition(x - 2, y + 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" │ │   ");
            Console.ResetColor();
        }

        public bool check(int x, int y)
        {
            if ((x >= this.x - 6 && x <= this.x + 6) && (y >= this.y -3 && y <= this.y + 2))
                return true;
            return false;
        }

        private readonly string[] text =
{
            "Knight?",
            "Knight ,Is...... is it really you?",
            "You were alive!",
            "oh......",
            "I...",
            "I thought you might have given up or died...",
            "But I kept believing...!!!",
            "That you",
            "That, you...!!!",
            "That, you could make it this far, I believed!!!",
            "Thank you, really thank you, Knight.",
            "I love you, Knight.",
            "I love you"
        };

    }
}
