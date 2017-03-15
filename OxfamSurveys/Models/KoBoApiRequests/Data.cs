using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models.KoBoApiRequests
{
    class Data
    {
        public int PeopleNbr { get; set; }
        public List<NutvalData> Nutval { get; set; }
    }
}
