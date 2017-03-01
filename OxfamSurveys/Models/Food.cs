namespace OxfamSurveys.Models
{
    public class Food
    {
        public string Name { get; }
        public string Type { get; }

        public Food(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}