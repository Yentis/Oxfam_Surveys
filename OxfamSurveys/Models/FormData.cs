using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models
{
    public class FormData
    {
        public IEnumerable<FormLine> Lines { get; }

        public FormData(IEnumerable<FormLine> lines)
        {
            Lines = lines;
        }
    }
}
