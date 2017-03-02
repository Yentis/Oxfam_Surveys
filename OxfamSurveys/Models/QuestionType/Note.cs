using OxfamSurveys.Interfaces;
using System;

namespace OxfamSurveys.Models.QuestionType
{
    public class Note : Renderable
    {
        private readonly string type;
        private string name;
        private string label;
        private bool required;
        private string appearance;

        public string Type => type;
        public string Name => name;
        public string Label => label;
        public bool Required => required;
        public string Appearance => appearance;

        public Note(string name, string label, bool required, string appearance)
        {
            type = "note";
            this.name = name;
            this.label = label;
            this.required = required;
            this.appearance = appearance;
        }
    }
}
