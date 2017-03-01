using System;
using System.Collections.Generic;

namespace OxfamSurveys.Models
{
    public class KoBoApi : Api
    {
        public bool createForm(string name, IEnumerable<Food> food)
        {
            throw new NotImplementedException();
        }

        public FormData getData()
        {

            return new FormData(0, new List<FormLine>());
        }
    }
}
