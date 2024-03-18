using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Project_jUMPKING
{
    class Background
    {
        public static readonly int width = 162;
        public static readonly int height = 241;

        private int prePosX = 45;
        private int prePosY = 99;
        private int jumpPosY = 10;
        private char[,] _background = new char[width, height];
        private char[] save_Char = new char[4];

        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();

        public Dictionary<int, Item> item_Dic = new Dictionary<int, Item>();
        private int item_Dic_Count;

        private int save_posX = 0,save_posY = 0;

        public Background()
        {
            for (int i = 0; i < _background.GetLength(0); i++)
            {
                for (int j = 0; j < _background.GetLength(1); j++)
                {
                    _background[i, j] = ' ';
                }
            }

            //Make_backGround();
            Save_Background();
            Save_ground();
            Save_Wall();
            Save_Plattform();
            Save_Item();

            Draw_Background();
        }
        public void Save_ground()
        {
            for (int i = 0; i < _background.GetLength(0); i++)
            {
                _background[i, 0] = '▣';
            }
            for (int i = 0; i < _background.GetLength(0); i++)
            {
                _background[i, _background.GetLength(1)-1] = '▣';
            }
        }
        public void Save_Wall()
        {
            for (int i = 0; i < _background.GetLength(1); i++)
            {
                _background[0, i] = '▣';
            }

            for (int i = 0; i < _background.GetLength(1); i++)
            {
                _background[_background.GetLength(0) - 1, i] = '▣';
            }
        }
        public void Save_Background()
        {
            for (int i = 0; i < 160; i++)
            {
                for (int j = 0; j < 160; j++)
                {
                    _background[i+1, height - 240 + j] = Lucy[j][i];
                }
            }
            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    _background[i + 1, height - 80 + j] = Vinette[j][i];
                }
            }
            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    _background[i + 81, height - 80 + j] = Sumire[j][i];
                }
            }
        }
        public void Save_Plattform()
        {
            char y = 'y';
            char z = 'z';
            char s = 's';
            //Row 2 ~ 160
            // height ~62
            Platform(2, 18, 20);
            Platform(75, 20, 10);
            Platform(122, 18, 20);

            Platform(2, 35, 30);
            Platform(160, 35, -30);

            Platform(74, 50, 8);
            Platform(140, 55, 3);

            Platform(134, 75, 3);

            Platform(96, 82, 5,z);

            Platform(2, 81, 17, z , -6);            
            Platform(36, 80, -5, s);

            Platform(50, 113, 3);

            Platform(90, 112, 3,s);
            Platform(92, 113, 3);
            Platform(98, 112,-3, s);

            Platform(130, 100, 5,y);

            Platform(140, 120, 3);
            Platform(146, 119, -3,s);
            Platform(146, 109, -3, s);
            Platform(146, 99, -3, s);

            Platform(140, 134, 3);
            Platform(140, 160, 3);
            Platform(124, 168, 3);

            Platform(120, 136, 3);

            Platform(100, 138, 3);

            Platform(70, 146, 2);
            //Column

            // height ~62
        }
        private void Platform(int startX, int startY, int length, char dir = 'x', int h = 0)
        {
            if (dir == 'x')
            {
                if (length > 0)
                    for (int i = 0; i < length; i++) _background[startX + 2 * i, height - startY] = '▣';
                else
                    for (int i = 0; i < length * -1; i++) _background[startX - 2 * i, height - startY] = '▣';
            }
            else if (dir == 'y')
            {
                if (length > 0)
                    for (int i = 0; i < length; i++) _background[startX, height - startY - i] = '▣';
                else
                    for (int i = 0; i < length * -1; i++) _background[startX - 2, height - startY + i] = '▣';
            }
            else if (dir == 'z')
            {
                for (int i = 0; i < length; i++) _background[startX + 2 * i, height - startY] = '▣';
                for (int i = 0; i < length + h; i++) _background[startX, height - startY - i] = '▣';
                for (int i = 0; i < length + h; i++) _background[startX + (length-1) * 2, height - startY - i] = '▣';
                for (int i = 0; i < length; i++) _background[startX + 2 * i, height - startY - length - h] = '▣';
            }
            else if (dir == 's')
            {
                if (length > 0)
                    for (int i = 0; i < length; i++) _background[startX - 2 * i, height - startY + i] = '↙';
                else
                    for (int i = 0; i < length * -1; i++) _background[startX + 2 * i, height - startY + i] = '↘';
            }
        }

        public void Save_Item()
        {
            int x, y;
            // 0번 아이템(세이브 로드)
            x = 20; y = height - 115;
            item_Dic.Add(0, new SaveLoad(x, y, _background[x, y]));
            _background[x, y] = item_Dic[0].get_Char;

            // 1번 아이템
            x = 144; y = height - 105;
            item_Dic.Add(1, new Higher(x,y, _background[x,y]));
            _background[x, y] = item_Dic[1].get_Char;

            // 2번 아이템
            x = 122; y = height - 20;
            item_Dic.Add(2, new Further(x, y, _background[x, y]));
            _background[x, y] = item_Dic[2].get_Char;

            // 3번 아이템
            x = 132; y = height - 30;
            item_Dic.Add(3, new Longer(x, y, _background[x, y]));
            _background[x, y] = item_Dic[3].get_Char;

            item_Dic_Count = item_Dic.Count;
        }

        public void Save_Position(int preSaveX, int preSaveY, int saveX, int saveY) //저장하는 부분 모델링
        {
            save_posX = saveX;
            save_posY = saveY;
            if(preSaveX != 0)
            {
                //이전 세이브 위치 배경 복원
                _background[preSaveX - 1, preSaveY] = save_Char[0];
                _background[preSaveX, preSaveY] = save_Char[1];
                _background[preSaveX-1, preSaveY+1] = save_Char[2];
                _background[preSaveX, preSaveY + 1] = save_Char[3];
                
                //이전 세이브 위치 배경 출력
                Console.SetCursorPosition(preSaveX, preSaveY);
                Console.Write("{0}{1}", _background[preSaveX - 1, preSaveY], _background[preSaveX, preSaveY]);
                Console.SetCursorPosition(preSaveX, preSaveY + 1);
                Console.Write("{0}{1}", _background[preSaveX - 1, preSaveY + 1], _background[preSaveX, preSaveY + 1]);
            }

            //현재 세이브 위치 배경 저장
            save_Char[0] = _background[save_posX - 1, save_posY];
            save_Char[1] = _background[save_posX, save_posY];
            save_Char[2] = _background[save_posX - 1, save_posY + 1];
            save_Char[3] = _background[save_posX, save_posY + 1];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(save_posX, save_posY);
            Console.Write("##");
            Console.SetCursorPosition(save_posX, save_posY + 1);
            Console.Write("##");
            Console.ResetColor();
        }

        public void DrawChar(int positionX, int positionY, int direction_right)
        {
            sb2.Clear();
            Console.SetCursorPosition(prePosX - 2, prePosY - 2);
            for (int i = 0; i < 6; i++)
            {
                sb2.Append(_background[prePosX - 3 + i, prePosY - 2]);
               
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(prePosX - 2, prePosY - 1);
            for (int i = 0; i < 6; i++)
            {
                sb2.Append(_background[prePosX - 3 + i, prePosY - 1]);
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(prePosX - 2, prePosY);
            for (int i = 0; i < 6; i++)
            {
                sb2.Append(_background[prePosX - 3 + i, prePosY]);
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(prePosX - 2, prePosY + 1);
            for (int i = 0; i < 6; i++)
            {
                sb2.Append(_background[prePosX - 3 + i, prePosY + 1]);
            }
            Console.Write(sb2);

            if(save_posX != 0)
            {
                if(save_posX >= prePosX - 3 && save_posX <= prePosX + 3)
                {
                    if (save_posY >= prePosY - 2 && save_posY <= prePosY + 1)
                    {
                        Console.SetCursorPosition(save_posX, save_posY);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("##");
                        Console.SetCursorPosition(save_posX, save_posY + 1);
                        Console.Write("##");
                        Console.ResetColor();
                    }
                }
            }

            prePosX = positionX;
            prePosY = positionY;

            { // 머리
                Console.SetCursorPosition(positionX, positionY - 2);
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (direction_right == 1) Console.Write("→");
                else if (direction_right == -1) Console.Write("←");
                else Console.Write("↑");
            }


            { //몸통
                Console.SetCursorPosition(positionX - 2, positionY - 1);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write('▤');
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write('▤');
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write('▤');
            }


            { //다리
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(positionX - 2, positionY);
                Console.Write('▥');
                Console.SetCursorPosition(positionX + 2, positionY);
                Console.Write('▥');
            }

            { //발
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(positionX - 2, positionY + 1);
                Console.Write('▥');
                Console.SetCursorPosition(positionX + 2, positionY + 1);
                Console.Write('▥');
            }
            Console.ResetColor();

        }

            public void DrawChar_charging(int positionX, int positionY, int direction_right)
        {
            sb2.Clear();
            Console.SetCursorPosition(prePosX - 2, prePosY - 2);
            for (int i = 0; i < 6; i++)
            {
                sb2.Append(_background[prePosX - 3 + i, prePosY - 2]);

            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(prePosX - 2, prePosY - 1);
            for (int i = 0; i < 6; i++)
            {
                sb2.Append(_background[prePosX - 3 + i, prePosY - 1]);
            }
            Console.Write(sb2);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(positionX, positionY - 1);
            if (direction_right == 1) Console.Write('↗');
            else if (direction_right == -1) Console.Write('↖');
            else Console.Write('↑');
            Console.SetCursorPosition(positionX - 2, positionY);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write('▤');
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write('▤');
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write('▤');
            
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(positionX - 2, positionY + 1);
                Console.Write('▥');
                Console.SetCursorPosition(positionX + 2, positionY + 1);
                Console.Write('▥');
            }
            Console.ResetColor();
        }
        public void Draw_Background()
        {
            sb.Clear();
            for (int i = 0; i < height - 1; i++)
            {
                sb.Append('▣');
                if (i > height - 81 )
                {
                    for (int j = 0; j < 80; j++)
                    {
                        sb.Append(Vinette[i - (height - 80)][j]);
                    }
                    for (int j = 0; j < 80; j++)
                    {
                        sb.Append(Sumire[i - (height - 80)][j]);
                    }
                }
                else if (i > height - 241)
                {
                    for (int j = 0; j < 160; j++)
                    {
                        sb.Append(Lucy[i - (height - 240)][j]);
                    }
                }
                else
                {
                    for (int j = 0; j < 160; j++)
                    {
                        sb.Append(' ');
                    }
                }
                sb.Append('▣');
                sb.AppendLine();
            }
            for (int j = 0; j < 82; j++)
            {
                sb.Append('▣');
            }
        }
        public void Print_Back()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.Write(sb);            
            for (int i = 1; i < _background.GetLength(0)-1; i++)
            {
                for (int j = 1; j < _background.GetLength(1)-1; j++)
                {
                    if (j > height - 62)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (j > height - 62 * 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (j > height - 62 * 3)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (_background[i, j] == '▣')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('▣');
                        continue;
                    }

                    if (_background[i, j] == '↙')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('↙');
                        continue;
                    }

                    if (_background[i, j] == '↘')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('↘');
                        continue;
                    }
                }
            }
            Console.ResetColor();
            UIBar(1, 0);
        }

        public void Draw_Item(int max_height, int min_height, int time = 0)
        {
            for (int i = 0; i < item_Dic_Count; i++)
            {
                if (item_Dic[i] != null && !item_Dic[i].getItem)
                {
                    if (item_Dic[i].get_posY <= max_height && item_Dic[i].get_posY >= min_height)
                    item_Dic[i].PrintItem(time);
                }
            }
        }

        public void Get_Item(int itemNum)
        {
            Item item = item_Dic[itemNum];
            int x = item.get_posX;
            int y = item.get_posY;
            char tempChar = item.get_tempChar;
            item.getItem = true;
            _background[x,y] = tempChar;

            sb2.Clear(); // 아이템 위치 배경 초기화
            Console.SetCursorPosition(x-2,y-2);
            for(int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y - 2]);
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(x - 2, y-1);
            for (int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y-1]);
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(x - 2, y);
            for (int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y]);
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(x - 2, y+1);
            for (int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y+1]);
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(x - 2, y+2);
            for (int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y+2]);
            }
            Console.Write(sb2);

            Player.Get_item(itemNum);
            item_Dic[itemNum].Set_pos();
            item_Dic[itemNum].OffItem();
        }

        public void UIBar(int positionY, int power)
        {
            int height = Background.height - 1;
            ConsoleColor color = ConsoleColor.DarkGray;
            jumpPosY = positionY;
            while (positionY < height)
            {

                height -= 62;
                if (power == 0)
                {
                    Console.BackgroundColor = color;
                    if (height < 0)
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            Console.SetCursorPosition(198, 41 - i * 2);
                            Console.Write("         ");
                            Console.SetCursorPosition(198, 40 - i * 2);
                            Console.Write("         ");
                        }
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(185, 45);
                        Console.Write("┌──────────┬──────────┬──────────┐ ");
                        Console.SetCursorPosition(185, 46);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, 47);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, 48);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, 49);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, 50);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, 51);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, 52);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, 53);
                        Console.Write("└──────────┴──────────┴──────────┘ ");
                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            Console.SetCursorPosition(198, height + 41 - i * 2);
                            Console.Write("         ");
                            Console.SetCursorPosition(198, height + 40 - i * 2);
                            Console.Write("         ");
                        }
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(185, height + 45);
                        Console.Write("┌──────────┬──────────┬──────────┐ ");
                        Console.SetCursorPosition(185, height + 46);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, height + 47);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, height + 48);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, height + 49);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, height + 50);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, height + 51);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, height + 52);
                        Console.Write("│          │          │          │ ");
                        Console.SetCursorPosition(185, height + 53);
                        Console.Write("└──────────┴──────────┴──────────┘ ");

                    }
                }
                continue;
            }
            if(power == 1 || power == 52)
            {
                Console.BackgroundColor = color;
                for(int i = 0; i < 11; i++)
                {
                    if(height < 0)
                    {
                        Console.SetCursorPosition(198, 41 - i * 2);
                        Console.Write("         ");
                        Console.SetCursorPosition(198, 40 - i * 2);
                        Console.Write("         ");
                    }
                    else
                    {
                        Console.SetCursorPosition(198, height + 41 - i * 2);
                        Console.Write("         ");
                        Console.SetCursorPosition(198, height + 40 - i * 2);
                        Console.Write("         ");
                    }

                }
            }
            else if (power % 5 == 0)
            {
                switch (power / 5)
                {
                    case 1:
                        color = ConsoleColor.DarkBlue;
                        break;
                    case 2:
                        color = ConsoleColor.Blue;
                        break;
                    case 3:
                        color = ConsoleColor.Cyan;
                        break;
                    case 4:
                        color = ConsoleColor.Green;
                        break;
                    case 5:
                        color = ConsoleColor.DarkGreen;
                        break;
                    case 6:
                        color = ConsoleColor.DarkYellow;
                        break;
                    case 7:
                        color = ConsoleColor.Red;
                        break;
                    case 8:
                        color = ConsoleColor.Magenta;
                        break;
                    case 9:
                        color = ConsoleColor.DarkMagenta;
                        break;
                    case 10:
                        color = ConsoleColor.DarkRed;
                        break;
                }
                Console.BackgroundColor = color;
                if(height < 0 && power!= 0)
                {
                    Console.SetCursorPosition(200, 42 - (power / 5) * 2);
                    Console.Write("     ");
                    Console.SetCursorPosition(200, 41 - (power / 5) * 2);
                    Console.Write("     ");
                }
                else
                {
                    Console.SetCursorPosition(200, height + 42 - (power / 5) * 2);
                    Console.Write("     ");
                    Console.SetCursorPosition(200, height + 41 - (power / 5) * 2);
                    Console.Write("     ");
                }


            }
            Console.ResetColor();
        }

        public int Collide(int positionX, int positionY, int direction_right)
        {
            int num = 0;
            if (direction_right > 2) // Down , Up
            {
                positionY += direction_right == 4 ? 2 : -3;

                for (int i = 0; i < 7; i++)
                {
                    num = check(positionX + (i - 3), positionY);
                    if(num > 0)
                        return num;
                }
            }
            else // Left or Right
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        num = check(positionX - i +3 * direction_right, positionY - 2 + j);
                        if (num > 0)
                            return num;
                    }
                }
            }
            return 0;
        }

        private int check(int x, int y)
        {
            try
            {
                if (_background[x, y] == '▣')
                {
                    return 1;
                }
                else if (_background[x, y] == '↙')
                {
                    return 2;
                }
                else if (_background[x, y] == '↘')
                {
                    return 3;
                }
                else if (_background[x, y] == '0')
                {
                    Get_Item(0);
                }
                else if (_background[x, y] == '1')
                {
                    Get_Item(1);
                }
                else if (_background[x, y] == '2')
                {
                    Get_Item(2);
                }
                else if (_background[x, y] == '3')
                {
                    Get_Item(3);
                }
                return 0;
            }
            catch (Exception e)
            {
                return 1;
            }

        }



        public readonly char[][] Lucy =
        {
          "@@@@@@@@@@@@@@@@@@@@@@*~.          .  -=*:::  ............ ,-, .........      .--.       ..          .;$;.,~,             -:.   .  -!!-!@@@@@@@@@@@@*~~#@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@@@@#;,          ..  -$;::- ............ ,~~..........,.      .~,        ..          ,~!!,-,.            .-:.   . .-=-:=@@@@@@@@@@@!-:@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@@@@$-.          .. .:=:;~. .............~~-.......   ...      ,-.        ..          .~!;~,.             .~~.  .. .!;~;@@@@@@@@@@@;~!@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@@@#*.           .  ,;=;:, .............-;, .....      ..       --         ..           -=!~.               ~-. ... ,*;-$@@@@@@@@@#;:$@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@@@=~ .         ..  ,;#!, ..............;-  .                   ,-,         ..           .==:               .~~  ..  !!~-#@@@@@@@@=:;#@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@@@*..          ..  ~=$,...............-:.                       ,-          ..           ,*=~               .--  .. ,;!.$@@@@@@@@!:!@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@@=~ .         ... ,!*~.............. ,~~                        .,,       .  .            -!*,               .-,...  ~*~:$@@@@@@@;:*@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@@;, .         .. .;!: ............   ,~-                         .-       ..  .            -*!,               ,-,... ,;*-*@@@@@@#;!$@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@#~ .          ...~=:  ............   -~.                          -.      .-.  .            ,$:.               ,-, ,  -$-;$@@@@#=!=#@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@!, .         .  -$~. ............    --                           ,-.      .,.               -*;                .,... .:*:;@@@@#==$#@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@@@-...           ~!;..............    .--                           .~,       ,,.     .        .;*;                .,....,*;;$@@@$==$#@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@#=...           -!!,........ ....     ,-,                            --.       ,,     ..        ,;=~                .,,.  ;!;!@@#$==$@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@$;... .        .**- .......  ...      --.                            .-,       .,,     ..        -*$-           .    ,-,  .;!:#@#$==#@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@=.  . .       .*=: ,....,.  .,.       --.                             ,-        .-      ,,        -#!,          ..    --,  :!;$@#===#@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@@* .  .       .-$:......,.   ,.        -,                              ,-        .,,.    .--        -=:           ,.    ,~, ,:**##==$#@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@$; .. .       -!;, ......   .,         -,                              ,-.        .,.     ,~-.       !!~          ...   .,~,.-*$##$=$@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@*- .. .      .;=,.......   ...        .-,                              .-,         ,.      ,~-.      -**.          ..     -~,,;@#$$=#@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@@;    ..      ;=; ....,.    ..         ,-.                              .--         .,.      -:-       :=:          .,,     -~,,#@#$=@@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@$: . ..      ,!!.....,.    ..          -,.                               ,-          ,,       -~-  ... .:=.          .,-.    --,;##$$@@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@*- ....      ;!- .....     ..          ~,                                .-          .,.      .-:.  ,-. -=;,          .,-.   .,,~$@##@@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@:. ....     -=:.......    ..          .~.                                .-.         .,,       .~~. .~,.,:=-          ..--,    .,!#@#@@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@@~  ....    ,**- ,....     .           .~.                                .~.          .-        .;-. --,..=:.         ...-~.     -*@@@@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@@#- ....     -$-......     ..           ,~.                     .          .~,          .-,        ,;~ ,,-,.;*:          . .-!,     -=@@@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@#*, ....    -!*......     ..           .,-.                     .          .~,        .  ,-.        ::-.,,,,,*;.          ...-;~.   .~$@@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@#:......    :*~....,.     ..           .--                      ..         .~,        .  .:,        ,:;.,,,, ;!~      ... ....~:~.   .:$@@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@$- .,..    ,;! .....     ..            .~.                      ..         .~-        .. .:-.    ..  -!~..,,.~!*      ...  ..  ~;~.    ;$@@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@$. .,..    ;*: .....     ..            ,~                       ,.         .:-.        .. ::,     .   -=,.,,,.:=-     .......,  -*:.    ;#@@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@@= .,,..    *;. ....     ..             ,-                       ,,.        ,;~.        .. ,;-     .,  .;;-.--.-*=,    ......... .,;;-   .:$@@@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@$! ,,,.    ~*: .....     ..            .--                       ,,.        ,!:.        ,,..;~      ,,  ,!:,.-..:#~.   ............-:;~.  .~=#@@@@@@@".ToCharArray(),
          "@@@@@@@@@@@=~ ,-,.   .*;- ,....    ..             ,-,                       ,,.        ,;:.        .,, ::,     .,,  :;~.,..-#!-  ............. ,:;:-.  ~*#@@@@@@".ToCharArray(),
          "@@@@@@@@@@@*, --..   ,$~  ....     ,.             --                        ,,.        .:~. .,,..  .,, ~:~      ,-,..:;.,,.,==:. .............. .~=*~.  ,!#@@@@@".ToCharArray(),
          "@@@@@@@@@@@!  --.   ,;=-......    .-             .-,           .        .   -,. ..     .-~,,,,,,,,  .-.,:: ..    .--.-:;....;!;-. ........ ,-, .. .*$!.  .:@@@@@".ToCharArray(),
          "@@@@@@@@@#$: .-,.   -=;.......   .,,         .   --,          ..        ..  ,-,.,,.    .~~,,,,,,,-,..,,,~:,,,,,...,,,,-;-. .-;:~, ........ ,-~ ... ,*$*:, .-*#@@".ToCharArray(),
          "@@@@@@@@#!!~ -~,    ~=-....,..  .,-.        .   .~-. .       .,.    ... .. .,-,,,,,. . .~:-,,,,,,,,..,,.-:-,,,,,,.,,,.,~;,  ,::~~...........,~..... ~=@$!:, -;=@".ToCharArray(),
          "@@@@@@@$!~;~ ~-,   ,;*.,,..,.  .,--        .,   ,:,         .,-. ..,,,,.,. .,--,,,,,....~:-,,,,,,,,,,,,.-:~.,,,,,,,-,,..;~. .;:~:,...........--..., ,;##@#!-,,~=".ToCharArray(),
          "@@@@@@@! ;!~.:-.   :*; ,,..,. .,--,       .,.  ,:-. .      .,--,,---,,,,,. ,,--,,,,,,,..~:-,,,,,,,,,,,-.-:;..,,,,,,~~,..,;~  ::::-...........,~.... .~*;$@@@$:. ".ToCharArray(),
          "@@@@@@!. *;,.:,.   ;*-.,...,,,,,~-.       ,- .,-:. .,    .,,,,-,,,,,,,,,,. ,,--,,,,,,,.,~:-,,,,,,,,,,,---~*,.,,,,,,,~~,..-:~ ~:::-............~,.... -*:=@@@@#$=".ToCharArray(),
          "@@@@@!- ,=~..:,.  .!!.,,..,,,,,,~-       ,,,..,~~ .,.  ..,,,,--,,,,,,,,,,..,,--,,,,,,,,,~*~,,,,,,,,,,,--,~*-,,,,,,,.,~~, .~;,-::~~............--, .. ,;;!=@@@@@@".ToCharArray(),
          "@@@@$- .~=-.-~.   -*; ,,..,,,,,-~, ..   .,,.,.-~, ,,. .,-,,,,~~,,,,,,,,,,,.,-~-.,,,,,,..~$:,.,,,,,,,,,,-,-*~,.,,,,,,.-~~. ,::-~:~~,............-,... .~*:*@@@@@@".ToCharArray(),
          "@@@#:. .;=-.~~.   ;*: -,,,,,,,.~~, ,   ,-,,,,,~~  -, .,-,,,.-:-,,,,,,,,,,,,.-~-.,,,,,,..~$:,.,,,,,,,,,,-,-!:,.,,,,,,,.-:,  .!;~~:~,........... --. .. ,$:;#@@@@@".ToCharArray(),
          "@@#!.. ,*!,.~~.   *!- ,,,,,,,,,:-. , .,-,,,,.::, ~,. -,,,,,,~;,,,,,,,,,,,,,.-~-.,,,,,,.,:$:,.,,,,,,,,,,-,,;;-,,,,,,,,..,;,  .**:-:,........... ,-- ....*;:!@@@@@".ToCharArray(),
          "@#*~,. -=~.,:-.  .=;,.,,,,,,,,-:,..,.,-,,,,,-~-.--..,-,,,,.-:;,,,,,,,,,,,,,.-~-.,,,,,,.,:$:..,,,,,...,.--,:;~,.,,,,,... ~~,  -;*:~,.............--.... ~!:~@@@@@".ToCharArray(),
          "@*:-,  ~$, ,:,.  ,=~.,,,,,,,,,~:..,,,,,,,,,,~,,,~,.,,,,,,,.~;:,,,,,,,,,,,,,.~-,,,,,,,,.,;#:..,,,,......-,,:;~,.,,,,......~~.  -;!:-............ ,-, .. .!;-=@@@@".ToCharArray(),
          "=:--. .:=. -:,. .~=~.,,,,,,,.-:-..-,,,,,,,,-~.,~-.,,,,,,,.,:;:.,,,,,,,,,,,,,~-,,,,,,,,.,;#;..,,,...... -,,~;:,..,....... -:~   ,**~............ .-~...  !;,~$@@@".ToCharArray(),
          "--, . -!*..-:.. .;*-.-,,,,,,,-;..,,,,,,,,,-~ ,~~.,,,,,,,,.~;;~.,,,,,,,,,,,,,~-..,,,,,.,~!#;, ,,....... --,~:;:, ......... ~*,   .*!- .............:.... :!: *@@@".ToCharArray(),
          "-,. , ~*;..~: . ,*!-.-,,,,,.,~:.,,,,,,,,,--..:~,.,,,,,,,.,~;:-.,,,,,,,,.,,,,~,...,,,,.,:*=;- ......... --,;:;;~ ......... ,:$:,  ,:;~  ...........~-... ~!; ,*@@".ToCharArray(),
          "..... :*- ,:; . ,=!,.-,,,,,.-:~.,,,,,,,,--,,-:-.,,,,,,,..--::,.,,,,,,,..,,,-~,...,,,,.,;=*;- ......... ~-,!:~:; ...........-$=!~-.,~~-. ..........,~, . ,;!. ~=@".ToCharArray(),
          " ... .;*. -:; . ,=;,.,,,,,,.:;-.,,,,,,,,-,,,~~-.,,,,,,..,-,:~.,,,,,,,..,,..~~,.,..,,,.-;=!;~ ......... ~-,*!-~*.............:==*!~--~;~,. .........~-.   ~!-..:#".ToCharArray(),
          " ... ,!!  -;; . -$:,.,,,,,.,;;,,,,,,,.,---,,~~,.,,,,,...,-,~~ ,,,,,....,,..:~......,,.-;=;;:. .........;~,!=~-!-..,........  ;*==**!:!*!:.. ...... --,   ~*~-.,!".ToCharArray(),
          ".... ~!;  ~!; . -=:,.,,,,,.:;~.,,,,,.,~:~,.,;-,,,,,..,.,-.,~- .........-,.,;-....... .-;=~;;- ........,;~.;*~.~!,.,......... ~=$:-~~~:;*$=*:, .... ,--   ~!~-,..".ToCharArray(),
          ".... ;!~  :!: . -$:,.,,,,,.;:,.,,,,.-~:~,..-;,,,,,.....-- ~~, ........,-..-;,....... ,~;$,~;: .....,..-;-.~-~~,=~..,........ .!#!---..,-~~:~, ......--   -!;,-, ".ToCharArray(),
          ".... !;,  ;*- . -$:,.,,,,.-;-.,,,,,-~:~,. ,~:,.,..... ,-- :-..........--..~!........ -~;$.,:! .....,..~:-.:-;;.;;- .,........ ~=$~-,     .,,....... --.  .:=,-- ".ToCharArray(),
          ".....*:. .**, . -$:..-,,..::,.-,,,-~~-. ..-:-,....... ~-,,;-.,........--.-;!..,......~~;=. ~*......,..::,,*!*;,.;;, ........, ,;#~-,.....   ......  ,--   -$,,-,".ToCharArray(),
          "....-*~  -=!  . -$:..-,. ,!-,...,~~..,,. ,~:..........:-.;:,.,.......,~, ~*;..,.....-~~:*. -!,..,..,..;:,-#*~-;,-;*   .,...... ~$;~,..............  .--   ,$-.~*".ToCharArray(),
          "....:!-  !$!  . -$;,.,,..~;,..,---..-,.. -~~ .....,..;;-,!,..........-~. :=~..,..,..:-~::  ,;:..,.....=:,~*-,,-~---::,  .,.....,**~...............  .,~.  ,=:,~#".ToCharArray(),
          ".. .!!- -*=;. . -$*, .. ,~~..,,-,..,-,...::, .......,=~-~;..,...... ,~~ .!$, ......,!,~;~  .-;..,....,=~,--....-::--::-......,..:=~...............   ,~,  .!!-,!".ToCharArray(),
          "...,*;,.;*!:. ..,==- ...-:,..,,,. .~,...,!;......, ,:$--:~..,....., -~- ~==. ......-!.~;,   .;,......-;~~~..,,,-:;:.-:;~,.......,=:. .............   .--. .~=~.,".ToCharArray(),
          "..,-=:.,=;!:. ..,!$~ ...-~ .......,~....!=: ,..... ~=*,~;.........,.~:, !$*....., ,::.:;  ..,~,.....,-.~!:.--~~-,~!~.,~;!~.  .,..*;, ..............  .,~.  .*:  ".ToCharArray(),
          ",,,-=-.-=-!;. ...:$~ . ,~- ......,--. .,=:. ,..., ,;=-~:~ .......,,,:~ -**~,..... -:.,~:.~,,~!,.... ,, ;:. ,-,    -!;~,.,~;;. .. ;!~ ..............  ..~,   *;. ".ToCharArray(),
          "-,,-=,,:=,!;  .. ,$:.  -~, ......,-.. ,;=-....,...:*!~;~,.......,-.::-.*;~--......-: ,;;,,,:=*,.., .,,-*;,,;~,   ,~~~,. .,-~;:,  ~!~ ..............    ~-.  *!- ".ToCharArray(),
          "-..~=,~!;.!!  .. .=!-  ~~...... ,-- ..~=;,.,.... ~;;:!;- .......,-,!:,~=-,~-.... -:~ ~!;,-;***,....~:~:!;:!*!:~~:!;,,...-,.,:!;- .;:...............    --.  ;*; ".ToCharArray(),
          ". .:*,:*- !!  ... *=: ,:- ..... --,  ,;=,....,..-=~-;$:. ,.... ,--~*~-=:.,:-... .;!- !!~,!*=!-,.. -**;;;;*@*=##=#@=~---~:-.  ~*=- :;, .............    .-,  -==,".ToCharArray(),
          ". ,:!.;!  !!  ... =#; -:- ..... --.. :=*..,.,. -*-.-==,....... --~$:,~!. ,:-... ~;~, ;--$$*-.-~. .;~..,;=$@#$!--=@##**$$:,    :$$-~:~ ........... .     -,  ,!$!".ToCharArray(),
          ". ,;:,;! .*!  ...,!$!,~:, .... -~,  ,!*- .,.. ~*=--;$:.........-~;$-~:,  ,~-. .-~-,,,~;*=:,,~;- .,-~;*=#####*;;=#@@$;=@#$=!:-.~!==!;~ ........... .     --. ,;!;".ToCharArray(),
          ". -;-~!; ,*!. ...::;!;:~. .....!~. .;*; .... -*$*;!==,....... ,-;*;-~~ . ,~~..-~,..,-;!;:.-~:~,,-:;*$@@@@@@#======$=*$@####$*,-;$$!;: ...........       ,-, .:!-".ToCharArray(),
          ". -!,;;~ .*!. ..,!-,:$!-. .. .~=- .~$*- ,...-!!;;!$$; .......,~:*!.~-. . .~~.,~-  ,-:!:, .::-.-:!=@@@@@##$$=$$$;~:;!*==$###@#-,;@=:;!.....,......       .-,  ;!.".ToCharArray(),
          ". -!,;;, .*!. ..,*-.-=$- ... -=:. -##: .,..-=:.   :;, ..... ,:!==;.:, .. .--,~~   -;;.  .:-  -*=$@@@@#$$!-.::;*!-,,,,,~*@$$@=,.;@$~-;,....,......       .,-  :*-".ToCharArray(),
          ". ~!,!:. ,*!,  .-*, .-*, .. ,:=..:=#!, . ,:;-..  ,:~ ,..   ~!=!~-~:,. ..  ,;~,.  ,;!.  ...  .:!*=$#$=!;!:,-*!;;$:,.,,..:#@#=, .:##~,~-. ..,......        ,-. ~!;".ToCharArray(),
          "..~!-!~..,**,  .~!. .-!, .. ~;:,:=#=-. .-::-  .  ~~, . ..,~!*;-.,~~. ...  .:-.  .:;~ .....  ,;!*===!;;;!*!!==!;=*- ..  -#@*-  .:##~,~~. .,,......        ,-, -!*".ToCharArray(),
          " .~!-!~.,.**-  ,:;. ,~!, ...;!-~;$#;..,~:~,  . .,:-. .,,--:;~.  -:,  .... .-.   -:~..,.. . .-!!***!::!*!;;;:;;:!=: ..  ,=*~  .,:#$~,::. .,,......        .-~ ,;*".ToCharArray(),
          " .~!~!~.,.!*~  -;~..,~!,.. -;!;;~!=..-:!~.  .  ,:-..-:;;:-.    .~:   ......    .:, .,,. .. ,:**;:!*;!=;-.   ,--,;:. ...-!-  ..,:$$~,:;. ..-......        .,:.,:=".ToCharArray(),
          " ,~!~!~.,.!=~  ~;,..,~!,.. ~*=;.;!~ ;*;. .   .-~~.-!=$$$=*;~,  ,~~ .......... .~~..,,....  -!!;::=*!;~.  .~.   .--, ..,:~. .,,.-*$-,:;. .,-......         .:..~$".ToCharArray(),
          " ,:!:!~.,.!=: .:; ,..-!, . *=;,,!;-!!:,   .,~;!;,.,-~-----,,.  ,-- ......... .,~..,,,...  .-:~:~~$*:-, .:=$, .. .,, ..,-,...-..:$$-,:;.  ,-......         .:,.-$".ToCharArray(),
          " ,~!:!-.,.;=; ,;; ,..-!, .~=!-.;!;;;~---.-:!;*=*:::::~~--,,,.. ,-, ......... .-~ .,,,..   .,-.-,-$!-., .!#=,.,, .,,..... ..,,.-!#$-,:;.  ,-......         .~-.-$".ToCharArray(),
          " .:!;;-.- ;=; -;: -..-!, ,**-,~=;*=. ,;*:*$$!=$$#$$$$$==*!:~--.... ......... ..-. .,,.    ..  ..,=:..,. ~!~..-- .,,....  .,, ,-;$$-,:;.  --......          -~,-=".ToCharArray(),
          " ,:!;;-., !$; ::, -..-!, -$,,;$:-!$.  ~$=##$##$#@@@@@@@@@#$*;:~.  ...........  .~. .-.       ...,;-..,-.    .. .,-,  ,-. ...,-,~#=-,:;. .--......          ,:,,*".ToCharArray(),
          ".,~!!;-..-*$; -~~,-. -!,.:*.:;-.-*$- ~*!;*##=:!$######@#$$=*!;;;~,  ........... ,-, ...       ...,,...    ..    ,,.,--, ....:..:#$~-!;.  --......           ~,,!".ToCharArray(),
          ",.-!*;-.~~:**.~;;-.  ,*,,::.,-  ~=#!;=$=*=$##*=$$$$$$$$==$=*!;;;:~,............ .,-,  ..    .  ...-.           ,::-:~,. ...-- .:#$~-!:. .--. ...            ~,,;".ToCharArray(),
          ",.~*=;--!--;=.:!:,.  .*,-:~     ~*@#$##=$$#$=$$=*====*;!*==*!;:~~~-.. .......... .,-.  ..  .   ...,,. ..,. ,--,:;:-:-. ...,-. .:#$~-!;. .~~. ...            --,~".ToCharArray(),
          "..:$=~-:*.,!=,!;~,   .!-~;,  .. ,!@@@#$$##$*~!!;-!!!!;-:!~**!::,  ............... .,~.  ...    ....-~-~;:~,:;;;!-.   ... ,-,  .:#$~-*!, .;~. ...            --,-".ToCharArray(),
          " .;$*;*!. .;=-!;-,   .!:::.     ;#@####@#!:--!!:!$=;~,:*=*!*;....   ..............  ,~,  .....   ..:!==;~~:-..,.       .,-, . .~$$~~==, .:~. ...            ,--,".ToCharArray(),
          " ~;!=$!-  .:=;;~,,   .;!;-   -;*$@@@###$;~~~-**!=!~,..,-~:~*!. ... ................. ,--    ..   ..,---,..,.       .,,,--,... .~$$~~=$, .:~. ...             --,".ToCharArray(),
          ".;;~$$:... ~==;-,,    ~*;,   -*#@@####=:---,-*=!:-.,:;-..-.:;- ... .................  ,--.        .            ..,,,--,,..... .~$=~~=#- .:~. ...             -~,".ToCharArray(),
          "-;~-@*- ,. -*@:--.    -=!,   .~*#@###=:,~-, .=*-   ~$@:. , ,~~ ... ...........   ..    .~-.                ...,,----,,.  .... .~$=~~*#~ .:~. ...             -~-".ToCharArray(),
          ":~,-@$~ .. ,!@~~~      **- .    !#@#=;--,,. .*:  ,.,:$- .,.  . .. .........      ..      -~,          ...,------,.    ..... . .~==:~!#:..~~. ....            ,-~".ToCharArray(),
          ";-,:@#:  . ,;=~:;   ., !*~ .  . ~*=*=!~,.....:- .,,..,   .  .,..........         ..       .,,,..,,,,,,,,,,,.....  ........  . .~==:~!#;,.~~, ....            .,~".ToCharArray(),
          ";,-;;#;. , ,;:~;;   .- :!;    ..:!;-!*;......-,..,.       .~:~.  .....                     .,,,,,,,......   ...  .........  . .~==;~;$*-.-:, . ..             ,~".ToCharArray(),
          ":~:; =*- . ~*,~!;  .,-.-::    ..!*~ ~*! .... .,. .    .,--~!!~   ...                         .....         ............... .. .:==;:;*=~ ,:, . ..             .-".ToCharArray(),
          ",:;~ *=;  .:= :!:  ,-,-..,..  ..!*:  ;!. .....,,     .-!=*=;- .....                                      ...............  ... .:=*;:;!=~ ,~,.. .              .~".ToCharArray(),
          ",:~  ~**  -!* ;!~ .,-,-,. ..  ..!*:  ~*; .... .,~;;:;*=;-,,.  ...                                       ............... ..... ,:=*;;;;$:  ~,..                ,!".ToCharArray(),
          "::- ..!=- ;*~ !!~ .--,-,........**~  -;=..,-,,,~;!**:~~,.                                              ...................... ,:=!!;;!#;  ~,..                .:".ToCharArray(),
          "*~... ~*!.!!. !!- ,-,,-,........**~  .:$, .-,,-~~-;:,                                                 ....................... ,:*!!;;*#!. ~-...               .-".ToCharArray(),
          "*, ., ,;$~!:  !!- ,-,,-,........**~ . -=~. ...,.  ..   ...                                          ......................... ,;*!!!**=!- --,..                .".ToCharArray(),
          "~. ... -=#!,  !!- ,,,,-,.......,**- . ,!=-      ..  ......                                        ........................... -;*!!*$:;!: --, .                 ".ToCharArray(),
          "...... .:#:   !!~ ,,,,-,.......,*!, ...-=-  .  .............                   .                 ............................ -!=:!*!,;*; ,-, .                 ".ToCharArray(),
          " ..... .~$~   ;!~ ,,,,-,...... ,*!, ,. .*:, .................                 .,.             ............................... -!=:**;,:*! .-- .                 ".ToCharArray(),
          " ..... ,;!,   ;!: ,,,,-,...... ,=!. ,,  :!~ ......................            .:~.          ................................. -!=!=*;,~*! .--...                ".ToCharArray(),
          "...... -$~.   :!: ,,,,-,...... -=!. ,,. .!: ................ ..........     . ,!*,       .................................... -*@#=*;--!*  --...                ".ToCharArray(),
          "..... .:$,    -;: ,,,,-,....,. ,*;. ,,.  ;!, ................................ .~;,   ........................................ ~=$~!=!--!*. ,-,..                ".ToCharArray(),
          "..... ~*!.    ~!; ,-,.,,...,,. -*;  ,-,  ~!: ...................................,............................................ :*:.!$*~,;*- ,-- ..               ".ToCharArray(),
          "..... ;*~     !=; ,-,.,,....,. -=;  .--  ,;! ..................................  ...........................................  :*. ;$=~,;=~ .-- ..               ".ToCharArray(),
          ".... .!!     ,==: ,-,..,....,. ~=; ..--.  ~*,  .............................................................................  ;!  ;$=~,:=: .-~ ..               ".ToCharArray(),
          ".... :*;    .$@$: .-,.......,. -=; . ,~-  -*!. ............................................................................. .;!  :$$~.:*;. ,~....              ".ToCharArray(),
          ".... !!-   .:==$* .,,.......,, -*; . ,--  .:$, .....................................................   ..................... -!;  ~=$~.:*!. ,~,...              ".ToCharArray(),
          "....,*:.   ~*~-*=. ,,.......,, -*: ...-:.  -$~. ..........................................           .-..................... ;*:  ~=#~,~**. .-~...              ".ToCharArray(),
          "....:*~   ,;*  :=- ,-.......,, -*: .. ,!~. ,=;, ....................................     ......... .-::. ..................  **-  -*#:,~**. .,~....             ".ToCharArray(),
          "....=;,   ;=:  ~!;.,-.......,,.~*: .. ,!*, .;=: ....................................    ..,,--~~~~-;==,  .................  .=;   ,!#:,~!=.  .:,...             ".ToCharArray(),
          "...,$~   ,*!.. -!*.,-,.........~*: ....-*-  .=:  ...............................    .,,-------,,-!$#*, ................... .*=~ . .;#;,-!=.  .~,...           ..".ToCharArray(),
          ". .~$,   !!- , ,:=..,,.........:!~ .....!-.  *!, ............................   .,,------,,,,.,,:$@*-  ..................  -#;, . .;#!,-;=,.  ~-,.,.          ..".ToCharArray(),
          ". -!*.  -$:... .~$,.,,.........;!~ ..,. :~.  ;*: ..........................  ..,--~~--,.    ..,~!#*-  ................... ,;#-  .  :#=--;=,.  -~,.,.          ..".ToCharArray(),
          ". ~*~. ,!=- ... -$~..-.........*!- .,,. ~~,  -*! .........................  .-~::~-.     ...,,,:==,  ...................  :$!,  .. ~#$--;=,.  .~-.,.           .".ToCharArray(),
          " .:*.  ~$-  ... ,=;,.-.........*!- .,,. -~,  .;*   ..................      .;!:-.   ........   ~;, .. .................. :=*.,.    -*#-.:=,.   ~-..,.          .".ToCharArray(),
          " -!!  -!*...... ,!*-.-,........=;, .,,. -~, . ;*-  ......................-;*~,..........     ,~~- ..................... .*#=!=*;;;~;=@:~;$,..  -~, ..          .".ToCharArray(),
          " :!:  :*~...... .~=~.,,.......,=:, .,,. -~, , ~*;  ................. .-::*$@-    ....     .,-~~-...................... .;**;;!!****=#@=*=$,..  ,~- ...         .".ToCharArray(),
          " !!, ,!! ....... .$:..-.......,=:. .,,. -~, . -!*. ................  .~;*$$#:,..       ..,-~:~,...................... .~#;, ..,--~:;*@=*=$,... .-~ ...         .".ToCharArray(),
          ",*;  !*: ....... .$!, -,......,=~. .,,. -~, . .:$.  .................   .,~!=*:-..  .,,-~~:-. ......................  ~#=-          ,@~ -$,...  -~. ..         .".ToCharArray(),
          ";*: .*;. ....,...,==: ,,......,=~...,,..-~,  ..-$,  ...................    .,-~~~~~~~~~--,...,,....................  ~*$-........   ,#;,-$,...  ,~- ...        .".ToCharArray(),
          "=!-.:*~ ....,-..,-;=; .,,.....,*-..,,,. -~,  . -$~. ....................     .,,,--,,,,,,,...,....................  -*=~ .........  ,#=--$-...  .-:. .,        .".ToCharArray(),
          "#:.,=;, .. .--..-~~**. .,.....-*-..,,,. -~,  ..,*!, ........................    ....  ...,,,,....................  .!=;  .......... ,$$~-$-...   ,;,  ,.        ".ToCharArray(),
          "#- -$- ....,~. ,~:,!=: .......-=- .,,,. ~-.  ...:#:. .................................,,........................  .;$:  ,...........-*$:,=-...   .;-. ..        ".ToCharArray(),
          "=-,!=,... .-- .-~~,;$*  ......-;,..-,,..~-.  .  ,##!.  ........................................................  .:@:. ,............-~=;-*- ..   .~:,  ,.       ".ToCharArray(),
          ":.~*~.... ,-- .-~~,:*$,.......,~..,-,...:,   .. .*!!*:,  ...................................................... .;=;...............,~.=!~!- ..    .:-  ..       ".ToCharArray(),
          "..:*..... --, ,~:~--;$:,......-~..,-,..,:,    .. *:~!$;-.  ................................................... .;*!................-~ !*;;-...     ~-.  ..      ".ToCharArray(),
          " -!! .....--. ,~~~~,:$!-...,..-~..--,..-:.    .. !!-.*=*~,  ................................................. .:$!, ...............-: :*!:~...     -~-  ..      ".ToCharArray(),
          " !*- ... ,~-  -~~,,.:#*-,..,..~-..--,..:~.    .. ~$!  .!#=,   .............................................. .:@#: ...............,~~ -!*:~. .      ~~  ...     ".ToCharArray(),
          " !;..... -~-  ,--:*=$=$;-, ..,~,.,-,..,;.     .. -=*.  .~!=;,   ........................................... ,;$;!~ .............. ,~~ .:=*:. .      -~,  ..     ".ToCharArray(),
          ":!: .....:-. .~;*==!::#*:-...,~,.,-,.,~:      .. .*!-....-:!*:,.  ........................................ ,;*;,;: .............. ,~~  ~=$:. .      ,--  ...    ".ToCharArray(),
          "=;- ....,:,,-:*=*;~, .$$!~-..-~.,--, -:-      ..  ;;-,...  ~!*!~,.  .................................... .,;=!..!; .............. -~-. -*@;. .       ,:.  ..    ".ToCharArray(),
          "#~  ... ,--;$#*~.    .!#*:~..-~.,-,..~~       ..  ;;-~..,,. .-=$!-   ..................................  ,;#*- ,*: .............. -~-. .:@!, .       ,:,  .,    ".ToCharArray(),
          "$-.,.  .;$$=;,.       ~$*;~-,-~.,-, ~~,       ..  ;;,~....... .:*$!-    ..............................  -*#!-. ,!: .............. -~,.. -@*, .       .-:.  ,.   ".ToCharArray(),
          ";, . .~*$!:-,         -!!;:~~~,.--.,:-        .,  !;.~,.........-;#=!~.    ..........................  ~*=;:,  .!: ...............~~....,==- .        .:,  .,.  ".ToCharArray(),
          ",. .-;**;-.           -;;;;~::.,-,.:~.        ..  !;.~-,.......  -#***;~,.   .......................  ~*=;~~.  .!: ...............~~.....:$- .         ~~.  ,.  ".ToCharArray(),
          "  .~=$;,              ~:::!~:~.-,.-;.         ..  !; ~~, ,.......-$-~*$#!~,.  ..................... .-$$;-;-.  .!;...............,~- ... ,=~ .         ,~-  .,. ".ToCharArray(),
          " -*#=,                :;--*::~.,.~;.          ,-  !; ,~, ,...... -#~..-$##$!,    .................  :#$~-::.    !;. ............ ,~- .....=~            ~~.  ,, ".ToCharArray(),
          "!=!~, .~!-.          .:!-,!!;-.,~:,          .::  !: .~-........ -#;,  -:!*$=*:,    ............  .;=*~~::,     !;. ............ -:- .....*:            ,~-  .,.".ToCharArray(),
          "=;-   .-;!~,         .;!,.;!:,.::-           ,;; .!:  ~-........ ,$!-   ,~;!!*=!;~,     ......  .-!*!:-::~. ..  ;!, ............ -~- .... *;.            -~.  .,".ToCharArray(),
          ":.      .;!:.        .!;,.;!~,~;-.           -;~ ,*:  ~~, .......,=*- . .,~~~;!==*;~,.         .~*=;~-:;:,  .   ;!- ............ ~~, .... !!,            .~~. .,".ToCharArray(),
          "          ;*;        .=~ .;:~:!,  .          ~:- -*~  ~~- .......,*=~ .. .-;:~--:*$$=;-.      ,!#$~-~:::-. .    :!~ ..........   ~~,  .,. ;*-             ,;,  .".ToCharArray(),
          "          .:=;.      ,$- .~**:.              ::. :!-  -~~ ........!$~  .. .,:::~~~~;*==*;~,-;*==!~~~::;:,    .-;$*- ...,,.    ,-:===;. .. ~*~             .~:, .".ToCharArray(),
          "           .~*~. .. .-$- ,*$;,               ;~  !;,  ,~~ ........:$:       ,:;;:~~::;!*=*!*=!;;:~:::~~,   -;!=$@=~ .,.   ,-:;!*!!::!!,.. -*:              .:~. ".ToCharArray(),
          "            .;;- .. ,;!.,:$!,               ,;-  *:. ..~:......,..:#*~-,.    ,~~~~~~--~:;!==!:~--~~~~~-,-~:*$@@#@$; ....-~;!*!;:-,..-*;,  ,!;               -:- ".ToCharArray(),
          "              !:. . -*,,;=*.  .            .:;- .=~ .. -:, .......~#@#$!:~-,...,----~~--,-;;:;;~---~:::;=#@@@@#$#@*  .-:*=*:-.       .=:.. !!                :;,".ToCharArray(),
          "              -;; ..-!.!=;   .             .!:, ,$~    ,~-       .~###@@@@##$*!;!;;;:::::*;!@#$*;!*$#@@@@@#$######=.~*$*:,            ;!-  ;*.             . ,:!".ToCharArray(),
          "             ..:*-..:*!!~.                 ,*- .~*-,;*!*=*;;;:~-,,;#######@@@#######$$$$$#*!!!#####@@###########@@#**:-,.             -*:  :*,                ,~".ToCharArray(),
          "            .. -;;.~==;-.  .              .-*, ,!::;=!;;;!!!!!!!;!=@@############@@@@#@@@@*:--!#@@@@###########@$=!:-.                .!;  ~*~              .  .".ToCharArray(),
          "          ......-!~*$!.  ..               ,:!. -=:!*~.   ..,,-~:;!*==$$#@@#####$$#####@@##!-..~*#=@#########@@#=;-.                    ;!  ,!;               .  ".ToCharArray(),
          "         .....,. -$=~   .        .      . -!~. ~==:.                .-~!=#@@@@@#####$@@$;*;-..;*;.=@@$#@@@@@#$!.                       ;*  .:=                . ".ToCharArray(),
          ".       ......  -=!-. ..        .         ~!. .;#!,                    ..,-:*$#@@@@@#@!-,$#*~;$#*-,*@@@@$!:-,,.  ..                    ;* . ~=.                .".ToCharArray(),
          "!~.   ....... ,:!;,.  ..    ..         . ,:;  ~=#~.                        .,~;*###@@*--!=;!=$!!=*~:*#;:~,.       .....                ;* . -=-                 ".ToCharArray(),
          "!;~. ........-;!:.   .  .  . .           :;-  ;=*-                            .-;!!*$:~;$:.-*!-~!**;*$~.        .......                ;* . ,!:.                ".ToCharArray(),
          " ;!~ ....,..-==~  ... . ..  .       .   .;:  ,!;~;.                       .,,..    .!;*=;. ,~,,:!-*##$*~.      .--,                    ;* . .:*,                ".ToCharArray(),
          "  :*~..,,  ~$!,  ...  ..       ..    .  :!~  !;,,=-                         ..,,,  -##@=-;;:;:-~:;=*~-;=!~.. .~;;-.                   .!* ...-$-  . .           ".ToCharArray(),
          "  ,~!,.. .;*:,  ...         ...     .  .*:. .=: .=~               ..         ..,.,:=$==$*:--:!,.,~$;, .!=;.~::~-,.                    -*! ...,=~  .             ".ToCharArray(),
          "   .:~,.-;!~.  ...        . ....    . .~*- .;!-  *;.            ...... ....   .. :!;:~~;#-..,~.  .;!:-~**:~::-,                       :*: .,..*;. .             ".ToCharArray(),
          "..  .~~~*=-   ..         ........  .. -*;, -#-.  :*-           ........,--,,,,..,=!.  ,:*.        .!$*==;~:-.                         !!, .,..:*-               ".ToCharArray(),
          "*:-, .!#;.   ...    ..  ............  ~=. ,!=,.. ,*:               ..  ,---~~~~:;$=:  !=.         ,;**=---                            *;. ... ,*:           .   ".ToCharArray(),
          "@##$==*~.  ..   ...... ............. ~!:  :=,.....;;                        ...-~-;==:*$~   .!~, ,*:~!~.                              !:  .... *;     .  .      ".ToCharArray(),
          "##@@#!-.  ..  .. ............... .. ,;!. ~!: .... :*,                          ...,~**;;=;,,:=*;:;:,-=;,                              !;,...   :!, ..           ".ToCharArray(),
          "###$!,   ....~,. .................  :!~ ,*;. .... ~!~                              .-=;-:!;;*~:*=!..:#$;~--,.                         !=!:~~---:*:          .   ".ToCharArray(),
          "@@$;     . .-=-  ................  .*: .!=~ ,,,,. ,:: .                            . ;!-  *$!  ;$: -*@#$$$$;-.                        !#@##$$$=$#*              ".ToCharArray()
        };
        public readonly char[][] Vinette = {
        "~~---,,,,            .,,~,                         .           .    ,           ".ToCharArray(),
        ",-~;~~----,..   ..,,.  -,                                     -     .           ".ToCharArray(),
        "-,-,,.,-,,..,,,,,,.   .,                          .           ~  .              ".ToCharArray(),
        ",,~,,,-,,...,,,.,.    -                           .          ,-  .              ".ToCharArray(),
        ",,~-,,-.,..,,....    ,                           .          .,,                 ".ToCharArray(),
        "--~-,,-...,,...     .,                    .      .         ..,  .               ".ToCharArray(),
        "---~,-,,..,,....    ,                    .      ..        ., -  .               ".ToCharArray(),
        "~--~.~,,.,,....  .  .                    .      ,         , .,                  ".ToCharArray(),
        "-~-~,~-........    .                           .         -  ,  .                ".ToCharArray(),
        ",:-~-:,.......  .  .                    .     ..       .,   ,                   ".ToCharArray(),
        ":~-~-;........ .. .                    ,      ,       .-      ,      ,.         ".ToCharArray(),
        ",!--~-.......  ..                     ..     ,       ,.    . .       ~          ".ToCharArray(),
        ":~-,~ .......  .                     .~     ,       -.    .. .       ~          ".ToCharArray(),
        "-!-:,........ ..                     -     ,      ,,.     . .       ..          ".ToCharArray(),
        "-:~~.....  .  ..       .....        ~.    .     .~...    . .        ,.          ".ToCharArray(),
        "--: .. .  -.. ..            .,-,   -.   ,.    ,-.,       ..        .,,          ".ToCharArray(),
        "~!.. .. .,. . .                .,,~. .,.   .,,,.-.      ..         - ,          ".ToCharArray(),
        "!   .  ,~, .  .                 .-~~-.  .,-, ...,      .,          - ,          ".ToCharArray(),
        ".    ,~--. . ..                ,,-..,~-.., ... -      .,          ,  ,          ".ToCharArray(),
        "   .-~,,-... ..          .    ,.,    .-~, .   -.     .,           -  ,          ".ToCharArray(),
        ".~;~.-,-- .  ...        .   -,,,  ... -,~,   ..     ,.           -   ,         .".ToCharArray(),
        "!,-~,---,    ...     ...  .-,..     .-.  -. .-     .        .,,,--,,,.  .      ,".ToCharArray(),
        "~ -:,-,-.     .    ...  .~,..-.    --       -            .-~-.  -   , ..,      ,".ToCharArray(),
        "~.-:----.     ,  ...  ,~~,,--    ,-.       ,.          ,-,.    .,   ,   .     ,.".ToCharArray(),
        "~.-.~--,..    - .  .-~~..,,,   .~.        .,          .,       ,   ,.         -.".ToCharArray(),
        "-,~ :~-..     ,.,~~~~-~!$:. .-*~..,.     .,                   ,    -   ,     ,,,".ToCharArray(),
        "--.;;~,...    ;;!;::;;;~ .-:*==#=:,.,   .,  ,                .,   ..   -     -..".ToCharArray(),
        ";:,.;~-. ,    ;:;;;;*!;**=*==.:*!:,-.,.~.  -.                ,    ,   ,,    -,.,".ToCharArray(),
        ";,:-~:-..,    ;~;;;!!;;!**==$$*=-~;~-,~. .~~                ,    ..   -    .-.,~".ToCharArray(),
        ".=,-,:,-...  .: ::;--!;!***.:**=;=*=-, .:,:                ,~:~~,-    ,    , -;~".ToCharArray(),
        " ,*;~.~.,.,  .: .;;-,*!!****;***.;==::-,,-,               ,~,  .:.. ~;    ,.-:~,".ToCharArray(),
        " -,,;,;.-.-  .:   ;: *******!=$;,-!:=:-,,-          .... ,:;!-~=.,.:,,    -~*~. ".ToCharArray(),
        " ~-..::~ - . .;   .;.!!;!:!;!!=.,,,- ~..:          ...,.:;!!!!*~ ,*:;..  ,:-:,.-".ToCharArray(),
        ".~,,.  -,,,,  ;    ..-=;;!;*;;  ,.,,...:          ...,-!;!!*!!~.:!!=,   ,;~~..,:".ToCharArray(),
        ".~,,....;.~-. ~.      :;~:~:!:........-. ..       ...~!!***~=:***!*-,. ~;-~.-:;.".ToCharArray(),
        ",-,,.....~-,~ :.      .-;:!!, ...... -. ..         .~,!***=:!!*;~!~...:~:;~;;~.,".ToCharArray(),
        "-,,,..,. ,:,..-       ,!,.      .   ,, ,...       ~, -=**==!**$:.~  -$ *-:,~-,;.".ToCharArray(),
        "~.,,.,,. .-~..~         ,-,.     ,--,.,.....   .~-   -==*=!*!=~.: ,;~!~;!~ :~-~,".ToCharArray(),
        "~ ,,.,,, ..~. ,.              .~-,,,.,......,~:,  .  .=!::;;:=.~.:*.;!,.: -~,---".ToCharArray(),
        "~ .-,..,  ,-,  ,                .~,,-,----,,-.  .,... ;:!:::*;--::.-!*.-, :-,.-~".ToCharArray(),
        "~ .-,.,,  .,,   .              .-,-.      ,~~,,-., ....=~~:*: .,~ ,!~ -: ,--~ .~".ToCharArray(),
        "~. -,.,,   ,-   ..           .~~.      . ,:~    -,......-:~.,;~...-~ .~~-:~,-, ,".ToCharArray(),
        "~, -,,,,   .-    ..         .,          -;-     ,,,.      .~:,....,  .-:-.~~,-  ".ToCharArray(),
        "-- ,,,,,    ~      .                   ,~.       -,      ..  .... ,  - !:, ~,,, ".ToCharArray(),
        "-~ .,.,,.   -        ....             ,           -       .....   .,. ~ ~:, -,,,".ToCharArray(),
        "-~  ,.,,.   ..           ....,...   ..            ,.       ...    ,~.-.. -:, ~~-".ToCharArray(),
        "-~. . ,.,    ,                   ...               ,     .       , :.-  ..~-~.,:".ToCharArray(),
        ",~, . ,,,    -                                     .,..         -. .~    .-,--.,".ToCharArray(),
        ",--   .,,    ~                                        ..........,,,~-.    .-.,-.".ToCharArray(),
        ",,~    ,,.   ,-                                                --.  ~     .,...,".ToCharArray(),
        ",-~.   ...    :                                                ~    -      .,   ".ToCharArray(),
        ",--:    ,.    -~                                              .~.   ,.      ,.  ".ToCharArray(),
        ",-,~.   .,    .~-                                             ;,,.  .-.      .  ".ToCharArray(),
        ",--,~    ,.    ~:~                                           -,.,,   -..     ...".ToCharArray(),
        ",-----   ,,    ,~*,                                         .:., -,  ,..      ,.".ToCharArray(),
        ",---,~.  .!     ;,:-               .~-,                    .:,:., ,, - .       ,".ToCharArray(),
        ",,---,~   ~-    ,- :-                .......,,.           .-,~.~., ,.-.        .".ToCharArray(),
        ",,,-,,,~   ~,    ~. ;~                                   .-.,,~--., .: .        ".ToCharArray(),
        "-,.-,,,-~  -,,    -. -;                                 -,.,,,-,~,,,..~.        ".ToCharArray(),
        "-- --,,,--  ~,-    ~. .;.                             .~,,,,,,,-.~.,,, ~-       ".ToCharArray(),
        "-,. -,,,,-, ,,.,-   -,  :~                           --.,,,,,,,,-,-..--..~.     ".ToCharArray(),
        "-,. ,-,,,,-~ ~,*;-,. .,. ~:.                        ~-.,,,,,,,,,-.-..,--,.--.   ".ToCharArray(),
        ",,.  -,,,,,~- !=~..-~- .,..!~,                    -: .-,,,,..,,,,,.,.,,----.--. ".ToCharArray(),
        ".-,   -,,,,~.--=-,,...,.,,,.-;,                 ,-.:,  .,,,...,,,,.,..,,,,--,,--".ToCharArray(),
        " -,   .-,,--~!#;;,,.....      ;;              .~.   ;    ,,......., , .,,,,,,,--".ToCharArray(),
        " -,.   .-,,,~#**$ ,.......     ,::          ,~,     ,:    .. . ,,.. ,..,,,,,,,,,".ToCharArray(),
        " ,,,    ,-,,,$**=;.,........     ,;~.     .:~        ~-     .. .,,,,. .~~-,,,,,-".ToCharArray(),
        "..,,.    .--,!***=~.,..........    .::  .;-           ~.     .. .-,,..  .,-~~~-.".ToCharArray(),
        "- ,,.     .--,$**!=:................ ,~-*,             ;.     ,  ,-,,,  ......  ".ToCharArray(),
        "~ .,.       ,-:!**!=* ................  !               ;         ~-,,  ...,... ".ToCharArray(),
        "~  ,,.         !***=!=~ ...............,;               ~:        .---.  ...,.. ".ToCharArray(),
        "-. .,.          =!;=*;!=..,.....,......:~                ,-        --,-  ...,...".ToCharArray(),
        ".-  ...         .$~=**!:**, .-..-......*!                 .,       .-,~  .......".ToCharArray(),
        ":* ....          -~$****;:=*,..,.-...,.$*-             ,   .-       ---  ....,..".ToCharArray(),
        "*#-  ..         ,~~,*$***==,-#~- -...,,$;#            :,    .:      .~-   ....,.".ToCharArray(),
        "!=* . .         ~-,  .~;;~  *:=. ,.,,.:=**:          ;.-     .:.    .~-.  ......".ToCharArray(),
        "*=#. . ..      :.!        .;*!;. -; ,.!**~#         ,: -       :    .~--   .....".ToCharArray(),
        "$$$$.        .-.:.      ..:*!*,  ,$=.,#*!=*~        ;~ ,.      .:   .-,~    ....".ToCharArray(),
        "####;. ,....,,.:,      ..!*!=.-  ,==$,$!===*,      ,;-  ,        -  ..,-.   ....".ToCharArray(),
        ",####-,,.....,~,     .~*=*!*,-, ,,**=***=**$$$;    ~~~  ,   .    ~. - -~,.   ...".ToCharArray(),
        };

        public readonly char[][] Sumire = {
        "@@@@@##@@@@===$$=$$$=#==***!!!*!;:=$$===**=====$**!!:*!=;;*=::::-,,.,,,.*~::;#.~".ToCharArray(),
        "@@@@@@#@@@#!$#=$$$$=@$==*=***!**:$$=$========$=*;=*;!*!=;;:$::~::,,.,,---!::*$.-".ToCharArray(),
        "@@@@@@@@#=*##==$$$=#$=$=====*=*!$$;=;!*****!!$!!$=*!!*!=;;:;*~~:--,.,----:~;==,-".ToCharArray(),
        "@@@@@@@=!=@#*$$$$$##=$===$==$=*#=!=**********=*!$*=!**!$;;;~=~~:-~,,-~~.~-!:=!~~".ToCharArray(),
        "@@@@$*!==@$=$$$$$=#=$$=$$$$$$$$====!********=*=$===*=*!$;;:~;!~~~---,,-;.~!*;:::".ToCharArray(),
        "#$*!!!$*@$*$$$$==#$$$$$$$$$$$$===$!*******=====$====**!#;;:~-!--~--. . .. ~*~;,~".ToCharArray(),
        "!!!!=$*@=*$=====$#=$$$=$$$$$$===$$===*==*====$=$==*=**!#;;:~-!,~;--, .. ,  ;*:*-".ToCharArray(),
        "!!*=$!#=*=======@=$=====$$$$===$$$=$=$$===$$$=$===$**=*#!;~~~::---.-... :=:~;:~;".ToCharArray(),
        "==$#!@=!=======@$======$==#===$=$=$=$$$$$$$=$=$==$*****!!:~~~~*..,.,....;-!:~~;,".ToCharArray(),
        "@@#!#$;==*====##========$$===$=====$$$$$====$$*$$*=*=*=;!-::~;$.,..,,..;~~~::~.;".ToCharArray(),
        "#$*##;!!====*##===***===$===$==============$=*#$===**!$~!~:..-=-,..., ;-~~~~;  ;".ToCharArray(),
        "$**@!!!!**=*=#$*******=$===$=============$===#====***!$-!~.,,.!:...,.;~~~-:;; .!".ToCharArray(),
        "=!#!!!!!!!:=#$==*****==$*=$============$===$#$===****;;~~!.,,,-!.,,.;-~~~;:~;,:;".ToCharArray(),
        "!#=;!!!*!*=#=$;******=#*$======*=========$$=$*==**!=~-~,.;,,,, * ,.:-~~~~---~:!;".ToCharArray(),
        "*#!!!!**==$$==!;;!***=:==*!!***==$$$====$$==$*=**!*~*.~~.-,,,,.:..:-~~~~---:;!;!".ToCharArray(),
        "#!!!!!==*=#==***!!!**$$****=**=*****==$$$*=$=!=!~-=;;.,*.--,,,,~,:--~~--,:!;:;;!".ToCharArray(),
        "=*******!#*=$******=$$*******=*====$$$===*=$*!;,,!.! ..=,--,,,-,!---~--:!;;!!!!;".ToCharArray(),
        "!*******#$!=$****!=*#!=*****=********=*=*!#$~!--:-$-...!~,-,,-.;.-~~,:*!;!$: ,*-".ToCharArray(),
        "********#**==***!*$$!******=********#**!!=!;,~---:~   .;;,-,,,!---~;!;!;*:,.,.! ".ToCharArray(),
        "******!$#**=*****#=!=************=*=***;$.:------!    .~;-,,.:,--;*!!==~:.-,,-, ".ToCharArray(),
        "******=##!!$!!*!$=!=****!*=!***!=**$$*-#~,;,----=.     .!:-,~-:!!!==;-:*~,-,.~  ".ToCharArray(),
        "*****!#==!!$!!*$*!***!!;!=!!!*!*=!$:;,$!,:~----*        !;,~;!=!$=;--~~=---,;.. ".ToCharArray(),
        "****!$==!!*=!=#;*=!*!;:=$!!!*!!#!*:, =#,-!-,-,=         :~~,::-,$$:~~-:*,-,~. . ".ToCharArray(),
        "*****#*=!!#=$=!=*!;;;:=$;!!*!*=~;!.,*$,-~:,,~*.        .~:;.,,,-*$;~~~;!--,: ..,".ToCharArray(),
        "====$=!*!!@$*==:;;;!-!#;!!***!.,;.~=@-~,*.,~*          ..=*----,*$!~~~;~--;,.. !".ToCharArray(),
        "===*$;!!*===*;:;;;;:=#**!!**;,---!~*--,;;,~;             ***,---!$!~~-!,~--....=".ToCharArray(),
        "=====:*$$=*;:;;;~,:#$;!!=$$$-,~;!.!:--:*-,;              ,!#~,--;$*~~-;,-:;,  ~*".ToCharArray(),
        "$==$$==;~~---,-~;$!=-:~!==:-=$:. ~!!;;~;,!                ;==~--!$*;~::-~--;;.=!".ToCharArray(),
        "$*=#$~****===$*!$.;~::!!;!;!~  ,:=:~:!$,! .---,,.        .-**=--=$*!:!~~~~--~!!;".ToCharArray(),
        "$*=$#:!=;;#=!#-@@!::!;~;$*~ .:**, -==-:! ~$$$$==$##=!-     !:=!~$$*=:*-;~~-~.*;-".ToCharArray(),
        "$=$$#!;$:;#!~!==:-~::*$=- ~==!::!!~, ;;,         .,--~::--..$:#!$#*=!;::~~;-~*;~".ToCharArray(),
        "$=$!#$:=!:$;;@$=*=$@$*=#@:  ~      .,=.                  .-. *,$=#=*=,*-:~;,!!;;".ToCharArray(),
        "*=$:$$;!#;*!~#@*;*!=*!=#$:! ..     -*.     ,~---,. .*       ..!-##==!*!~~*~~$!;!".ToCharArray(),
        "!#=:=$=~#*=!~##!;!~$,~==$#$:      ..      ,,   ...-$,    ,   . -$$=$~#~:=~~:$=;!".ToCharArray(),
        ":#*~#*=*:!;=~#$!;~;~,  =;=#@~    ..      .  .!$###*@#*!!*, . ...@=#:=;:$:~-*!=!!".ToCharArray(),
        "!**=;-==!;@$;==;;:*.;  ~~~$!=    .         ,!==##@;$####$:--,. :@$=!*!$*~~~#=*=!".ToCharArray(),
        "$-$!-~.!=$:#*!=:;:!,.  -~ :@~-             :$$@$#$=@#@#-*@@$   *$$;*!$=~:~==!=$$".ToCharArray(),
        "=#!!-~,;@@#$@$*:::!;.:~ =,-.,-            -~,, !**#*=,$$$**!=-.#=**=$$:::~*;*=*;".ToCharArray(),
        "*=#*;.-@@@@@#*$::~=!,-:, =,.!                  -*-@**$~$:;**  $=*=$=$;~:~=$=~!$#".ToCharArray(),
        "#:=!~~$@@@@@==:::-=* .-~:!**.                  -.-,=$.-#~=-  -=!#=$$!~:;!!.~-*$=".ToCharArray(),
        "$=;$,*@@@@@#!!::~~*=.   -                      -~.-  .::-   .=:$=$=!~:!-*,,,~!=*".ToCharArray(),
        "*#=:=@@@=@@=*;::~!!$.                        ~:.-!...*=,    **@==*!~~#~!-.,,=!:=".ToCharArray(),
        "=~!=!#@@@@@*=;::~$;=.                         -~.:!!$:  ,- ;=;$*!;-:$;:!,: !;;*;".ToCharArray(),
        "=~-=$*#@@@#*!;:::=:!.                           ,         =; *:*:,*$=,*.- -$;;!;".ToCharArray(),
        "*~:@@$##@@!*;;:~!::;.                                   ,~, ;*!~~=$$:!~  .!!=;;*".ToCharArray(),
        "!-=@@@@@@#!!;;:-*~~;,                                      -=~~*#==*:!  .=,!*;;:".ToCharArray(),
        ":;@@@@@@@==!;::~;!,;-       ,                             :!;======-! .;*~!-=!;;".ToCharArray(),
        ":$@@@@@@@**!;:~*:!,:;       -.                        .-:=$==*$===:=-:!~~~!~~*;;".ToCharArray(),
        ":#@@@@@@=*!!;:~!:-,;*                              :*$$=*!;;*#==*~=!-.,,.~,:~;*;".ToCharArray(),
        "!@@@@@@@!!!!;:;-;,,;*,                              .,~:;;*#$=**;=*=;!;;!!;;:~!;".ToCharArray(),
        "=@@@@@@$!!!!;~!:~--:~!                              ...   *$=!*:=**:-~,     ,~;#".ToCharArray(),
        "#@@@@@@**!!;;;;*--~-.*                               . , *$***:=$!;::;!**!!!;:~~".ToCharArray(),
        "@@@@@@$!*!!;:!~:--~.-;-                             ~;~.;$!**;$$!$*=*!;~----~~~~".ToCharArray(),
        "@@@ @#***!!;::;,-.:;~.$.       -.                 .!-!-~$=!=*$$~;;:::::::::::::~".ToCharArray(),
        "@@@@@$****!;!;;~~!:,.,.!       ,*:.:-            :!-*;!~,;==$;~-*:;;::::::::::~~".ToCharArray(),
        "#@@@@#=**!;;;;;!!:--,.!*!          .;          .=,;!:::~:~.;=$,!::;;;::::::::~~~".ToCharArray(),
        "#@#~.     ......,--,:!~ :~                .::;*!~;;::~--,,-.-;;;;;;;;:::::::::::".ToCharArray(),
        "@@@@#*;:  ..,,.,,-:*;  ~##,               *!.~;~!;:-,,....,.**:;;;;;;;;;;;;:::::".ToCharArray(),
        "@@##=* ;:, .,,~:!:~, -*=:,!-            .!!,;!$;,,;===*=*!;*;:;;;;;;;;;;;;;;;;::".ToCharArray(),
        "@@$#!!. =**~~!;-~,,.!=*~-~-*-          .-:$$;$-,!$#==*=**!;:;;;;;;;;;;;;;;;;;:!$".ToCharArray(),
        "@$#;*;. ,$;;;--~~:-=*!-~~~~-;~        .-!!*;=$=$$$======!;;;;;;;;;;;;;;;;;;!*$*;".ToCharArray(),
        "##*!*~.  *=~~~-;-:=*:-~~~~~~-!-       -=-=!;$*=$$=====*!;;;;;;;;;;;;;;;;!*$$=!;;".ToCharArray(),
        "$$!!*~   ~;~~~--=;=-~~~~~~~~~,=     :$#~,*;$$$=====*!!!;;;;;;;;;;;;;!*=##==*;;;;".ToCharArray(),
        "#!!!*-   --:~-;$!$~~~~~~~~~~~~~$~:*==$,--**#===========*********===$$#$===!;;;;;".ToCharArray(),
        "=!!!*-   ~ !,!=!=,~~~~~~~~~~-,;$#=**@~~-~!$=========$$$$$==$$$$$=$##$*===!;;;;;;".ToCharArray(),
        "!!!!*,   ~ ~*=*$:~~~~~~~~--:=$=***==*.;-;=$============$$$$$$=$##$*;!=**;;;;;;;;".ToCharArray(),
        "!!!!*,   - -$$!!-~~~~~---;$$*!*!!*;$-.!-*#==*=========$$$$$$$##*;;;!=**;;;;;;;;;".ToCharArray(),
        "!!!!*,   - .**#!==***=$$#**********$,,!~=#=****=======$$$=$#$!;;!;***!!;;;;;;;;;".ToCharArray(),
        "!!!;#    ~  *;:!!!!!*=**!*********==.,!;$==***=========$$#=!;;!!;***!!;!;;;;;;;;".ToCharArray(),
        "!!;#$    ~  !;;;;;;==*************$*-,!!#===*=========#@=:;!!!!;***!!!!!;;;;;;;;".ToCharArray(),
        "!;$$*    ~  *;;;!*****************$*;-:!#=**=======$##*;;!!!!!!**!!!!!!;;;;;;;;;".ToCharArray(),
        ";$$#:    ~  !====*********==*******=;~~$$=*====**=##*:;!!!!!;!**!!!!!;;;;;;;;;;;".ToCharArray(),
        "==#=-    :  ~=**********====*******=!~:$#=*=*=*=#$!;;!!!!;;!**!!!!!!;;;;;;;;;;;!".ToCharArray(),
        "$#$~,    ~  ~;$======**==========***#;~:#=====##*;!!!!!!!!***!;!!!;;!!!!!!!!!!!*".ToCharArray(),
        "$#!;..   ~  ~~**=========*==========*$!~;#==##!;!!!!;;!*===*;;!!!!!!!!!!!****==*".ToCharArray(),
        "#@;;..   -  ~-$$*=====================$*:!#$*;!!!!!**====*!;;;;;!!!!!!!!!!***==$".ToCharArray(),
        "#=!; .   ,  ,=~$======================$$#!!:!!;!==$$=====***!!!;!!!!!!!!!!;!$=, ".ToCharArray(),
        "=$*~ .  .,  ,*;!#!=================$$=$$!!*$*==$$$===========***!!!!!!;;!*!~.   ".ToCharArray(),
        "~**~ .  .,  ,=;$@$**=============$$==#*;**==$===================****!;!=;.      ".ToCharArray(),
        ";~$, .  ,.  ,$;;!$#=**======$$$$$=$#$*$$$$=$$#===========*********!=$!-.        ".ToCharArray(),
        };
    }
}