using System.IO;
using System.Numerics;
using System.Threading;

namespace Project_jUMPKING
{
    internal class Program
    {
        private static bool Btutorial = false;

        static void Main(string[] args)
        {            
            Background background = new Background();
            Background tutorial = new Background(0);
            Princess princess = background.Princess;
            Player tutoplayer = new Player();
            Player player = new Player();            

            ConsoleKeyInfo key;
            Console.SetWindowSize(237, 63);           
            while (true)
            {
                try
                {
                    Console.SetCursorPosition(213, 0);
                    break;
                    
                }
                catch
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("콘솔창을 휠로 축소하고, 시작하려면 아무키나 눌러주세요.\n(1920 x 1080 이상 해상도 권장)");
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
            MainMenu(whilein,cursorx, cursor, background, player);
            while (Btutorial)
            {
<<<<<<< Updated upstream
                if (LoadGame(tutorial, tutoplayer, 0)) break;
=======
                if (LoadGame(tutorial, tutoplayer, 1)) break;
>>>>>>> Stashed changes
            }
            while (Btutorial)
            {
                tutoplayer.Move(tutorial);
                tutoplayer.CalPos(tutorial);
            }

            tutorial = null;
            tutoplayer = null;

            while (true)
            {
                if (LoadGame(background, player)) break;
            }
            while (true)
            {
                try
                {
                    player.Move(background);
                    player.CalPos(background);

                    if (player.positionY < 25)
                    {
                        if (princess.check(player.positionX, player.positionY)) // 엔딩조건체크
                        {
                            background.Ending(player);
                            break;
                        }
                    }
                }
                catch
                {
                    //물리 재설정
                    player.ErrorPosSet();
                    background.ErrorPosSet(player.positionX, player.positionY);

                    //배경 재설정
                    Console.Clear();
                    background.Print_Back(0);
                    background.DrawChar(player.positionX, player.positionY, player.direction_right);
                }

            }
        } // Program End
        public static bool get_Btutorial()
        {
            return Btutorial;
        }
<<<<<<< Updated upstream
        private static bool LoadGame(Background background, Player player, int map = 1)
=======
        private static bool LoadGame(Background background, Player player, int map = 0)
>>>>>>> Stashed changes
        {
            try
            {
                Console.Clear();
                background.Print_Back(map);
                background.DrawChar(player.positionX, player.positionY, player.direction_right);
                return true;
            }
            catch
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("콘솔창을 최대로 키우고, 시작하려면 아무키나 눌러주세요.\n(1920 x 1080 이상 해상도 권장)");
                ConsoleKeyInfo key = Console.ReadKey(true);
                return false;
            }
        }

        private static void MainMenu(bool whilein,int cursorx ,int cursor,Background background ,Player player)
        {
            string[] save = new string[10];
            while (whilein)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
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
                        CurrentDirectory = CurrentDirectory.Substring(0, CurrentDirectory.IndexOf("JumpKing"));
                        string path = CurrentDirectory + "JumpKing\\SaveData.txt";
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
                        player.saveJump = int.Parse(save[7].Split(' ')[1]);
                    }
                    catch (Exception e)
                    {
                        Console.SetCursorPosition(162, cursor);
                        Console.WriteLine("세이브 데이터가 없거나 관리자모드가 아닙니다.");
                        Console.SetCursorPosition(162, cursor + 1);
                        Console.WriteLine("Exception: " + e.Message);
                        whilein = true;
                        MainMenu(whilein, cursorx, cursor, background, player);
                    }
                    break;
                case 29:
                    Console.SetCursorPosition(0, 0);
                    Environment.Exit(0);
                    break;
            }
        }

        public static void TutorialClear()
        {
            Btutorial = false;
            Player.ItemClear();
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