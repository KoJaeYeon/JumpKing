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
                    Console.Clear();
                    background.Print_Back();
                    //background.Print_Char(player.positionX, player.positionY, player.direction_right);
                    break;
                }
                catch (Exception e)
                {
                    Console.Clear();
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
//경사 
//아이템 : 세이브 로드, 중력 잠시 무시, 점프거리 늘려주는 옵션 선택 아이템 123(마찰무시도, 높이도);
//구간세이브 ######
//중력제어 무중력느낌
//바람
//아래키 점프 제자리 ######
//튜토리얼
//유사
//마찰