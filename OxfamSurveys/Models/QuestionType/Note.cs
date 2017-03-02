namespace OxfamSurveys.Models.QuestionType
{
    public class Note : ExcelLine
    {
        private string content;

        public string Content
        {
            get { return content; }
            set
            {
                this.content = value;
            }
        }

        public Note(string name, string label, bool required, string appearance) : base(name, label, required, appearance)
        {
        }
    }
}
