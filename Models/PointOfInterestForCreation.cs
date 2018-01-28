using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Models
{
    public class PointOfInterestForCreation
    {
        [Required(ErrorMessage="You should provide name")]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
    }
}