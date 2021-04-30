using System;
using System.Collections.Generic;
using System.Linq;


namespace Example1
{
    public class Food : GameObject
    {
        private readonly Random _random = new Random();
        public Food(char sign, ConsoleColor color) : base(sign, color)
        {
            var location = new Point { X = _random.Next(2, Game.Width - 2), Y = _random.Next(2, Game.Height - 2) };
            Body.Add(location);
            Draw();
        }

        public void GenerateLocation(List<Point> wormBody, List<Point> wallBody)
        {
            Body.Clear();
            var random = new Random();
            var p = new Point { X = random.Next(2, 38), Y = random.Next(2, 38) };
            while (!IsAvailablePoint(p, wormBody) || !IsAvailablePoint(p, wallBody))
            {
                p = new Point { X = random.Next(2, 38), Y = random.Next(2, 38) };
            }
            Body.Add(p);
            Draw();
        }

        private static bool IsAvailablePoint(Point p, List<Point> points)
        {
            if (points == null) throw new ArgumentNullException(nameof(points));
            return points.All(t => p.X != t.X && p.Y != t.Y);
        }
    }
}

