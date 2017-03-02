using OxfamSurveys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models.QuestionType
{
    public class ExcelLine : Renderable
    {
        public string Type => type;
        public string Name => name;
        public string Label => label;
        public bool Required => required;
        public string Appearance => appearance;

        public ExcelLine(string name, string label, bool required, string appearance)
        {
            type = "integer";
            this.name = name;
            this.label = label;
            this.required = required;
            this.appearance = appearance;            
        }
    }
}
