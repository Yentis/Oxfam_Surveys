using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using OxfamSurveys.Interfaces;

namespace OxfamSurveys.Models.QuestionType
{
    public class ExcelLine : Renderable
    {
        public string Type { protected set; get; }
        public string Name { protected set; get; }
        public string Label { protected set; get; }
        public bool Required { protected set; get; }
        public string Appearance { protected set; get; }

        public ExcelLine(string name, string label, bool required, string appearance)
        {
            this.Name = name;
            this.Label = label;
            this.Required = required;
            this.Appearance = appearance;       
        }

        public virtual void Render()
        {
            throw new NotImplementedException();
        }
    }
}
