namespace OxfamSurveys.Models
{
    public class FoodAmount
    {
        public Food Food { get; }
        public float Amount { get; }

        public FoodAmount(Food food, float amount)
        {
            Food = food;
            Amount = amount;
        }
    }
}