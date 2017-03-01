using System;
using System.Collections.Generic;

namespace OxfamSurveys.Models
{
    class KoBoApi : Api
    {
        public bool createForm()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodAmount> getData()
        {
            return new List<FoodAmount>();
        }
    }
}
