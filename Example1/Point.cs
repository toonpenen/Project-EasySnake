namespace Example1
{
    public class Point
    {
        private int _x;
        private int _y;
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value < 0)
                {
                    _x = Game.Width - 1;
                }
                else if (value >= Game.Width)
                {
                    _x = 0;
                }
                else
                {
                    _x = value;
                }
            }
        }
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value < 0)
                {
                    _y = Game.Width - 1;
                }
                else if (value >= Game.Width)
                {
                    _y = 0;
                }
                else
                {
                    _y = value;
                }
            }
        }
    }
}

