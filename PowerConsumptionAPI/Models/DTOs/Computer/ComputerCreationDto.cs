using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models.DTOs.Computer
{
    public class ComputerCreationDto
    {
        [Required(ErrorMessage = "Computer ID is a required field")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Computer name is a required field")]
        public string Name { get; set; }
    }
}
