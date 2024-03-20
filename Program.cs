using System.IO;
using System.Threading;

namespace Project_jUMPKING
{
    internal class Program
    {
        private static bool Btutorial = false;
        public static bool get_Btutorial()
        {
            return Btutorial;
        }
        static void Main(string[] args)
        {            
            Background background = new Background();
            Background tutorial = new Background(0);
            Princess princess = background.Princess;
            Player tutoplayer = new Player();
            Player player = new Player();
            string[] save = new string[10];

            Console.WriteLine("콘솔창을 최대로 키우고, 시작하려면 아무키나 눌러주세요.");
            ConsoleKeyInfo key = Console.ReadKey(true);

            while (true)
            {
                try
                {
                    Console.SetCursorPosition(213, 0);
                    break;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("콘솔창을 최대로 키우고, 시작하려면 아무키나 눌러주세요.");
                    key = Console.ReadKey(true);
                    continue;
                }
            }
            
                Console.Clear();
                Console.SetCursorPosition(70, 20);
                Console.WriteLine("┌────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.SetCursorPosition(70, 21);
                Console.WriteLine("│                                       Jump King                                        │");
                Console.SetCursorPosition(70, 22);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 23);
                Console.WriteLine("│                                      1. 튜토리얼                                       │");
                Console.SetCursorPosition(70, 24);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 25);
                Console.WriteLine("│                                      2. 게임시작                                       │");
                Console.SetCursorPosition(70, 26);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 27);
                Console.WriteLine("│                                      3. 불러오기                                       │");
                Console.SetCursorPosition(70, 28);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 29);
                Console.WriteLine("│                                      4. 게임종료                                       │");
                Console.SetCursorPosition(70, 30);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 31);
                Console.WriteLine("└────────────────────────────────────────────────────────────────────────────────────────┘");
            int cursor = 23;
            int cursorx = 103;
            Console.SetCursorPosition(cursorx, cursor);
            Console.Write('▶');
            bool whilein = true;
            while (whilein)
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (cursor > 23)
                        {
                            Console.SetCursorPosition(cursorx, cursor);
                            Console.Write("  ");
                            cursor -= 2;
                            Console.SetCursorPosition(cursorx, cursor);
                            Console.Write('▶');
                        }
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (cursor < 29)
                        {
                            Console.SetCursorPosition(cursorx, cursor);
                            Console.Write("  ");
                            cursor += 2;
                            Console.SetCursorPosition(cursorx, cursor);
                            Console.Write('▶');
                        }
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        cursor = 23;
                        whilein = false;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        cursor = 25;
                        whilein = false;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        cursor = 27;
                        whilein = false;
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        cursor = 29;
                        whilein = false;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        whilein = false;
                        break;
                }
            }
            switch (cursor)
            {
                case 23:
                    Btutorial = true;
                    break;
                case 25:
                    break;
                case 27:
                    try
                    {
                        string line;
                        string CurrentDirectory = Directory.GetCurrentDirectory();
                        CurrentDirectory = CurrentDirectory.Substring(0, CurrentDirectory.IndexOf("bin"));
                        string path = CurrentDirectory + "SaveData.txt";
                        //Pass the file path and file name to the StreamReader constructor
                        StreamReader sr = new StreamReader(path);
                        //Read the first line of text
                        int index = 0;                        
                        line = sr.ReadLine();
                        save[index] = line;
                        //Continue to read until you reach end of file
                        while (line != null)
                        {
                            //Read the next line
                            index++;
                            line = sr.ReadLine();
                            save[index] = line;
                        }
                        //close the file
                        sr.Close();
                        player.positionX = int.Parse(save[0].Split(' ')[1]);
                        player.positionY = int.Parse(save[1].Split(' ')[1]);
                        player.direction_right = int.Parse(save[2].Split(' ')[1]);
                        background.Load(save);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("세이브 데이터가 없거나 관리자모드가 아닙니다.");
                        Console.WriteLine("Exception: " + e.Message);
                    }
                    break;
                case 29:
                    Environment.Exit(0);
                    break;
            }
            while (Btutorial)
            {
                try
                {
                    Console.Clear();
                    tutorial.Print_Back();
                    tutorial.DrawChar(tutoplayer.positionX, tutoplayer.positionY, tutoplayer.direction_right);
                    break;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("콘솔창을 최대로 키우고, 시작하려면 아무키나 눌러주세요.");
                    key = Console.ReadKey(true);
                    continue;
                }
            }
            while (Btutorial)
            {
                tutoplayer.Move(tutorial);
                tutoplayer.CalPos(tutorial);
            }
            while (true)
            {
                try
                {
                    Console.Clear();
                    background.Print_Back();
                    background.DrawChar(player.positionX, player.positionY, player.direction_right);
                    break;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("콘솔창을 최대로 키우고, 시작하려면 아무키나 눌러주세요.");
                    key = Console.ReadKey(true);
                    continue;
                }
            }
            while (true)
            {
                player.Move(background);
                player.CalPos(background);
                if (player.positionY < 25)
                {
                    if (princess.check(player.positionX, player.positionY))
                    {
                        background.Ending(player);
                        break;
                    }
                }

            }
        }
        public static void TutorialClear()
        {
            Btutorial = false;
        }
    }
}
//맵설계
//배경 ######
//경사 ######
//아이템 : 세이브 로드, 중력 잠시 무시, 점프거리 늘려주는 옵션 선택 아이템 123(마찰무시도, 높이도) #####
//구간세이브 ######
//중력제어 무중력느낌
//바람
//아래키 점프 제자리 ######
//유사
//마찰
//튜토리얼 ######