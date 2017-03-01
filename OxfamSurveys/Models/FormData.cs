using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models
{
    public class FormData
    {
        public int NbPeople { get; }
        public IEnumerable<FormLine> Lines { get; }

        public FormData(int nbPeople, IEnumerable<FormLine> lines)
        {
            NbPeople = nbPeople;
            Lines = lines;
        }
    }
}
