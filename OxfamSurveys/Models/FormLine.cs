using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models
{
    public class FormLine : FoodAmount
    {
        public string Location { get; }

        public FormLine(Food food, float amount, string location) : base(food, amount)
        {
            Location = location;
        }
    }
}
