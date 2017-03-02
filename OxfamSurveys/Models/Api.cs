using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models
{
    public interface Api
    {
        bool CreateForm(string name, IEnumerable<Food> food);
        FormData GetData(object formId);
    }
}
