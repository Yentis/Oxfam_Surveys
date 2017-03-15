using OxfamSurveys.Models.KoBoApiRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models
{
    public interface Api
    {
        Form CreateForm(string name, string path);
        FormData GetData(object formId, object query = null);
        List<Form> GetForms();
    }
}
