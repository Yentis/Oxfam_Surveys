using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models.QuestionType
{
    public class Geopoint
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
    }
}
