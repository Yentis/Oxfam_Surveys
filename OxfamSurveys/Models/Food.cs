namespace OxfamSurveys.Models
{
    public class Food
    {
        public string Name { get; }
        public string Type { get; }
        public int PeopleNbr { get; set; }

        public Food(string name, string type)
        {
            Name = name;
            Type = type;
            PeopleNbr = 0;
        }
    }
}