using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfamSurveys.Models
{
    class Origins
    {
        private static Dictionary<int, string> origins = new Dictionary<int, string>()
        {
            { 0, "Crops from own production" },
            { 1, "Product from own livestock" },
            { 2, "Wild food" },
            { 3, "Purchase" },
            { 4, "Payment in kind" },
            { 5, "Gift/loan of food" }
        };

        public static string GetById(int id)
        {
            return origins[id];
        }

        public static int Count()
        {
            return origins.Count;
        }
    }
}
