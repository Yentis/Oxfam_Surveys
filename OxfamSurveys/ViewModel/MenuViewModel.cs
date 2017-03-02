using GalaSoft.MvvmLight.Command;
using Microsoft.Office.Interop.Excel;
using OxfamSurveys.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class MenuViewModel
    {
        private Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        private Workbook workbook = null;
        private ICommand _CreateCommand;
        public ICommand CreateCommand
        {
            get
            {
                return _CreateCommand ?? (
                    _CreateCommand = new RelayCommand(() =>
                    {
                        /*_Worksheet worksheet = LoadFile("NutVal.xlsm", "Database");
                        List<Food> foods = ReadData(worksheet);
                        List<FoodAmount> foodamounts = new List<FoodAmount>();
                        foodamounts.Add(new FoodAmount(foods[5], 200));
                        Random rand = new Random();
                        for(int i = 0; i < 20; i++)
                        {
                            foodamounts.Add(new FoodAmount(foods[rand.Next(0, foods.Count-1)], rand.Next(5, 100)));
                        }
                        worksheet = (Worksheet)excelApp.Worksheets["Calculation Sheet"];
                        WriteData(worksheet, foodamounts);
                        excelApp.Visible = true;*/
                        excelApp.Visible = true;
                        workbook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                        workbook.Worksheets.Add();
                        Worksheet excelWorkSheet = null;

                        try
                        {
                            excelWorkSheet = workbook.Worksheets[2]; // Compulsory Line in which sheet you want to write data
                                                                          
                            excelWorkSheet.Cells[1, "A"] = "Bro";
                            excelWorkSheet.Cells[2, "B"] = "Yolo";
                            excelWorkSheet.Cells[3, "C"] = "Pupu";

                            workbook.Worksheets[1].Name = "survey";
                            workbook.Worksheets[2].Name = "choices";
                            workbook.SaveAs(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"Test.xlsx");
                        }
                        catch (Exception exHandle)
                        {
                            Console.WriteLine("Exception: " + exHandle.Message);
                            Console.ReadLine();
                        }
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

        public Workbook Workbook
        {
            get
            {
                return workbook;
            }
        }

        private List<Food> ReadData(_Worksheet sheet)
        {
            List<Food> food = new List<Food>();
            int i = 12;

            while((sheet.Cells[i, "C"] as Range).Value != null)
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

            if((sheet.Cells[i, "C"] as Range).Value != null)
            {
                Range foodNames = sheet.get_Range(sheet.Cells[i, "C"], sheet.Cells[17, "C"]);
                Range rationAmounts = sheet.get_Range(sheet.Cells[i, "F"], sheet.Cells[17, "F"]);
                foodNames = null;
                rationAmounts = null;
            }
            
            if (foodnames.Count > 20)
            {
                MessageBox.Show("Sorry! There is a maximum of 20 foods.");
               
            } else
            {

                for (int f = 9; f < foodnames.Count - 1; f++)
                {
                    excelApp.Run("AddRow");
                }
                for (int j = 0; j < foodnames.Count; j++)
                {
                    sheet.Cells[i, "C"] = foodnames[j].Food.Name;
                    sheet.Cells[i, "F"] = foodnames[j].Amount;
                    i++;
                }
            }
        }

        private _Worksheet LoadFile(string location, string sheettoread)
        {
            string workbookPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            workbookPath = System.IO.Path.GetDirectoryName(workbookPath) + "\\Excel\\" + location;
            workbook = excelApp.Workbooks.Open(workbookPath);

            /*foreach (Worksheet worksheet in workbook.Worksheets)
            {
                Console.WriteLine(worksheet.Name);
            }*/

            return (Worksheet)excelApp.Worksheets[sheettoread];
        }
    }
}
