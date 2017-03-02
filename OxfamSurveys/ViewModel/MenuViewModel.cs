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
        #region Commands
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

                        Excel excelFile = new Excel("NutVal.xlsm", "Database");
                        List<Food> food = excelFile.ReadData();

                        var formExcel = new Microsoft.Office.Interop.Excel.Application();
                        Workbooks formWorkbooks = formExcel.Workbooks;
                        Workbook formWorkbook = formWorkbooks.Add(XlWBATemplate.xlWBATWorksheet);
                        Sheets formWorksheets = formWorkbook.Worksheets;
                        formWorksheets.Add();

                        Worksheet groupSheet = formWorksheets[1];
                        Worksheet listSheet = formWorksheets[2];

                        groupSheet.Name = "survey";
                        groupSheet.Name = "choices";

                        try
                        {
                            // Create group
                            groupSheet.Cells[1, "A"] = "type";
                            groupSheet.Cells[1, "B"] = "name";
                            groupSheet.Cells[1, "C"] = "label";
                            groupSheet.Cells[1, "D"] = "appearance";
                            groupSheet.Cells[1, "E"] = "required";

                            groupSheet.Cells[2, "A"] = "begin repeat";
                            groupSheet.Cells[2, "B"] = "nutval";
                            groupSheet.Cells[2, "C"] = "Food";
                            groupSheet.Cells[2, "D"] = "field-list";

                            groupSheet.Cells[3, "A"] = "select_one food";
                            groupSheet.Cells[3, "B"] = "food";
                            groupSheet.Cells[3, "C"] = "Select a food item";
                            groupSheet.Cells[3, "D"] = "minimal";
                            groupSheet.Cells[3, "E"] = "VRAI";

                            groupSheet.Cells[4, "A"] = "decimal";
                            groupSheet.Cells[4, "B"] = "quantity";
                            groupSheet.Cells[4, "C"] = "Quantity";
                            groupSheet.Cells[4, "E"] = "VRAI";

                            groupSheet.Cells[5, "A"] = "select_one origin";
                            groupSheet.Cells[5, "B"] = "origin";
                            groupSheet.Cells[5, "C"] = "Origin";
                            groupSheet.Cells[5, "E"] = "VRAI";

                            groupSheet.Cells[6, "A"] = "end repeat";

                            // Set lists
                            listSheet.Cells[1, "A"] = "list_name";
                            listSheet.Cells[1, "B"] = "name";
                            listSheet.Cells[1, "C"] = "label";

                            int index = 2;

                            for (int i = 0; i < Origins.Count(); i++)
                            {
                                listSheet.Cells[index, "A"] = "origin";
                                listSheet.Cells[index, "B"] = i;
                                listSheet.Cells[index, "C"] = Origins.GetById(i);
                                index++;
                            }

                            for (int i = 0; i < food.Count; i++)
                            {
                                listSheet.Cells[index, "A"] = "food";
                                listSheet.Cells[index, "B"] = i;
                                listSheet.Cells[index, "C"] = food[i].Name;
                                index++;
                            }

                            formWorkbook.SaveAs(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "/Test2.xlsx");
                            formWorkbook.Close();
                            formExcel.Quit();

                            // Release all COM objects
                            Marshal.ReleaseComObject(groupSheet);
                            Marshal.ReleaseComObject(listSheet);
                            Marshal.ReleaseComObject(formWorksheets);
                            Marshal.ReleaseComObject(formWorkbook);
                            Marshal.ReleaseComObject(formWorkbooks);
                            Marshal.ReleaseComObject(formExcel);

                            MessageBox.Show("File creation complete", "Success!");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Exception: " + e.Message, "Error");
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
        #endregion
    }
}
