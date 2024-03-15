using Project_jUMPKING;
using System;
using System.Numerics;

namespace Project_jUMPKING
{

    class Player
    {
        private double doubleX = 0, doubleY = 0;
        private double speedX = 2, speedY = 0;
        private double gravity = -0.01;
        private int _positionX, _positionY;
        private int prePosX, prePosY;
        private int _direction_right = 1; // 진행방향
        private int _power; // 점프 게이지
        private int term = 20; // 좌우 입력버퍼 지우기 위한 간격
        //디버깅용
        private int _saveX = 45, _saveY = 98, _saveDir = 0;
        public int positionX { get { return _positionX; } }
        public int positionY { get { return _positionY; } }
        public int direction_right { get { return _direction_right; } }
        public int power { get { return _power; } }
        public Player(int positionX = 80, int positionY = 220)
        {
            _positionX = positionX;
            _positionY = positionY;
        }

        public void Move(Background background)
        {
            while (true)
            {
                while (Console.KeyAvailable == false)
                {
                    Thread.Sleep(4);
                    PlayerCamera(_positionY);
                }
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        _direction_right = -1;
                        Move_LR(background);
                        break;
                    case ConsoleKey.RightArrow:
                        _direction_right = 1;
                        Move_LR(background);
                        break;
                    case ConsoleKey.Spacebar:
                        Power(background);
                        return;
                    case ConsoleKey.UpArrow:
                        _direction_right = 0;
                        background.DrawChar(_positionX, _positionY, _direction_right);
                        CameraTest();
                        break;
                    case ConsoleKey.DownArrow:
                        _direction_right = 0;
                        background.DrawChar(_positionX, _positionY, _direction_right);
                        break;
                    ///디버깅용///
                    case ConsoleKey.S:
                        _saveX = _positionX;
                        _saveY = _positionY;
                        _saveDir = _direction_right;
                        break;
                    case ConsoleKey.R:
                        _positionX = _saveX;
                        _positionY = _saveY;
                        _direction_right = _saveDir;
                        background.DrawChar(_positionX, _positionY, _direction_right);
                        break;
                }


            }
        }

        public void Move_LR(Background background)
        {
            if (background.Collide((int)_positionX, (int)_positionY, _direction_right) == 1) // 벽과 충돌 했을 때
            {              
            }
            else
            {
                _positionX += 1 * direction_right;
            }
            background.DrawChar(_positionX, _positionY, _direction_right);
            CalPos(background);
            while (Console.KeyAvailable)
                Console.ReadKey(false);
            //if (!background.Collide(_positionX, _positionY, _direction_right))//충돌검사//
            //{

            //    //while (!background.Collide(_positionX, _positionY, 4))
            //    //{
            //    //    _positionY += 1;
            //    //    background.DrawChar(_positionX, _positionY, _direction_right);
            //    //    Thread.Sleep(term);
            //    //    while (Console.KeyAvailable)
            //    //        Console.ReadKey(false);
            //    //}
            //}
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

                    Thread.Sleep(1);
                    buffer++; // 0. while문을 돌면서 입력을 놓칠경우 발생하는 횟수를 담아놓기 위한 변수
                    if (buffer > 5) // 3. buffer가 일정 횟수 이상 담기면 검사 (1 == Thred.Sleep 속도에 따른 임의의 수)
                    {
                        if (keyEnter) // 4-1. 입력이 존재하여 KeyUp = true로 변화했으면 KeyUp 이벤트 처리, 최초 입력지연 때 발생하는 오류제거
                        {
                            int temp = power;
                            keyEnter = false;
                            _power = power;
                            background.PowerBar(_positionY, 52);
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
                    background.DrawChar_charging(_positionX, _positionY, direction_right);
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Spacebar:
                        if (power <= 50) power++;
                        background.PowerBar(_positionY, power);
                        break;
                }
                buffer = 0; // 1. 입력이 있으면 buffer를 0으로 초기화
                keyEnter = true; // 2. 입력 대기상태로 변할 때 KeyUp이 발생하도록 변수 조절
                if (key.Key == ConsoleKey.Tab) { return; }
            }
        }

        public void CalPos(Background background)
        {
            int temp = _power;
            int colisionGround = 0;
            bool colisionTop = false;
            bool istouched = false;
            prePosX = _positionX;
            prePosY = _positionY;
            double doubleX = _positionX;
            double doubleY = _positionY;
            SetPower(temp);
            gravity = -0.03;
            while (colisionGround != 1)
            {
                if (power == 0)
                {
                    Thread.Sleep(3);
                }
                else
                {
                    Thread.Sleep(10);
                }
                doubleX += speedX * direction_right;
                doubleY -= speedY;
                if (speedY > -1)
                { speedY += gravity; }

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
                    PlayerCamera(_positionY);
                }

                try
                {
                    //충돌 검사
                    switch (background.Collide((int)_positionX + direction_right, (int)_positionY, _direction_right))
                    {
                        case 1:
                            _direction_right *= -1;
                            speedX *= 0.8;
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
                    }
                
                    //if (background.Collide((int)_positionX + direction_right, (int)_positionY, _direction_right) == 1) // 벽과 충돌 했을 때
                    //{
                    //    _direction_right *= -1;
                    //    speedX *= 0.8;
                    //}
                    colisionTop = background.Collide((int)_positionX, (int)_positionY, 3) == 1;
                    if (colisionTop) // 천장과 충돌 했을 때
                    {
                        if (!istouched)
                        {
                            istouched = true;
                            speedY *= -1;
                        }
                    }

                    colisionGround = background.Collide((int)_positionX, (int)_positionY, 4);
                    switch(colisionGround)
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
                    }
                }
                catch (IndexOutOfRangeException e) // 맵뚫 버그 발생시 맵 및 캐릭터 초기화
                {
                    _positionX = 45;
                    _positionY = 98;
                    background.DrawChar(_positionX, _positionY, _direction_right);
                    speedY = 0;
                    _power = 0;
                    return;
                }

                //PlayerCamera(_positionY);
                //Thread.Sleep(1);

            }
        }

        public void SetPower(int power)
        {
            if (power == 0)
            {
                speedX = 0;
                speedY = 0;
            }
            else if (power < 10)
            {
                speedX = 0.3;
                speedY = 0.2;
            }
            else if (power < 15)
            {
                speedX = 0.35;
                speedY = 0.25;
            }
            else if (power < 20)
            {
                speedX = 0.3;
                speedY = 0.2;
            }
            else if (power < 25)
            {
                speedX = 0.4;
                speedY = 0.3;
            }
            else if (power < 30)
            {
                speedX = 0.5;
                speedY = 0.4;
            }
            else if (power < 35)
            {
                speedX = 0.6;
                speedY = 0.5;
            }
            else if (power < 40)
            {
                speedX = 0.7;
                speedY = 0.6;
            }
            else if (power < 45)
            {
                speedX = 0.75;
                speedY = 0.75;
            }
            else if (power < 50)
            {
                speedX = 0.8;
                speedY = 0.9;
            }
            else
            {
                speedX = 0.8;
                speedY = 1.1;
            }
            if (direction_right == 0) speedX = 0;

        }


        public void PlayerCamera(int positionY)
        {
            int height = Background.height - 1;
            while (positionY < height)
            {
                height -= 62;
                continue;
            }
            if (height < 0)
            {
                Console.SetCursorPosition(0, 0);
            }
            else
            {
                Console.SetCursorPosition(0, height);
                Console.SetCursorPosition(0, height + 62);
            }
        }

        public void CameraTest()
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
                            PlayerCamera(_positionY);
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
                        PlayerCamera(_positionY - 62);
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