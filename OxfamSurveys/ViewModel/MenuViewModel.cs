using GalaSoft.MvvmLight.Command;
using Microsoft.Office.Interop.Excel;
using OxfamSurveys.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class MenuViewModel
    {
        private Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        private ICommand _CreateCommand;
        public ICommand CreateCommand
        {
            get
            {
                return _CreateCommand ?? (
                    _CreateCommand = new RelayCommand(() =>
                    {
                        _Worksheet worksheet = LoadFile("NutVal.xlsm", "Database");
                        List<Food> foods = ReadData(worksheet);
                        MessageBox.Show(foods.Count.ToString());
                        List<FoodAmount> foodamounts = new List<FoodAmount>();
                        //WriteData(excelApp, worksheet, foodamounts);
                        excelApp.Visible = true;
                    })
                );
            }
        }

        public Microsoft.Office.Interop.Excel.Application ExcelApp
        {
            get
            {
                return excelApp;
            }
        }

        private List<Food> ReadData(_Worksheet sheet)
        {
            List<Food> food = new List<Food>();
            int i = 12;

            while((string)(sheet.Cells[i, "C"] as Range).Value != null)
            {
                var foodtype = (string)(sheet.Cells[i, "C"] as Range).Value;
                var foodname = (string)(sheet.Cells[i, "D"] as Range).Value;
                food.Add(new Food(foodname, foodtype));

                i++;
            }

            return food;
        }

        private void WriteData(_Worksheet sheet, List<FoodAmount> foodnames)
        {
            int i = 8;
            
            if (foodnames.Count> 9 && foodnames.Count<=20)
            {
                for (int f = 9; f < foodnames.Count; f++)
                {
                    excelApp.Run("AddRow");
                }
            } else if (foodnames.Count > 20)
            {
                throw new IndexOutOfRangeException();
            }

            for (int j = 0; j < foodnames.Count; j++)
            {
                sheet.Cells[i, "C"] = foodnames[j].Food.Name;
                sheet.Cells[i, "F"] = foodnames[j].Amount;
                i++;
            }
        }

        private _Worksheet LoadFile(string location, string sheettoread)
        {
            string workbookPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            workbookPath = System.IO.Path.GetDirectoryName(workbookPath) + "\\Excel\\" + location;
            var workbook = excelApp.Workbooks.Open(workbookPath);

            /*foreach (Worksheet worksheet in workbook.Worksheets)
            {
                Console.WriteLine(worksheet.Name);
            }*/

            return (Worksheet)excelApp.Worksheets[sheettoread];
        }
    }
}
