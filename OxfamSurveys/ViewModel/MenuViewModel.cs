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
        private Excel excelFile = new Excel();
        private ICommand _CreateCommand;
        public ICommand CreateCommand
        {
            get
            {
                return _CreateCommand ?? (
                    _CreateCommand = new RelayCommand(() =>
                    {
                        /*ExcelFile = new Excel("NutVal.xlsm", "Database");
                        List<Food> food = excelFile.ReadData();
                        List<FoodAmount> foodamounts = new List<FoodAmount>();
                        //foodamounts.Add(new FoodAmount(foods[5], 200));
                        Random rand = new Random();
                        for(int i = 0; i < 20; i++)
                        {
                            foodamounts.Add(new FoodAmount(foods[rand.Next(0, foods.Count-1)], rand.Next(5, 100)));
                        }
                        ExcelFile.SetWorkSheet("Calculation Sheet");
                        ExcelFile.WriteData(foodamounts);*/
                        List<string> food = new List<string>
                        {
                            "Crops from own production", "Product from own livestock", "Wild food",
                            "Purchase", "Payment in kind", "Gift/loan of food", "ANIMAL FAT",
                            "APPLE JUICE, NO ADDED VITAMIN C", "APPLES", "APRICOTS, DRIED",
                            "AVOCADO PEAR", "BANANA", "BARLEY, DEHULLED", "BASIL, DRIED",
                            "BEANS, BLACK", "BEANS, DRIED", "BEANS, GREAT NORTHERN", "BEANS, KIDNEY, ALL TYPES",
                            "BEANS, NAVY (PEA BEANS)", "BEANS, PINK", "BEANS, PINTO", "BEANS, SOYA", "BEEF LIVER",
                            "BEEF, MODERATELY FAT", "BP-5™", "BREAD, MADE FROM WHEAT", "BREASTMILK, HUMAN, MATURE",
                            "FLOUR, WHOLE", "GRAIN", "WHEAT", "WHEAT, FORTIFIED, [USAID]"
                        };

                        ExcelFile = new Excel();
                        ExcelFile.Workbook = ExcelFile.ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                        ExcelFile.Workbook.Worksheets.Add();
                        ExcelFile.Workbook.Worksheets[1].Name = "survey";
                        ExcelFile.Workbook.Worksheets[2].Name = "choices";

                        try
                        {
                            ExcelFile.SetWorkSheet(1); // Compulsory Line in which sheet you want to write data
                                                                          
                            ExcelFile.Worksheet.Cells[1, "A"] = "type";
                            ExcelFile.Worksheet.Cells[1, "B"] = "name";
                            ExcelFile.Worksheet.Cells[1, "C"] = "label";
                            ExcelFile.Worksheet.Cells[1, "D"] = "appearance";
                            ExcelFile.Worksheet.Cells[1, "E"] = "required";

                            ExcelFile.Worksheet.Cells[2, "A"] = "begin repeat";
                            ExcelFile.Worksheet.Cells[2, "B"] = "nutval";
                            ExcelFile.Worksheet.Cells[2, "C"] = "Food";
                            ExcelFile.Worksheet.Cells[2, "D"] = "field-list";

                            ExcelFile.Worksheet.Cells[3, "A"] = "select_one food";
                            ExcelFile.Worksheet.Cells[3, "B"] = "food";
                            ExcelFile.Worksheet.Cells[3, "C"] = "Select a food item";
                            ExcelFile.Worksheet.Cells[3, "D"] = "minimal";
                            ExcelFile.Worksheet.Cells[3, "E"] = "VRAI";

                            ExcelFile.Worksheet.Cells[4, "A"] = "decimal";
                            ExcelFile.Worksheet.Cells[4, "B"] = "quantity";
                            ExcelFile.Worksheet.Cells[4, "C"] = "Quantity";
                            ExcelFile.Worksheet.Cells[4, "E"] = "VRAI";

                            ExcelFile.Worksheet.Cells[5, "A"] = "select_one origin";
                            ExcelFile.Worksheet.Cells[5, "B"] = "origin";
                            ExcelFile.Worksheet.Cells[5, "C"] = "Origin";
                            ExcelFile.Worksheet.Cells[5, "E"] = "VRAI";

                            ExcelFile.Worksheet.Cells[6, "A"] = "end repeat";

                            ExcelFile.SetWorkSheet(2);

                            ExcelFile.Worksheet.Cells[1, "A"] = "list_name";
                            ExcelFile.Worksheet.Cells[1, "B"] = "name";
                            ExcelFile.Worksheet.Cells[1, "C"] = "label";

                            int index = 2;

                            for(int i = 0; i < food.Count; i++)
                            {
                                if(index < 8)
                                {
                                    ExcelFile.Worksheet.Cells[index, "A"] = "origin";
                                    ExcelFile.Worksheet.Cells[index, "B"] = index - 1;
                                }
                                else
                                {
                                    ExcelFile.Worksheet.Cells[index, "A"] = "food";
                                    ExcelFile.Worksheet.Cells[index, "B"] = index - 7;
                                }
                                ExcelFile.Worksheet.Cells[index, "C"] = food[i];
                                index++;
                            }

                            /*int index = 2;

                            foreach (Food product in food)
                            {
                                ExcelFile.Worksheet.Cells[index, "A"] = "food_list";
                                ExcelFile.Worksheet.Cells[index, "B"] = product.Type;
                                ExcelFile.Worksheet.Cells[index, "C"] = product.Name;
                                index++;
                            }*/

                            ExcelFile.Workbook.SaveAs(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"Test2.xlsx");
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

                        foreach (FormLine line in api.GetData("87035").Lines)
                        {
                            MessageBox.Show(line.Food + ": " + line.Amount + " - " + line.Origin);
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
