using System;
using System.Collections.Generic;
using System.IO;

namespace OxfamSurveys.Models
{
    class FoodList
    {
        private string filepath;

        public FoodList(string filepath = null)
        {
            if (filepath == null)
            {
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                filepath = Path.Combine(Path.GetDirectoryName(path), "database.txt");
            }

            this.filepath = filepath;
        }

        public List<Food> Get()
        {
            List<Food> food;
            try
            {
                food = ReadFile();
                if (food.Count == 0)
                {
                    throw new Exception("Empty file");
                }
            }
            catch (Exception)
            {
                Excel excel = new Excel();
                food = excel.ExcelData(filepath);
                Set(food);
            }

            return food;
        }

        public void Set(List<Food> food)
        {
            using (StreamWriter file = new StreamWriter(filepath))
            {
                foreach (Food item in food)
                {
                    file.WriteLine(item.Type + "|" + item.Name);
                }
            }
        }

        private List<Food> ReadFile()
        {
            string[] lines = File.ReadAllLines(filepath);
            List<Food> data = new List<Food>();

            foreach (string line in lines)
            {
                string type = line.Substring(0, line.IndexOf("|"));
                string name = line.Substring(line.IndexOf("|") + 1, (line.Length - 1) - line.IndexOf("|"));
                data.Add(new Food(name, type));
            }

            return data;
        }
    }
}
