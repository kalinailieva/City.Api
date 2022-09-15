using System.ComponentModel.DataAnnotations;

namespace CityInfo.Api.Models
{
    public class CityWithoutPointsDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
