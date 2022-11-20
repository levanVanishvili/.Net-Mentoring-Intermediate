using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace craft_beer.Models
{
    public class beer
    {
        public int Id { get; set; }
        public string? BeerTitle { get; set; }

        [Required]
        public int BeerStyleId { get; set; }

        [ForeignKey("BeerStyleId")]
        public BeerStyle? Style { get; set; }
    }
}
