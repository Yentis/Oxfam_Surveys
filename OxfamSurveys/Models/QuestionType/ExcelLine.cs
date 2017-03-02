using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models.QuestionType
{
    class ExcelLine : Renderable
    {
        private string type;
        private string name;
        private string label;
        private bool required;
        private string appearance;

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
