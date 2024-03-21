using Project_jUMPKING;
using System;
using System.Numerics;
using System.IO;

namespace Project_jUMPKING
{

    class Player
    {
        private double doubleX, doubleY;
        private double speedX = 2, speedY = 0;
        private int _positionX, _positionY; // 캐릭터의 현재 위치
        private int prePosX, prePosY; // 렌더링을 위한 이전 좌표 확인용 위치
        private int _direction_right = 1; // 진행방향
        private int _power; // 점프 게이지
        //private int term = 20; // 좌우 입력버퍼 지우기 위한 간격
        int max_height = 0, min_height = 0; // 카메라의 높이 최대 크기

        private static bool[] itemcheck = new bool[10]; // 아이템 체크
        private bool[] itemUse = new bool[10]; // 현재 사용중인 아이템
        private int now_Item = 0;
        private bool whilein = false;
        private bool snowy = false;
        private int time = 150;
        private int sand = 0;
        int calbuffer = 0;

        private bool glacier = false;
        private bool bSand = false;

        private int _saveX = 0, _saveY = 0, _saveDir = 0;
        public int positionX { get { return _positionX; } set { _positionX = value; } }
        public int positionY { get { return _positionY; } set { _positionY = value; } }
        public int direction_right { get { return _direction_right; } set { _direction_right = value; } }
        public int power { get { return _power; } }

        private int buffer;
        private int delay_buffer;

        public Player(int positionX = 40 * 2, int positionY = 611 - 3)
        {
            _positionX = positionX;
            _positionY = positionY;
            Array.Clear(itemcheck);
            Array.Clear(itemUse);
        }

        public static void ItemClear()
        {
            Array.Clear(itemcheck);
        }

        public void Move(Background background)
        {
            while (true)
            {
                while (Console.KeyAvailable == false)
                {
                    PlayerCamera(_positionY, background);
                    Thread.Sleep(1);
                    return;
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        _direction_right = -1;
                        Move_LR(background);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        _direction_right = 1;
                        Move_LR(background);
                        break;
                    case ConsoleKey.Spacebar:
                        Power(background);
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        _direction_right = 0;
                        background.DrawChar(_positionX, _positionY, _direction_right);
                        CameraTest(background);
                        break;
                    case ConsoleKey.Delete:
                        while (true)
                        {
                            key = Console.ReadKey(true);
                            switch (key.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    _positionY -= 1;
                                    background.DrawChar(_positionX, _positionY, _direction_right);
                                    break;
                                case ConsoleKey.Delete:
                                    return;
                                case ConsoleKey.LeftArrow:
                                    _positionX -= 1;
                                    background.DrawChar(_positionX, _positionY, _direction_right);
                                    break;
                                case ConsoleKey.RightArrow:
                                    _positionX += 1;
                                    background.DrawChar(_positionX, _positionY, _direction_right);
                                    break;
                                case ConsoleKey.DownArrow:
                                    _positionY += 1;
                                    background.DrawChar(_positionX, _positionY, _direction_right);
                                    break;
                            }
                        }
                    case ConsoleKey.PageDown:
                        if (glacier) glacier = false;
                        else glacier = true;
                        break;
                    case ConsoleKey.S:
                        if (!itemcheck[0]) break;
                        if (glacier) break;
                        if (bSand) break;
                        background.Save_Position(_saveX, _saveY, _positionX, _positionY);
                        _saveX = _positionX;
                        _saveY = _positionY;
                        _saveDir = _direction_right;

                        itemUse[0] = true;
                        break;
                    case ConsoleKey.R:
                        if (!itemUse[0]) break;
                        if (glacier) break;
                        _positionX = _saveX;
                        _positionY = _saveY;
                        _direction_right = _saveDir;
                        background.DrawChar(_positionX, _positionY, _direction_right);
                        break;
                    case ConsoleKey.D1:
                        UseItem(background, 1);
                        break;
                    case ConsoleKey.D2:
                        UseItem(background, 2);
                        break;
                    case ConsoleKey.D3:
                        UseItem(background, 3);
                        break;
                    case ConsoleKey.Escape:
                        int height = Background.height + 14;
                        whilein = true;

                        Console.SetCursorPosition(0, height);
                        Console.SetCursorPosition(0, height + 62);
                        menu(height, background);
                        break;
                    case ConsoleKey.End:
                        _positionX = 90;
                        _positionY = 15;
                        break;

                }


            }
        }
        public void menu(int height, Background background)
        {
            int cursorx = 106;
            int cursor = height + 29;
            Console.SetCursorPosition(cursorx, cursor + 2);
            Console.Write("  ");
            Console.SetCursorPosition(cursorx, cursor + 4);
            Console.Write("  ");
            Console.SetCursorPosition(cursorx, cursor);
            Console.Write('▶');
            while (whilein)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (cursor > height + 29)
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
                        if (cursor < height + 33)
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
                    case ConsoleKey.Escape:
                        whilein = false;
                        Console.SetCursorPosition(cursorx, height + 29);
                        Console.Write('▶');
                        Console.SetCursorPosition(cursorx, height + 31);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 33);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 35);
                        Console.Write("  ");
                        Console.SetCursorPosition(180, height + 31);
                        Console.Write("                    ");
                        cursor = height + 29;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        whilein = true;
                        if (!Program.get_Btutorial()) Save(height + 31);
                        Console.SetCursorPosition(cursorx, height + 29);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 31);
                        Console.Write('▶');
                        Console.SetCursorPosition(cursorx, height + 33);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 35);
                        Console.Write("  ");
                        cursor = height + 31;
                        Console.SetCursorPosition(cursorx, cursor);
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        whilein = false;
                        Console.SetCursorPosition(cursorx, height + 29);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 31);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 33);
                        Console.Write('▶');
                        Console.SetCursorPosition(cursorx, height + 35);
                        Console.Write("  ");
                        cursor = height + 33;
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        whilein = false;
                        Console.SetCursorPosition(cursorx, height + 29);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 31);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 33);
                        Console.Write("  ");
                        Console.SetCursorPosition(cursorx, height + 35);
                        Console.Write('▶');
                        Console.SetCursorPosition(180, height + 31);
                        Console.Write("                    ");
                        cursor = height + 35;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        whilein = false;
                        break;
                }
            }
            if (cursor == height + 29)
            {
                whilein = false;
            }
            else if (cursor == height + 31)
            {
                Save(height + 31);
                whilein = true;
                menu(height, background);
            }
            else if (cursor == height + 33)
            {
                Console.SetCursorPosition(0, height);
                Environment.Exit(0);
            }
            else if (cursor == height + 35)
            {
                Console.Clear();
                background.Print_Back();
                background.DrawChar(_positionX, _positionY, _direction_right);
            }
        }
        public void Save(int height)
        {
            Console.SetCursorPosition(180, height);

            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();
                CurrentDirectory = CurrentDirectory.Substring(0, CurrentDirectory.IndexOf("bin"));
                //Pass the filepath and filename to the StreamWriter Constructor
                string path = CurrentDirectory + "SaveData.txt";
                StreamWriter sw = new StreamWriter(path);
                //Write a line of text
                sw.WriteLine("_positionX {0}", _positionX);
                sw.WriteLine("_positionY {0}", _positionY);
                sw.WriteLine("_direction_right {0}", _direction_right);
                sw.WriteLine("itemcheck[0] {0}", itemcheck[0]);
                sw.WriteLine("itemcheck[1] {0}", itemcheck[1]);
                sw.WriteLine("itemcheck[2] {0}", itemcheck[2]);
                sw.WriteLine("itemcheck[3] {0}", itemcheck[3]);
                //Close the file
                sw.Close();
                Console.Write("저장되었습니다.");
            }
            catch (Exception e)
            {
                Console.WriteLine("관리자 권한으로 실행해주세요.");
                Console.WriteLine("Exception: " + e.Message);
            }

        }
        private void UseItem(Background background, int num)
        {
            if (!itemcheck[num]) return;
            if (itemUse[num])
            {
                itemUse[now_Item] = false;
                background.item_Dic[now_Item].OffItem();
                return;
            }
            if (now_Item != 0)
            {
                itemUse[now_Item] = false;
            }
            background.item_Dic[now_Item].OffItem();
            now_Item = num;
            itemUse[now_Item] = true;
            background.item_Dic[now_Item].OnItem();
            return;
        }
        public void Move_LR(Background background)
        {
            if (snowy == true)
            {
                background.DrawChar(_positionX, _positionY, _direction_right);
                return;
            }
            if (background.Collide((int)_positionX, (int)_positionY, _direction_right) != 1) // 벽과 충돌 하지 않았을 때
            {
                if (glacier)
                {
                    speedX += (0.03 * direction_right);
                }
                else
                {
                    _positionX += 1 * direction_right;
                }
                background.DrawChar(_positionX, _positionY, _direction_right);
            }
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        public void Power(Background background)
        {
            int buffer = 0;
            int power = 0;
            bool keyEnter = false;
            while (true)
            {
                while (Console.KeyAvailable == false)
                {
                    CalPos(background);
                    Thread.Sleep(2);
                    buffer++; // 0. while문을 돌면서 입력을 놓칠경우 발생하는 횟수를 담아놓기 위한 변수
                    if (buffer > 5) // 3. buffer가 일정 횟수 이상 담기면 검사 (1 == Thred.Sleep 속도에 따른 임의의 수)
                    {
                        if (keyEnter) // 4-1. 입력이 존재하여 KeyUp = true로 변화했으면 KeyUp 이벤트 처리, 최초 입력지연 때 발생하는 오류제거
                        {
                            int temp = power;
                            keyEnter = false;
                            _power = power;
                            background.UIBar(_positionY, 52);
                            return;
                        }
                        else // 4-2. 입력이 발생하지 않아 KeyUp = false로 유지되었으면 계속해서 대기상태로 유지
                        {
                            buffer = 0;
                        }
                    }
                }

                if (glacier)
                {
                    calbuffer++;
                    if (calbuffer % 2 == 0)
                    {
                        CalPos(background);
                        calbuffer += -2;
                    }
                }
                else
                {
                    if (power >= 1)
                    {
                        background.DrawChar_charging(_positionX, _positionY, direction_right);
                    }
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Spacebar:
                        if (power <= 50) power++;
                        background.UIBar(_positionY, power);
                        break;
                }
                buffer = 0; // 1. 입력이 있으면 buffer를 0으로 초기화
                keyEnter = true; // 2. 입력 대기상태로 변할 때 KeyUp이 발생하도록 변수 조절
            }
        }

        public void CalPos(Background background)
        {
            int temp = _power;
            int colisionGround = 0;
            bool istouched = false;

            prePosX = _positionX;
            prePosY = _positionY;
            double acc = 0.00;
            if (glacier)
            {
            }
            else
            {
                doubleX = _positionX;
            }
            doubleY = _positionY;

            if (bSand)
            {
                sand++;
                if (sand >= 25)
                {
                    _positionY++;
                    sand = 0;
                    background.DrawChar(_positionX, _positionY, _direction_right);
                }
            }
            SetPower(temp);
            double gravity = -0.03;
            while (colisionGround != 1)
            {                
                //Console.SetCursorPosition(_positionX, _positionY - 4);
                //Console.Write(time);
                if (positionY <= 176 && positionY >= 114)
                {
                    time++;
                    int seqeunce;
                    int sequence2;
                    int term = 20;
                    int term2 = 10;
                    if (time == 40 * term) time = 0;
                    seqeunce = time / term;
                    sequence2 = time / term2;
                    background.Draw_Storm(sequence2);
                    if(seqeunce < 10)
                    {

                    }
                    else if(seqeunce < 20) 
                    {
                        speedX += 0.01;
                    }
                    else if (seqeunce < 30)
                    {

                    }
                    else
                    {
                        speedX -= 0.01;
                    }
                    //Console.SetCursorPosition(_positionX, _positionY - 4);
                    //Console.Write(seqeunce);
                    if(direction_right == 0)
                    {
                        if(speedY > 0)
                        {
                            if (speedX > 0) { direction_right = 1; }
                            else if (speedX < 0) { direction_right = -1; }
                        }

                    }

                }

                background.Draw_Item(max_height, min_height, 2);
                if (power == 0)
                {
                    Thread.Sleep(3);
                }
                else
                {
                    Thread.Sleep(10);
                }
                if (glacier)
                {
                    if (speedX > 0.2 || speedX < -0.2)
                    {
                        doubleX += speedX + (acc * direction_right);
                    }
                    if (speedX > 0.7) speedX = 0.7;
                    else if (speedX < -0.7) speedX = -0.7;
                }
                else
                {
                    doubleX += speedX;
                }
                doubleY -= speedY;
                if (speedY > -1)
                { speedY += gravity; }
                if (itemUse[3])
                { speedY += 0.01; }

                if (prePosX != (int)Math.Round(doubleX))
                {
                    _positionX = (int)doubleX;
                    prePosX = (int)doubleX;
                    background.DrawChar(_positionX, _positionY, _direction_right);
                }
                if (prePosY != (int)(doubleY))
                {
                    _positionY = (int)doubleY;
                    prePosY = (int)doubleY;
                    background.DrawChar(_positionX, _positionY, _direction_right);
                    PlayerCamera(_positionY, background);
                }

                //충돌 검사
                switch (background.Collide((int)_positionX + direction_right, (int)_positionY, _direction_right)) //좌우 벽과 충돌 했을 때
                {
                    case 1:
                        _direction_right *= -1;
                        if (glacier)
                        {
                            speedX *= -0.8;
                        }
                        else
                        {
                            speedX = 0.8 * _direction_right;
                        }
                        break;
                    case 2:
                        _direction_right = -1;
                        if (glacier)
                        {
                            speedX *= -0.5;
                        }
                        else
                        {
                            speedX = 0.5;
                        }
                        break;
                    case 3:
                        _direction_right = 1;
                        if (glacier)
                        {
                            speedX *= -0.5;
                        }
                        else
                        {
                            speedX = 0.5;
                        }
                        break;
                    case 4:
                        _direction_right *= -1;
                        speedX *= 0.8;
                        break;
                    case 5:
                        _direction_right *= -1;
                        if (glacier)
                        {
                            speedX *= -0.8;
                        }
                        else
                        {
                            speedX = 0.8 * _direction_right;
                        }
                        break;
                }
                int collisiontop = background.Collide((int)_positionX, (int)_positionY, 3);
                if (collisiontop > 0 && collisiontop != 6) // 천장과 충돌했을 때
                {
                    if (!istouched) // 속도변환 한번만 일어나게 해주는 체크 bool, 해당 체크가 없으면 순식간에 가속됨
                    {
                        istouched = true;
                        speedY *= -1;
                    }
                }

                snowy = false;
                glacier = false;
                bSand = false;
                if (speedY <= 0)
                {
                    switch (background.Collide((int)_positionX, (int)_positionY, 4)) // 바닥과 충돌했을때
                    {
                        case 1:
                            speedY = 0;
                            _power = 0;
                            return;
                        case 2:
                            _direction_right = -1;
                            speedX = 0.5;
                            speedY = 0;
                            break;
                        case 3:
                            _direction_right = 1;
                            speedX = 0.5;
                            speedY = 0;
                            break;
                        case 4:
                            snowy = true;
                            speedY = 0;
                            _power = 0;
                            return;
                        case 5:
                            glacier = true;
                            speedY = 0;
                            _power = 53;
                            return;
                        case 6:
                            snowy = true;
                            bSand = true;
                            speedY = 0;
                            _power = 0;
                            return;
                    }
                }


            }
        }

        public void SetPower(int power)
        {
            //if (power == 0)
            //{
            //    speedX = 0;
            //    speedY = 0;
            //}
            if (power < 10)
            {
                speedX += 0.3 * _direction_right;
                speedY = 0.2;
            }
            else if (power < 15)
            {
                speedX += 0.35 * _direction_right;
                speedY = 0.25;
            }
            else if (power < 20)
            {
                speedX += 0.3 * _direction_right;
                speedY = 0.2;
            }
            else if (power < 25)
            {
                speedX += 0.4 * _direction_right;
                speedY = 0.3;
            }
            else if (power < 30)
            {
                speedX += 0.5 * _direction_right;
                speedY = 0.4;
            }
            else if (power < 35)
            {
                speedX += 0.6 * _direction_right;
                speedY = 0.5;
            }
            else if (power < 40)
            {
                speedX += 0.7 * _direction_right;
                speedY = 0.6;
            }
            else if (power < 45)
            {
                speedX += 0.75 * _direction_right;
                speedY = 0.75;
            }
            else if (power <= 50)
            {
                speedX += 0.8 * _direction_right;
                speedY = 0.9;
            }
            else if (power == 51)
            {
                speedX += 0.8 * _direction_right;
                speedY = 1.1;
            }
            if (itemUse[1]) speedX += 0.6 * direction_right;
            else if (itemUse[2]) speedY += 0.4;
            if (power == 0)
            {
                speedX = 0;
                speedY = 0;
            }
            else if (power == 53)
            {
                if (!glacier)
                {
                    speedX = 0;
                }
                speedY = 0;
            }
            if (direction_right == 0) speedX = 0;

        }

        public static void Get_item(int itemNum)
        {
            itemcheck[itemNum] = true;
        }

        public void PlayerCamera(int positionY, Background background)
        {
            int height = Background.height - 1;
            while (positionY < height)
            {
                height -= 62;
                continue;
            }
            if (min_height != height)
            {
                min_height = height;
                max_height = height + 62;
                for (int i = 0; i < 10; i++)
                {
                    if (itemcheck[i])
                    {
                        if (i == 0) { continue; }
                        if (height < 0) { background.item_Dic[i].Set_posY(49); }
                        else { background.item_Dic[i].Set_posY(height + 49); }

                        if (itemUse[i]) background.item_Dic[i].OnItem();
                        else background.item_Dic[i].OffItem();
                    }
                }
                if (height < 0)
                {
                    background.Princess.Print_Princess();
                }
            }
            if (height < 0)
            {
                min_height = 0;
                max_height = 62;
                Console.SetCursorPosition(0, 0);
            }
            else
            {


                Console.SetCursorPosition(0, height);
                Console.SetCursorPosition(0, height + 62);
            }
        }

        public void CameraTest(Background background)
        {
            int buffer = 0;
            int power = 0;
            bool keyEnter = false;
            while (true)
            {
                while (Console.KeyAvailable == false)
                {

                    Thread.Sleep(1);
                    buffer++; // 0. while문을 돌면서 입력을 놓칠경우 발생하는 횟수를 담아놓기 위한 변수
                    if (buffer > 5) // 3. buffer가 일정 횟수 이상 담기면 검사 (1 == Thred.Sleep 속도에 따른 임의의 수)
                    {
                        if (keyEnter) // 4-1. 입력이 존재하여 KeyUp = true로 변화했으면 KeyUp 이벤트 처리, 최초 입력지연 때 발생하는 오류제거
                        {
                            keyEnter = false;
                            PlayerCamera(_positionY, background);
                            return;
                        }
                        else // 4-2. 입력이 발생하지 않아 KeyUp = false로 유지되었으면 계속해서 대기상태로 유지
                        {
                            buffer = 0;
                        }
                    }
                }
                if (power == 1)
                {
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        PlayerCamera(_positionY - 62, background);
                        power++;
                        break;
                }
                buffer = 0; // 1. 입력이 있으면 buffer를 0으로 초기화
                keyEnter = true; // 2. 입력 대기상태로 변할 때 KeyUp이 발생하도록 변수 조절
                if (key.Key == ConsoleKey.Tab) { return; }
            }
        }
    }
}