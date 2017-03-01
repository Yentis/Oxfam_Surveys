using GalaSoft.MvvmLight.Command;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OxfamSurveys.ViewModel
{
    public class MenuViewModel
    {
        private ICommand _CreateCommand;
        public ICommand CreateCommand
        {
            get
            {
                return _CreateCommand ?? (
                    _CreateCommand = new RelayCommand(() =>
                    {
                        var excelApp = new Microsoft.Office.Interop.Excel.Application();
                        _Worksheet worksheet = LoadFile(excelApp, "NutVal.xlsm", "Calculation Sheet");
                        List<string> foodnames = new List<string>();
                        List<int> amounts = new List<int>();
                        foodnames.Add("BANANA");
                        amounts.Add(10);
                        WriteData(worksheet, foodnames, amounts);
                        excelApp.Visible = true;
                    })
                );
            }
        }
        private void WriteData(_Worksheet sheet, List<string> foodnames, List<int> amounts)
        {
            int i = 8;

            if(foodnames.Count > 9)
            {
                // TODO - Add a row
            }

            for (int j = 0; j < foodnames.Count; j++)
            {
                sheet.Cells[i, "C"] = foodnames[j];
                sheet.Cells[i, "F"] = amounts[j];
                i++;
            }
        }

        private _Worksheet LoadFile(Microsoft.Office.Interop.Excel.Application excelApp, string location, string sheettoread)
        {
            string workbookPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            workbookPath = System.IO.Path.GetDirectoryName(workbookPath) + "\\Excel\\" + location;
            var workbook = excelApp.Workbooks.Open(workbookPath);

            /*foreach (Worksheet worksheet in workbook.Worksheets)
            {
                Console.WriteLine(worksheet.Name);
            }*/

            return (Worksheet)excelApp.Worksheets[sheettoread];

            /*workSheet.Cells[1, "A"] = "ID Number";
            workSheet.Cells[1, "B"] = "Current Balance";

            var row = 1;
            foreach (var acct in accounts)
            {
                row++;
                workSheet.Cells[row, "A"] = acct.ID;
                workSheet.Cells[row, "B"] = acct.Balance;
            }

            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();*/
        }
    }
}
