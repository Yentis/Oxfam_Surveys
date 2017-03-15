using RestSharp.Deserializers;

namespace OxfamSurveys.Models.KoBoApiRequests
{
    public class NutvalData
    {
        [DeserializeAs(Name = "nutval/food")]
        public int Food { get; set; }

        [DeserializeAs(Name = "nutval/quantity")]
        public float Quantity { get; set; }

        [DeserializeAs(Name = "nutval/origin")]
        public int Origin { get; set; }

        [DeserializeAs(Name = "nutval/peopleNbr")]
        public int PeopleNbr { get; set; }
    }
}