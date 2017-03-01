using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models
{
    public interface Api
    {
        bool createForm(IEnumerable<Food>food);
        FormData getData();
    }
}
