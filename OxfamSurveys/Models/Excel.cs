using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace OxfamSurveys.Models
{
    class Excel
    {
        private Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        private Workbook workbook = null;
        private _Worksheet worksheet = null;

        public Excel(string location, string sheettoread)
        {
            Worksheet = LoadFile(location, sheettoread);
        }

        public Excel()
        {}

        public Microsoft.Office.Interop.Excel.Application ExcelApp
        {
            get
            {
                return excelApp;
            }
        }

        public Workbook Workbook
        {
            get
            {
                return workbook;
            }

            set
            {
                workbook = value;
            }
        }

        public _Worksheet Worksheet
        {
            get
            {
                return worksheet;
            }
            set
            {
                worksheet = value;
            }
        }

        public List<Food> ReadData()
        {
            string filePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            filePath = Path.GetDirectoryName(filePath) + "\\database.txt";
            List<Food> data = null;

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length < 2)
                {
                    data = ExcelData(filePath);
                } else
                {
                    data = new List<Food>();
                    foreach (string line in lines)
                    {
                        string type = line.Substring(0, line.IndexOf("|"));
                        string name = line.Substring(line.IndexOf("|") + 1, (line.Length - 1) - line.IndexOf("|"));
                        data.Add(new Food(name, type));
                    }
                }
                
                return data;
            }
            catch (FileNotFoundException)
            {
                data = ExcelData(filePath);
                return data;
            }
        }

        public List<Food> ExcelData(string filePath)
        {
            File.WriteAllText(filePath, string.Empty);
            List<Food> food = new List<Food>();
            int i = 12;

            while ((Worksheet.Cells[i, "C"] as Range).Value != null)
            {
                var foodtype = (string)(Worksheet.Cells[i, "C"] as Range).Value;
                var foodname = (string)(Worksheet.Cells[i, "D"] as Range).Value;
                food.Add(new Food(foodname, foodtype));

                i++;
            }

            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (Food item in food)
                {
                    file.WriteLine(item.Type + "|" + item.Name);
                }
            }

            return food;
        }

        public void WriteData(List<FoodAmount> foodnames)
        {
            int i = 8;

            if ((Worksheet.Cells[i, "C"] as Range).Value != null)
            {
                Range foodNames = Worksheet.get_Range(Worksheet.Cells[i, "C"], Worksheet.Cells[17, "C"]);
                Range rationAmounts = Worksheet.get_Range(Worksheet.Cells[i, "F"], Worksheet.Cells[17, "F"]);
                foodNames = null;
                rationAmounts = null;
            }

            if (foodnames.Count > 20)
            {
                MessageBox.Show("Sorry! There is a maximum of 20 foods.");

            }
            else
            {

                for (int f = 9; f < foodnames.Count - 1; f++)
                {
                    ExcelApp.Run("AddRow");
                }
                for (int j = 0; j < foodnames.Count; j++)
                {
                    Worksheet.Cells[i, "C"] = foodnames[j].Food.Name;
                    Worksheet.Cells[i, "F"] = foodnames[j].Amount;
                    i++;
                }
            }
        }

        public void SetWorkSheet(string worksheet)
        {
            Worksheet = (Worksheet)ExcelApp.Worksheets[worksheet];
        }

        public void SetWorkSheet(int worksheet)
        {
            Worksheet = (Worksheet)ExcelApp.Worksheets[worksheet];
        }

        public _Worksheet LoadFile(string location, string sheettoread)
        {
            string workbookPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            workbookPath = Path.GetDirectoryName(workbookPath) + "\\Excel\\" + location;
            Workbook = ExcelApp.Workbooks.Open(workbookPath);

            /*foreach (Worksheet worksheet in workbook.Worksheets)
            {
                Console.WriteLine(worksheet.Name);
            }*/

            return (Worksheet)ExcelApp.Worksheets[sheettoread];
        }
    }
}
