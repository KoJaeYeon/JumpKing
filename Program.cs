namespace Project_jUMPKING
{
    internal class Program
    {             
        static void Main(string[] args)
        {
            Background background = new Background();
            Player player = new Player();
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
            while(true)
            {
                Console.Clear();
                Console.SetCursorPosition(70, 20);
                Console.WriteLine("┌────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.SetCursorPosition(70, 21);
                Console.WriteLine("│                                       Jump King                                        │");
                Console.SetCursorPosition(70, 22);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 23);
                Console.WriteLine("│                                      1. 튜토리얼                                        │");
                Console.SetCursorPosition(70, 24);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 25);
                Console.WriteLine("│                                      2. 게임시작                                        │");
                Console.SetCursorPosition(70, 26);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 27);
                Console.WriteLine("│                                      3. 불러오기                                        │");
                Console.SetCursorPosition(70, 28);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 29);
                Console.WriteLine("│                                      4. 게임종료                                        │");
                Console.SetCursorPosition(70, 30);
                Console.WriteLine("│                                                                                        │");
                Console.SetCursorPosition(70, 31);
                Console.WriteLine("└────────────────────────────────────────────────────────────────────────────────────────┘");

                key = Console.ReadKey(true);
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
                int powerData = player.power;
                if (powerData > 2)
                {
                    player.CalPos(background);
                }

            }
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
//튜토리얼