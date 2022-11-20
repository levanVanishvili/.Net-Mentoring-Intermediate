namespace craft_beer.Models
{
    public class BeerStyle
    {
        public int Id { get; set; }
        public string? Style { get; set; }

        public ICollection<beer>? Beers { get; set; }
    }
}
