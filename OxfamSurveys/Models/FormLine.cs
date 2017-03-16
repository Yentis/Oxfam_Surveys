using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models
{
    public class FormLine : FoodAmount
    {
        public string Origin { get; }
        public int PeopleNbr { get; }

        public FormLine(Food food, float amount, string origin, int peopleNbr) : base(food, amount)
        {
            Origin = origin;
            PeopleNbr = peopleNbr;
        }
    }
}
