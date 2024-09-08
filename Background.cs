using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Project_jUMPKING
{
    class Background
    {
        public static readonly int width = 162;
        public static readonly int height = 611;

        BackgroundImage back_Image = new BackgroundImage();
        Princess princess = new Princess(110, 17);

        public Princess Princess { get { return princess; } }

        private int prePosX = 45;
        private int prePosY = 99;
        private int jumpPosY = 10;
        private int save_posX = 0, save_posY = 0;

        private char[,] _background = new char[width, height];
        private char[] save_Char = new char[4];
        char[][][] Image80 = { };
        char[][][] Image160 = { };

        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();

        public Dictionary<int, Item> item_Dic = new Dictionary<int, Item>();
        private int item_Dic_Count;



        public void Load(string[] save)
        {
            int posY = int.Parse(save[1].Split(' ')[1]);
            if (save[3].Split(' ')[1] == "True") Get_Item(0, posY);
            if (save[4].Split(' ')[1] == "True") Get_Item(1, posY);
            if (save[5].Split(' ')[1] == "True") Get_Item(2, posY);
            if (save[6].Split(' ')[1] == "True") Get_Item(3, posY);
        }
        public Background(int map = 1)
        {
            for (int i = 0; i < _background.GetLength(0); i++)
            {
                for (int j = 0; j < _background.GetLength(1); j++)
                {
                    _background[i, j] = ' ';
                }
            }
            Save_Map(map);
            Save_Background(map);
            Save_ground();
            Save_Wall();
            Save_Plattform(map);
            Save_Item(map);
            Draw_Background(map);
        }
        public void Save_Map(int map)
        {
            Image80 = back_Image.GetMap80(map);
            Image160 = back_Image.GetMap160(map);
        }
        public void Save_ground()
        {
            for (int i = 0; i < _background.GetLength(0); i++)
            {
                _background[i, 0] = '▣';
            }
            for (int i = 0; i < _background.GetLength(0); i++)
            {
                _background[i, _background.GetLength(1) - 1] = '▣';
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
        public void Save_Background(int map = 1)
        {
            if (map == 1)
            {
                for (int i = 0; i < 160; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        _background[i + 1, height - 500 + j] = Image160[2][j][i];
                    }
                }
                for (int i = 0; i < 160; i++)
                {
                    for (int j = 0; j < 112; j++)
                    {
                        _background[i + 1, height - 352 + j] = Image160[1][j][i];
                    }
                }
                for (int i = 0; i < 160; i++)
                {
                    for (int j = 0; j < 160; j++)
                    {
                        _background[i + 1, height - 240 + j] = Image160[0][j][i];
                    }
                }
                for (int i = 0; i < 80; i++)
                {
                    for (int j = 0; j < 80; j++)
                    {
                        _background[i + 1, height - 80 + j] = Image80[0][j][i];
                    }
                }
                for (int i = 0; i < 80; i++)
                {
                    for (int j = 0; j < 80; j++)
                    {
                        _background[i + 81, height - 80 + j] = Image80[1][j][i];
                    }
                }
            }
            else if (map == 0)
            {
                for (int i = 0; i < 160; i++)
                {
                    for (int j = 0; j < 160; j++)
                    {
                        _background[i + 1, height - 160 + j] = Image160[0][j][i];
                    }
                }
            }
        }
        public void Save_Plattform(int map = 1)
        {
            char x = 'x';
            char y = 'y';
            char z = 'z';
            char s = 's';
            char b = 'b';
            if (map == 1)
            {
                //Row 1 ~ 80
                Platform(1, 18, 20);
                Platform(37, 20, 10);
                Platform(61, 18, 20);

                Platform(1, 35, 30);
                Platform(80, 35, -30);

                Platform(38, 50, 7);
                Platform(70, 55, 3);

                Platform(67, 75, 3);

                Platform(48, 82, 5, z);

                Platform(1, 81, 17, z, -6);
                Platform(18, 80, -5, s);

                Platform(25, 113, 3);

                Platform(45, 112, 3, s);
                Platform(46, 113, 3);
                Platform(49, 112, -3, s);

                Platform(65, 100, 5, y);

                Platform(70, 120, 3);
                Platform(73, 119, -3, s);
                Platform(73, 109, -3, s);
                Platform(73, 99, -3, s);

                Platform(70, 134, 3);
                Platform(70, 160, 3);
                Platform(62, 168, 3);

                Platform(60, 136, 3);

                Platform(50, 138, 3);

                Platform(35, 146, 2);

                Platform(5, 153, 5);
                //
                Platform(13, 173, 10);
                Platform(35, 178, 5);

                Platform(50, 195, 7);
                Platform(57, 195, 6,y);

                Platform(20, 195, 16,z,-3);
                Platform(50, 212, 8,z,-5);
                Platform(50, 228, 8, z,6);

                Platform(70, 195, 4, z, 25);

                Platform(74, 224, -3, s);
                Platform(80, 190, 3, s);

                Platform(21, 218, 14);
                Platform(27, 225, 2);
                Platform(26, 224, 6, s);
                Platform(29, 224, -6, s);
                

                //Snow
                for (int i = 0; i < 20; i++) _background[40 + i, height - 252] = '▒';
                for (int i = 0; i < 20; i++) _background[40 + i, height - 251] = '□';

                for (int i = 0; i < 20; i++) _background[100 + i, height - 262] = '▒';
                for (int i = 0; i < 20; i++) _background[100 + i, height - 261] = '□';

                for (int i = 0; i < 20; i++) _background[140 + i, height - 272] = '▒';
                for (int i = 0; i < 20; i++) _background[140 + i, height - 271] = '□';

                for (int i = 0; i < 20; i++) _background[76 + i, height - 282] = '▒';
                for (int i = 0; i < 20; i++) _background[76 + i, height - 281] = '□';
                Platform(48, 280, -3, s);

                for (int i = 0; i < 30; i++) _background[14 + i, height - 291] = '▒';
                for (int i = 0; i < 30; i++) _background[14 + i, height - 290] = '□';
                Platform(22, 289, -3, s);

                for (int i = 0; i < 14; i++) _background[2 + i, height - 298] = '▒';
                for (int i = 0; i < 14; i++) _background[2 + i, height - 297] = '□';


                //glacier
                for (int i = 0; i < 14; i++) _background[2 + i, height - 315] = '▒';
                for (int i = 0; i < 14; i++) _background[2 + i, height - 314] = '□';
                for (int i = 0; i < 122; i++) _background[40 + i, height - 315] = '▒';
                for (int i = 0; i < 122; i++) _background[40 + i, height - 314] = '□';

                Platform(13, 340, -10, s);
                Platform(10, 339,-8, y);
                Platform(9, 340, 4, x, 0, '▤');

                Platform(40, 325, 35,x,0,'▤');
                Platform(30, 335, 40, x, 0, '▤');

                Platform(40, 350, 35, x, 0, '▤');
                Platform(30, 360, 40, x, 0, '▤');
                Platform(29, 359, 3, s);

                //desert
                Platform(1, 373, 15);
                Platform(13, 385, 20, b,-8, '▩');
                Platform(40, 382, 5, b, -2, '▩');
                Platform(50, 390, 10, b, -4, '▩');
                Platform(70, 410, 4, b, 6, '▩');
                Platform(32, 420, 12, b, 4, '▩');
                Platform(60, 415, 5, b, -2, '▩');

                Platform(70, 405, 8);

                //desert 2
                Platform(13, 440, 12, b, -6, '▩');
                Platform(53, 444, 12, b, -7, '▩');

                Platform(6, 453, 8);

                Platform(21, 457, 13, b, -8, '▩');

                Platform(41, 464, 6);
                Platform(47, 463, -4, s);

                Platform(28, 470, 13, b, -4, '▩');
                Platform(16, 469, 7, b, 8, '▩');

                Platform(48, 480, 7, b, -2, '▩');

                Platform(63, 469, 6, b, 10, '▩');
                Platform(34, 490, 10, b, -4, '▩');

                Platform(80, 487, -4);
                Platform(76, 493, 5, b, 8, '▩');

                //end
                Platform(1, 505, 73);

                Platform(20, 526, 7, z, 8);
                Platform(50, 539, 7, z, 10);
                Platform(50, 512, 7, z, 10);

                Platform(20, 505, 7, z, 4);
                Platform(1, height, 79);

                //ending
                for (int i = 0; i < 41; i++) _background[20 * 2 + 2 * i, 20] = '▣';
                for (int i = 0; i < 6; i++) _background[120, 14 + i] = '▣';
                for (int i = 0; i < 2; i++) _background[10 * 2 + 2 * i, 20] = '▣';
                for (int i = 0; i < 6; i++) _background[40, 21 + i] = '▣';
                for (int i = 0; i < 2; i++) _background[10 * 2 + 2 * i, 40] = '▣';
                for (int i = 0; i < 30; i++) _background[20 * 2 + 2 * i, 27] = '▣';

                for (int i = 0; i < 6; i++) _background[20 * 2 + 2 * i, 41] = '▣';
                for (int i = 0; i < 19; i++) _background[40, 41 + i] = '▣';
                for (int i = 0; i < 19; i++) _background[52, 41 + i] = '▣';

                for (int i = 0; i < 20; i++) _background[100, 27 + i] = '▣';
                for (int i = 0; i < 26; i++) _background[110, 21 + i] = '▣';

                for (int i = 0; i < 6; i++) _background[50 * 2 + 2 * i, 47] = '▣';
                for (int i = 0; i < 7; i++) _background[20 * 2 + 2 * i, 60] = '▣';
            }
            else if (map == 0)
            {
                Platform(1, 10, 50);
                Platform(20, 25, 50);
                Platform(1, 40, 50);

                Platform(65, 43, -10, s);
                Platform(64, 33, 11, y);
                Platform(64, 33, 11);

                Platform(80, 54, -5);
                Platform(73, 67, -5);

                Platform(60, 70, -55);

                Platform(1, 70, 10, z);

                Platform(80, 110, -47);
                for (int i = 0; i < 5; i++) _background[75 * 2 + 2 * i, height - 112] = '※';
                for (int i = 0; i < 5; i++) _background[75 * 2 + 2 * i, height - 113] = '※';
                for (int i = 0; i < 5; i++) _background[75 * 2 + 2 * i, height - 114] = '※';
                for (int i = 0; i < 5; i++) _background[75 * 2 + 2 * i, height - 115] = '※';
                for (int i = 0; i < 5; i++) _background[75 * 2 + 2 * i, height - 116] = '※';

            }

        }
        private void Platform(int startX, int startY, int length, char dir = 'x', int h = 0 , char block = '▣')
        {
            if (dir == 'x') // 가로
            {
                if (length > 0)
                    for (int i = 0; i < length; i++) _background[startX * 2 + 2 * i, height - startY] = block;
                else
                    for (int i = 0; i < length * -1; i++) _background[startX * 2 - 2 * i, height - startY] = block;
            }
            else if (dir == 'y') // 세로
            {
                if (length > 0)
                    for (int i = 0; i < length; i++) _background[startX * 2, height - startY - i] = block;
                else
                    for (int i = 0; i < length * -1; i++) _background[startX * 2 - 2, height - startY + i] = block;
            }
            else if (dir == 'z') // 빈 블럭
            {
                for (int i = 0; i < length; i++) _background[startX * 2 + 2 * i, height - startY] = block;
                for (int i = 0; i < length + h; i++) _background[startX * 2, height - startY - i] = block;
                for (int i = 0; i < length + h; i++) _background[startX * 2 + (length - 1) * 2, height - startY - i] = block;
                for (int i = 0; i < length; i++) _background[startX * 2 + 2 * i, height - startY - length - h] = block;
            }
            else if (dir == 's') // 경사
            {
                if (length > 0)
                    for (int i = 0; i < length; i++) _background[startX * 2 - 2 * i, height - startY + i] = '↙';
                else
                    for (int i = 0; i < length * -1; i++) _background[startX * 2 + 2 * i, height - startY + i] = '↘';
            }
            else if (dir == 'b') // 채워진 블럭
            {
                for (int i = 0; i < length; i++)
                {
                    for(int j = 0; j < length + h; j++)
                    {
                    _background[startX * 2 + 2 * i, height - startY - j] = block;
                    }
                }
            }
        }

        public void Save_Item(int map = 1)
        {
            if (map == 1)
            {
                int x, y;
                // 0번 아이템(세이브 로드)
                x = 20; y = height - 115;
                item_Dic.Add(0, new SaveLoad(x, y, _background[x, y]));
                _background[x, y] = item_Dic[0].get_Char;

                // 1번 아이템
                x = 144; y = height - 105;
                item_Dic.Add(1, new Further(x, y, _background[x, y]));
                _background[x, y] = item_Dic[1].get_Char;

                // 2번 아이템
                x = 15; y = height - 168;
                item_Dic.Add(2, new Higher(x, y, _background[x, y]));
                _background[x, y] = item_Dic[2].get_Char;

                // 3번 아이템
                x = 144; y = height - 228;
                item_Dic.Add(3, new Longer(x, y, _background[x, y]));
                _background[x, y] = item_Dic[3].get_Char;

                item_Dic_Count = item_Dic.Count;
            }
            else if (map == 0)
            {
                int x, y;
                // 0번 아이템(세이브 로드)
                x = 53; y = height - 32;
                item_Dic.Add(0, new SaveLoad(x, y, _background[x, y]));
                _background[x, y] = item_Dic[0].get_Char;

                //// 1번 아이템
                x = 116; y = height - 74;
                item_Dic.Add(1, new Further(x, y, _background[x, y]));
                _background[x, y] = item_Dic[1].get_Char;

                //// 2번 아이템
                x = 76; y = height - 74;
                item_Dic.Add(2, new Higher(x, y, _background[x, y]));
                _background[x, y] = item_Dic[2].get_Char;

                //// 3번 아이템
                x = 37; y = height - 74;
                item_Dic.Add(3, new Longer(x, y, _background[x, y]));
                _background[x, y] = item_Dic[3].get_Char;

                item_Dic_Count = item_Dic.Count;
            }
        }

        public void Save_Position(int preSaveX, int preSaveY, int saveX, int saveY) //저장하는 부분 모델링
        {
            save_posX = saveX;
            save_posY = saveY;
            if (preSaveX != 0)
            {
                //이전 세이브 위치 배경 복원
                _background[preSaveX - 1, preSaveY] = save_Char[0];
                _background[preSaveX, preSaveY] = save_Char[1];
                _background[preSaveX - 1, preSaveY + 1] = save_Char[2];
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

        public void DrawChar(int positionX, int positionY, int direction_right, bool knockbacked = false, bool knockdowned = false)
        {
            if(direction_right == 2) // 낙하 시 출력 보정
            {
                direction_right = 1;
                Console.SetCursorPosition(positionX + 4, positionY + 1);
                Console.Write("{0}{1}",_background[positionX + 3, positionY + 1], _background[positionX + 2, positionY + 1]);
            }
            else if(direction_right == -2)
            {
                direction_right = -1;
                Console.SetCursorPosition(positionX - 4, positionY + 1);
                Console.Write("{0}{1}",_background[positionX - 5, positionY + 1],_background[positionX - 4, positionY + 1]);
            }
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

            if (save_posX != 0)
            {
                if (save_posX >= prePosX - 3 && save_posX <= prePosX + 3)
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
            if(prePosY < 240 && prePosY > height - 505) // 유사 유사 보정
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                for (int i = 0; i < 6;i++)
                {
                    for(int j = 0; j < 8;j++)
                    {
                        if (_background[prePosX - 2 + i, positionY - 4 + j] == '▩')
                        {
                            Console.SetCursorPosition(prePosX -2 + i, positionY - 4 + j);
                            Console.Write('▩');
                        }
                    }
                }
                Console.ResetColor();
            }
            else if (prePosY < height - 248 && prePosY > height - 303) // 눈맵 눈 보정
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (_background[prePosX - 2 + i, positionY - 4 + j] == '▒')
                        {
                            Console.SetCursorPosition(prePosX - 2 + i, positionY - 4 + j);
                            Console.Write('▒');
                        }
                    }
                }
                Console.ResetColor();
            }
            else if (prePosY < height - 310 && prePosY > height - 320) // 빙하 눈 보정
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_background[prePosX - 2 + i, positionY + j] == '▒')
                        {
                            Console.SetCursorPosition(prePosX - 2 + i, positionY + j);
                            Console.Write('▒');
                        }
                    }
                }
                Console.ResetColor();
            }

            prePosX = positionX;
            prePosY = positionY;

            if(!knockdowned)
            {
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
                    if (!knockbacked)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(positionX - 2, positionY + 1);
                        Console.Write('▥');
                        Console.SetCursorPosition(positionX + 2, positionY + 1);
                        Console.Write('▥');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(positionX - 1 - direction_right, positionY + 1);
                        Console.Write("▥▥");
                    }

                }
            }
            else // 기절했을 때
            {
                if (direction_right == 1)
                {
                    { //윗부분
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(positionX, positionY);
                        Console.Write("▥▥");
                    }
                    //아랫부분
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.SetCursorPosition(positionX - 2, positionY + 1);
                    Console.Write('▥');
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(positionX, positionY + 1);
                    Console.Write('▥');
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.SetCursorPosition(positionX+ 2, positionY + 1);
                    Console.Write('▥');
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(positionX + 4, positionY + 1);
                    Console.Write('↓');
                }
                else
                {
                    { //윗부분
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(positionX - 2, positionY);
                        Console.Write("▥▥");
                    }
                    //아랫부분
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.SetCursorPosition(positionX + 2, positionY + 1);
                    Console.Write('▥');
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(positionX, positionY + 1);
                    Console.Write('▥');
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.SetCursorPosition(positionX - 2, positionY + 1);
                    Console.Write('▥');
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(positionX - 4, positionY + 1);
                    Console.Write('↓');
                }
            }

            Console.ResetColor();

        }

        public void DrawChar_charging(int positionX, int positionY, int direction_right)
        {
            sb2.Clear();
            Console.SetCursorPosition(prePosX - 2, prePosY - 3);
            for (int i = 0; i < 6; i++)
            {
                sb2.Append(_background[prePosX - 3 + i, prePosY - 3]);

            }
            Console.Write(sb2);

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

            prePosX = positionX;
            prePosY = positionY;

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
        public void Draw_Background(int map = 1)
        {
            sb.Clear();
            if (map == 1)
            {
                for (int i = 0; i < height - 1; i++)
                {
                    sb.Append('▣'); // 좌측 벽
                    if (i > height - 81) // 배경
                    {
                        for (int j = 0; j < 80; j++)
                        {
                            sb.Append(Image80[0][i - (height - 80)][j]);
                        }
                        for (int j = 0; j < 80; j++)
                        {
                            sb.Append(Image80[1][i - (height - 80)][j]);
                        }
                    }
                    else if (i > height - 241)
                    {
                        for (int j = 0; j < 160; j++)
                        {
                            sb.Append(Image160[0][i - (height - 240)][j]);
                        }
                    }
                    else if (i > height - 353)
                    {
                        for (int j = 0; j < 160; j++)
                        {
                            sb.Append(Image160[1][i - (height - 352)][j]);
                        }
                    }
                    else if (i > height - 373)
                    {
                        for (int j = 0; j < 160; j++)
                        {
                            sb.Append(' ');
                        }
                    }
                    else if (i > height - 501)
                    {
                        for (int j = 0; j < 160; j++)
                        {
                            sb.Append(Image160[2][i - (height - 500)][j]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 160; j++)
                        {
                            sb.Append(' ');
                        }
                    }
                    sb.Append('▣'); // 우측벽
                    sb.AppendLine();
                }
                for (int j = 0; j < 82; j++) // 바닥
                {
                    sb.Append('▣');
                }
                for (int i = 0; i < 40; i++) // 메뉴
                {
                    sb.AppendLine();
                }
                sb.Append("\t\t\t\t\t\t\t\t\t┌────────────────────────────────────────────────────────────────────────────────────────┐"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                       Jump King                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                      1. 계속하기                                       │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                      2. 저장하기                                       │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                      3. 게임종료                                       │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                      4. 배경 다시그리기                                │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t└────────────────────────────────────────────────────────────────────────────────────────┘"); sb.AppendLine();
            }

            else if (map == 0) // 튜토리얼
            {
                for (int i = 0; i < height - 1; i++)
                {
                    sb.Append('▣');
                    if (i > height - 161)
                    {
                        for (int j = 0; j < 160; j++)
                        {
                            sb.Append(Image160[0][i - (height - 160)][j]);
                        }
                    }
                    else if (i > height - 241)
                    {
                        for (int j = 0; j < 160; j++)
                        {
                            sb.Append(' ');
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
                for (int i = 0; i < 40; i++)
                {
                    sb.AppendLine();
                }
                sb.Append("\t\t\t\t\t\t\t\t\t┌────────────────────────────────────────────────────────────────────────────────────────┐"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                       Jump King                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                      1. 계속하기                                       │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                      2. 저장하기 (튜토리얼은 저장불가)                 │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                      3. 게임종료                                       │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t│                                                                                        │"); sb.AppendLine();
                sb.Append("\t\t\t\t\t\t\t\t\t└────────────────────────────────────────────────────────────────────────────────────────┘"); sb.AppendLine();
            }
        }

        public void Print_Back(int map = 0)
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.Write(sb);
            for (int j = 1; j < _background.GetLength(1) - 1; j++)
            {
                for (int i = 1; i < _background.GetLength(0) - 1; i++)
                {
                    if(j < 490 && map == 0)
                    if (map == 1 && j < 490)
                    {
                        j = 490;
                    }
                    if (j > height - 62) // 구간 설정
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (j > height - 62 * 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (j > height - 62 * 3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else if (j > height - 62 * 4)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    else if (j > height - 62 * 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    else if (j > height - 62 * 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (j > height - 62 *  8 -9 )
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (_background[i, j] == '▣') // 장애물 그리기
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('▣');
                        continue;
                    }
                    else if (_background[i, j] == '↙')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('↙');
                        continue;
                    }
                    else if (_background[i, j] == '↘')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('↘');
                        continue;
                    }
                    else if (_background[i, j] == '※')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('※');
                        continue;
                    }
                    else if (_background[i, j] == '▒')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('▒');
                        continue;
                    }
                    else if (_background[i, j] == '□')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('▒');
                        continue;
                    }
                    else if (_background[i, j] == '▤')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('▤');
                        continue;
                    }
                    else if (_background[i, j] == '▩')
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write('▩');
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

        public void Draw_Storm(int sequence) //모래바람
        {
            int[] x = new int[] { 70, 80, -36, 45, 30, 40, -28, -20, -30 , 0, 40 , 30 , 60 ,44}; //+ 40 ~ 80
            int[] y = new int[] { 146, 147, 114, 155, 157, 159, 124, 148, 163 , 173 , 168, 123, 117, 135};
            if (sequence < 20) // % 20 써서 나머지 구하면 더 깔끔해짐
            {
                if (sequence == 0)
                {
                    for (int i = 0; i < x.Length; i++)
                    {
                        Console.SetCursorPosition(x[i] + 38, y[i]);
                        Console.Write("{0}{1}", _background[x[i] + 35 + 2 - sequence * 2, y[i]], _background[x[i] + 35 + 3 - sequence * 2, y[i]]);
                        Console.SetCursorPosition(x[i] + 38, y[i]+1);
                        Console.Write("{0}{1}", _background[x[i] + 35 + 2 - sequence * 2, y[i]+1], _background[x[i] + 35 + 3 - sequence * 2, y[i] + 1]);
                    }
                }
            }
            else if (sequence < 40)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    Console.SetCursorPosition(x[i] - 1 + sequence * 2, y[i]);
                    Console.Write("{0}{1}", _background[x[i] - 2 + sequence * 2, y[i]], _background[x[i] - 1 + sequence * 2, y[i]]);
                    Console.SetCursorPosition(x[i] - 1 + sequence * 2, y[i]+1);
                    Console.Write("{0}{1}", _background[x[i] - 2 + sequence * 2, y[i] + 1], _background[x[i] - 1 + sequence * 2, y[i] +1]);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                //speedX += 0.01;
                for (int i = 0; i < x.Length; i++)
                {
                    Console.SetCursorPosition(x[i] + 1 + sequence * 2, y[i]);
                    Console.Write("{0}{1}", _background[x[i] + sequence * 2, y[i]], _background[x[i] + 1 + sequence * 2, y[i]]);
                    Console.SetCursorPosition(x[i] + 1 + sequence * 2, y[i]+1);
                    Console.Write("{0}{1}", _background[x[i] + sequence * 2, y[i] + 1], _background[x[i] + 1 + sequence * 2, y[i] + 1]);
                }
            }
            else if (sequence < 60)
            {
                if(sequence == 40)
                {
                    for (int i = 0; i < x.Length; i++)
                    {
                        Console.SetCursorPosition(x[i] - 1 + sequence * 2, y[i]);
                        Console.Write("{0}{1}", _background[x[i] - 2 + sequence * 2, y[i]], _background[x[i] - 1 + sequence * 2, y[i]]);
                        Console.SetCursorPosition(x[i] - 1 + sequence * 2, y[i] + 1);
                        Console.Write("{0}{1}", _background[x[i] - 2 + sequence * 2, y[i] + 1], _background[x[i] - 1 + sequence * 2, y[i] + 1]);
                    }
                }

            }
            else
            {
                for (int i = 0; i < x.Length; i++)
                {
                    Console.SetCursorPosition(x[i] + 195 + 3  - sequence * 2, y[i]);
                    Console.Write("{0}{1}", _background[x[i]+ 195 + 2 - sequence * 2, y[i]], _background[x[i] + 195 + 3 - sequence * 2, y[i]]);
                    Console.SetCursorPosition(x[i] + 195 + 3 - sequence * 2, y[i] + 1);
                    Console.Write("{0}{1}", _background[x[i] + 195 + 2 - sequence * 2, y[i] + 1], _background[x[i] + 195 + 3 - sequence * 2, y[i] + 1]);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                //speedX += 0.01;
                for (int i = 0; i < x.Length; i++)
                {
                    Console.SetCursorPosition(x[i] + 196 - sequence * 2, y[i]);
                    Console.Write("{0}{1}", _background[x[i] + 195 - sequence * 2, y[i]], _background[x[i] + 195 + 1 - sequence * 2, y[i]]);
                    Console.SetCursorPosition(x[i] + 196 - sequence * 2, y[i] + 1);
                    Console.Write("{0}{1}", _background[x[i] + 195 - sequence * 2, y[i] + 1], _background[x[i] + 195 + 1 - sequence * 2, y[i] + 1]);
                }
                //speedX -= 0.01;
            }
            Console.ResetColor();
        }

        public void Get_Item(int itemNum, int posy)
        {
            Item item = item_Dic[itemNum];
            int x = item.get_posX;
            int y = item.get_posY;
            char tempChar = item.get_tempChar;
            item.getItem = true;
            _background[x, y] = tempChar;

            sb2.Clear(); // 아이템 위치 배경 초기화
            Console.SetCursorPosition(x - 2, y - 2);
            for (int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y - 2]);
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(x - 2, y - 1);
            for (int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y - 1]);
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
            Console.SetCursorPosition(x - 2, y + 1);
            for (int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y + 1]);
            }
            Console.Write(sb2);

            sb2.Clear();
            Console.SetCursorPosition(x - 2, y + 2);
            for (int i = 0; i < 5; i++)
            {
                sb2.Append(_background[x - 2 + i, y + 2]);
            }
            Console.Write(sb2);

            Player.Get_item(itemNum);
            item_Dic[itemNum].Set_pos();
            int height = Background.height - 1;
            while (posy < height)
            {
                height -= 62;
                continue;
            }
            if (height < 0) { item_Dic[itemNum].Set_posY(49); }
            else { item_Dic[itemNum].Set_posY(height + 49); }
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
                if (power == 0) // 최초 생성
                {
                    Console.BackgroundColor = color;
                    if (height < 0)
                    {
                        for (int i = 0; i < 11; i++) // 에너지 바 그리기
                        {
                            Console.SetCursorPosition(198, 41 - i * 2);
                            Console.Write("         ");
                            Console.SetCursorPosition(198, 40 - i * 2);
                            Console.Write("         ");
                        }
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow; // 스킬 그리기
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
            if (power == 1 || power == 52) // 에니지 바 초기화
            {
                Console.BackgroundColor = color;
                for (int i = 0; i < 11; i++)
                {
                    if (height < 0)
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
                if (height < 0 && power != 0)
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
                    if (num > 0)
                        return num;
                }
            }
            else // Left or Right
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        num = check(positionX - i + 3 * direction_right, positionY - 2 + j);
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
                if (_background[x, y] == ' ')
                {
                    return 0;
                }
                else if (_background[x, y] == '▣')
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
                else if (_background[x, y] == '□')
                {
                    return 4;
                }
                else if (_background[x, y] == '▤')
                {
                    return 5;
                }
                else if (_background[x, y] == '▩')
                {
                    return 6;
                }
                else if (_background[x, y] == '0')
                {
                    Get_Item(0, y);
                }
                else if (_background[x, y] == '★')
                {
                    Get_Item(1, y);
                }
                else if (_background[x, y] == '☆')
                {
                    Get_Item(2, y);
                }
                else if (_background[x, y] == '○')
                {
                    Get_Item(3, y);
                }
                else if (_background[x, y] == '※')
                {
                    Program.TutorialClear();
                    Console.Clear();
                    return 10;

                }
                return 0;
            }
            catch
            {
                return 1;
            }

        }
        public void ErrorPosSet(int x, int y)
        {
            prePosX = x;
            prePosY = y;
        }

        public void Ending(Player player)
        {
            while (true)
            {
                if (princess.Y + 1 - player.positionY != 0)
                {
                    player.positionY += princess.Y + 1 - player.positionY;
                    DrawChar(player.positionX, player.positionY, player.direction_right);
                    Thread.Sleep(100);
                }
                else if (princess.X - 30 - player.positionX != 0)
                {
                    player.positionX += -1;
                    princess.Print_Princess();
                    DrawChar(player.positionX, player.positionY, -1);
                    Thread.Sleep(125);
                }
                else
                {
                    Thread.Sleep(1000);
                    DrawChar(player.positionX, player.positionY, 1);
                    Thread.Sleep(400);
                    break;
                }

            }
            string[] text = princess.Text();
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < text.Length; i++)
            {
                Console.SetCursorPosition(110 - text[i].Length / 2, 12);
                sb2.Clear();
                for (int j = 0; j < text[i].Length + 2; j++)
                {
                    sb2.Append(' ');
                }
                Console.Write(sb2);
                Console.SetCursorPosition(111 - text[i].Length / 2, 12);
                foreach (char c in text[i])
                {
                    Console.Write(c);
                    Thread.Sleep(100);
                }
                Thread.Sleep(1500);
                if (i == text.Length - 1)
                {
                    Console.SetCursorPosition(111 - text[i].Length / 2, 12);
                    Console.Write("I love you.");
                    Thread.Sleep(1500);
                }
                Console.SetCursorPosition(111 - text[i].Length / 2, 12);
                Console.Write(sb2);
            }

            for (int i = 0; i < 15; i++)
            {
                player.positionX += 1;
                princess.moveleft();
                princess.Print_Princess();
                DrawChar(player.positionX, player.positionY, 1);
                if (i == 14)
                {
                    Console.SetCursorPosition(98, 16);
                    Console.Write("  ");
                }
                Thread.Sleep(250);
                if (i > 5) Thread.Sleep(150);
                if (i > 8) Thread.Sleep(100);
                if (i > 11)
                {
                    Thread.Sleep(100);
                }
                if (i > 13)
                {
                    Thread.Sleep(100);
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(1000);
            Console.SetCursorPosition(95, 13);
            Console.Write("♥");
            Thread.Sleep(1500);
            Console.Clear();
            princess.Print_Princess();
            DrawChar(player.positionX, player.positionY, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(95, 13);
            Console.Write("♥");
            Console.SetCursorPosition(93, 11);
            Console.Write("♥  ♥");
            Thread.Sleep(2000);
            Console.Clear();
            Console.SetCursorPosition(95, 13);
            Console.Write("♥");
            Console.SetCursorPosition(93, 11);
            Console.Write("♥  ♥");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(91, 9);
            Console.Write("♥  ♥  ♥");
            Thread.Sleep(3000);

            Console.ResetColor();
            Console.SetCursorPosition(88, 16);
            Console.Write("총 점프 횟수 : {0}",player.saveJump);
            Thread.Sleep(1500);
            Console.SetCursorPosition(0, 0);
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }
    }
}