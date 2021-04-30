using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Example1
{
    public class Worm : GameObject
    {
        public int Dx { get; set; }
        public int Dy { get; set; }

        public Worm() : base()
        {

        }

        public Worm(char sign, ConsoleColor color) : base(sign, color)
        {
            var head = new Point { X = 20, Y = 20 };
            Body = new List<Point> {head};
            Draw();
            LengthOfWorm = 1;
            CountOfPoints = 0;
        }

        public void ChangeDirection(int dx, int dy)
        {
            this.Dx = dx;
            this.Dy = dy;
        }

        public void Move()
        {
            Clear();
            for (var i = Body.Count - 1; i > 0; i--)
            {
                Body[i].X = Body[i - 1].X;
                Body[i].Y = Body[i - 1].Y;
            }
            Body[0].X += Dx;
            Body[0].Y += Dy;
            Draw();
        }

        public void Increase(Point point)
        {
            Body.Add(new Point { X = point.X, Y = point.Y });
            LengthOfWorm++;
            CountOfPoints++;
        }

        public int LengthOfWorm { get; set; }

        public int CountOfPoints { get; set; }

        public void Save(string title)
        {
            if (File.Exists("save.xml"))
            {
                File.Delete("save.xml");
            }
            using FileStream fs = new FileStream(title + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worm));
            xmlSerializer.Serialize(fs, this);
        }

        public static Worm Load(string title)
        {
            using FileStream fs = new FileStream(title + ".xml", FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worm));
            Worm save = xmlSerializer.Deserialize(fs) as Worm;
            return save;
        }
    }
}

