using System;
using System.Timers;

namespace Example1
{
    internal class Game
    {
        public static int Width { get { return 40; } }
        public static int Height { get { return 40; } }

        private Worm _worm = new Worm('*', ConsoleColor.Green);
        private Wall _wall = new Wall('#', ConsoleColor.Blue);
        private Food _food = new Food('$', ConsoleColor.Red);
        private readonly Timer _wormTimer = new Timer(115);
        private readonly Timer _gameTimer = new Timer(300);
        private bool _pause;
        public bool IsRunning { get; private set; }
        
        public Game()
        {
            _wormTimer.Elapsed += MoveWorm;
            _wormTimer.Start();
            _gameTimer.Elapsed += GameTimerElapsed;
            _gameTimer.Start();           
            _wall.LoadLevel();
            _pause = false;
            IsRunning = true;
            Console.CursorVisible = false;
            Console.SetWindowSize(Width, Width);
            Console.SetBufferSize(40, 40);     
        }

        private void GameTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.Title = DateTime.Now.ToLongTimeString() + " Scores: " + _worm.CountOfPoints;
        }

        public void UpdatePoints(object sender, ElapsedEventArgs e)
        {    
            Console.SetCursorPosition(25, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Eaten Food: {0}", _worm.CountOfPoints, Console.ForegroundColor);
        }

        private void MoveWorm(object sender, ElapsedEventArgs e)
        {
            _worm.Move();

            if (_worm.IsIntersected(_wall.Body))
            {
                Console.Clear();
                Console.SetCursorPosition(15, 20);
                Console.Write("Game over ;(");
                Console.SetCursorPosition(14, 23);
                Console.Write("Your Scores: {0}", _worm.CountOfPoints);
                Console.SetCursorPosition(10, 26);
                Console.Write("Load Saved Game? Press L");
                _wormTimer.Stop();
                _pause = true;
            }

            if (_worm.IsIntersected(_food.Body))
            {
                _worm.Increase(_worm.Body[0]);
                _food.GenerateLocation(_worm.Body, _wall.Body);
            }

            if (_worm.CountOfPoints == 3)
            {
                _wall.Clear();
                _wall.NextLevel();
            }
        }


        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    _worm.ChangeDirection(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    _worm.ChangeDirection(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    _worm.ChangeDirection(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    _worm.ChangeDirection(1, 0);
                    break;
                case ConsoleKey.S:
                    _worm.Save("save");
                    _wall.Save("wall");                  
                    break;
                case ConsoleKey.L:
                    Console.Clear();
                    _wormTimer.Stop();                   
                    _wall.Clear();
                    _worm.Clear();
                    _food.Clear();
                    _wall = Wall.Load("wall");
                    _worm = Worm.Load("save");                 
                    _food = new Food('$', ConsoleColor.Red);
                    if (_worm.CountOfPoints >= 3)
                    {
                        _wall.NextLevel();
                    }
                    else
                    {
                        _wall.LoadLevel();
                    }
                    _wormTimer.Start();
                    break;
                case ConsoleKey.Spacebar:
                    if (!_pause)
                    {
                        _wormTimer.Stop();
                        _pause = true;
                    }
                    else
                    {
                        _wormTimer.Start();
                        _pause = false;
                    }
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.SetCursorPosition(15, 20);
                    Console.WriteLine("Good Bye");
                    IsRunning = false;
                    break;
            }           
        }
    }
}

