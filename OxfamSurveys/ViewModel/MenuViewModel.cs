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
        private Excel excelFile = null;
        private ICommand _CreateCommand;
        public ICommand CreateCommand
        {
            get
            {
                return _CreateCommand ?? (
                    _CreateCommand = new RelayCommand(() =>
                    {
                        /*ExcelFile = new Excel("NutVal.xlsm", "Database");
                        List<Food> foods = excelFile.ReadData();
                        List<FoodAmount> foodamounts = new List<FoodAmount>();
                        foodamounts.Add(new FoodAmount(foods[5], 200));
                        Random rand = new Random();
                        for(int i = 0; i < 20; i++)
                        {
                            foodamounts.Add(new FoodAmount(foods[rand.Next(0, foods.Count-1)], rand.Next(5, 100)));
                        }
                        ExcelFile.SetWorkSheet("Calculation Sheet");
                        ExcelFile.WriteData(foodamounts);*/
                        ExcelFile = new Excel();
                        ExcelFile.ExcelApp.Visible = true;
                        ExcelFile.Workbook = ExcelFile.ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                        ExcelFile.Workbook.Worksheets.Add();
                        
                        try
                        {
                            ExcelFile.SetWorkSheet(2); // Compulsory Line in which sheet you want to write data
                                                                          
                            ExcelFile.Worksheet.Cells[1, "A"] = "Bro";
                            ExcelFile.Worksheet.Cells[2, "B"] = "Yolo";
                            ExcelFile.Worksheet.Cells[3, "C"] = "Pupu";

                            ExcelFile.Workbook.Worksheets[1].Name = "survey";
                            ExcelFile.Workbook.Worksheets[2].Name = "choices";
                            ExcelFile.Workbook.SaveAs(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"Test.xlsx");
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

        private ICommand _DownloadNutValCommand;
        public ICommand DownloadNutValCommand
        {
            get
            {
                return _DownloadNutValCommand ?? (
                    _DownloadNutValCommand = new RelayCommand(() =>
                    {
                        KoBoApi api = new KoBoApi("labopluri2017", "LaboM'enfrin");

                        foreach (var project in api.GetForms())
                        {
                            MessageBox.Show(project.Title);
                        }
                    })
                );
            }
        }

        internal Excel ExcelFile
        {
            get
            {
                return excelFile;
            }
            set
            {
                excelFile = value;
            }
        }
    }
}
