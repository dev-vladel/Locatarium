namespace Locatarium.Models
{
    public class RatingModel
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public int ResidenceId { get; set; }

        public int? UserId { get; set; }
    }
}
