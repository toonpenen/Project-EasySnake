using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Example1
{
    public class Wall : GameObject
    {
        private enum GameLevel
        {
            First,
            Second
        }

        private GameLevel _gameLevel = GameLevel.First;

        public Wall() : base()
        {

        }

        public Wall(char sign, ConsoleColor color) : base(sign, color)
        {
            Body = new List<Point>();
        }

        public void LoadLevel()
        {
            Body = new List<Point>();
            var levelName = @"Levels/Level1.txt";
            if (_gameLevel == GameLevel.Second)
            {
                levelName = @"Levels/Level2.txt";
            }  
            using (FileStream fs = new FileStream(levelName, FileMode.Open, FileAccess.Read))
            {            
                using StreamReader reader = new StreamReader(fs);
                var rowNumber = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    for (var columnNumber = 0; columnNumber < line.Length; columnNumber++)
                    {
                        if (line[columnNumber] == '#')
                        {
                            Body.Add(new Point { X = columnNumber, Y = rowNumber });
                        }
                    }
                    rowNumber++;
                }
            }
            Draw();
        }
        public void NextLevel()
        {
            
            if (_gameLevel == GameLevel.First)
            {
                _gameLevel = GameLevel.Second;
            }
            LoadLevel();
        }

        public void Save(string title)
        {
            if (File.Exists("wall.xml"))
            {
                File.Delete("wall.xml");
            }
            using FileStream fs = new FileStream(title + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Wall));
            xmlSerializer.Serialize(fs, this);
        }

        public static Wall Load(string title)
        {
            using FileStream fs = new FileStream(title + ".xml", FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Wall));
            Wall save = xmlSerializer.Deserialize(fs) as Wall;
            return save;
        }
    }
}

