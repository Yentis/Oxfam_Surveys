using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace OxfamSurveys.Models
{
    class XLSForm
    {
        public string Generate(List<Food> food, string filename = "xlsform")
        {
            string path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName),
                filename + ".xlsx");

            var formExcel = new Application();
            Workbooks formWorkbooks = formExcel.Workbooks;
            Workbook formWorkbook = formWorkbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Sheets formWorksheets = formWorkbook.Worksheets;
            formWorksheets.Add();

            Worksheet groupSheet = formWorksheets[1];
            Worksheet listSheet = formWorksheets[2];

            groupSheet.Name = "survey";
            listSheet.Name = "choices";
            
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

            groupSheet.Cells[6, "A"] = "integer";
            groupSheet.Cells[6, "B"] = "nbPerson";
            groupSheet.Cells[6, "C"] = "Number of people";
            groupSheet.Cells[6, "E"] = "VRAI";


            groupSheet.Cells[7, "A"] = "end repeat";


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

            formExcel.DisplayAlerts = false;
            formWorkbook.SaveAs(path);
            formWorkbook.Close(true);
            formExcel.Quit();

            // Release all COM objects
            Marshal.ReleaseComObject(groupSheet);
            Marshal.ReleaseComObject(listSheet);
            Marshal.ReleaseComObject(formWorksheets);
            Marshal.ReleaseComObject(formWorkbook);
            Marshal.ReleaseComObject(formWorkbooks);
            Marshal.ReleaseComObject(formExcel);

            return path;
        }
    }
}
