using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

/*
 * 
 * /!\ WE NEED TO CLOSE COM OBJECTS
 * And there's no need to systematically create an Excel Application.
 * We may not need it, for example in ReadData()
 * 
 */

namespace OxfamSurveys.Models
{
    class Excel
    {
        private Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        private Workbook workbook = null;
        private _Worksheet worksheet = null;
        private Workbooks workbooks = null;
        private Sheets worksheets = null;

        public Excel(string location)
        {
            LoadFile(location);
        }

        public Excel()
        {}
        
        public List<Food> ExcelData(string filePath)
        {
            worksheet = (Worksheet)excelApp.Worksheets["Database"];

            File.WriteAllText(filePath, string.Empty);
            List<Food> food = new List<Food>();
            int i = 12;

            while ((worksheet.Cells[i, "C"] as Range).Value != null)
            {
                var foodtype = (string)(worksheet.Cells[i, "C"] as Range).Value;
                var foodname = (string)(worksheet.Cells[i, "D"] as Range).Value;
                food.Add(new Food(foodname, foodtype));

                i++;
            }

            return food;
        }

        public void ReleaseObjects()
        {
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(worksheets);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(workbooks);
            Marshal.ReleaseComObject(excelApp);
        }

        public void WriteData(List<FoodAmount> foodnames, string formname)
        {
            int i = 8;

            worksheet = (Worksheet)excelApp.Worksheets["Calculation Sheet"];
            worksheet.Unprotect("cich");

            if ((worksheet.Cells[i, "C"] as Range).Value != null)
            {
                Range foodNames = worksheet.get_Range(worksheet.Cells[i, "C"], worksheet.Cells[17, "C"]);
                Range rationAmounts = worksheet.get_Range(worksheet.Cells[i, "F"], worksheet.Cells[17, "F"]);
                foodNames = null;
                rationAmounts = null;
            }

            if (foodnames.Count > 20)
            {
                MessageBox.Show("Sorry! There is a maximum of 20 foods.");

            }
            else
            {
                worksheet.Cells[54, "D"] = formname;
                worksheet.Cells[56, "D"] = DateTime.Now;

                for (int f = 9; f < foodnames.Count - 1; f++)
                {
                    excelApp.Run("AddRow");
                }
                for (int j = 0; j < foodnames.Count; j++)
                {
                    worksheet.Cells[i, "C"] = foodnames[j].Food.Name;
                    worksheet.Cells[i, "F"] = foodnames[j].Amount;
                    i++;
                }
            }

            excelApp.Visible = true;
        }

        public void LoadFile(string location)
        {
            string workbookPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            workbookPath = Path.GetDirectoryName(workbookPath) + "\\Excel\\" + location;
            workbooks = excelApp.Workbooks;
            workbook = excelApp.Workbooks.Open(workbookPath);
            worksheets = excelApp.Worksheets;

            /*foreach (Worksheet worksheet in workbook.Worksheets)
            {
                Console.WriteLine(worksheet.Name);
            }*/
        }
    }
}
