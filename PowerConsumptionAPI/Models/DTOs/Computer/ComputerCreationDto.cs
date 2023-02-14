using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models.DTOs.Computer
{
    public class ComputerCreationDto
    {
        [Required(ErrorMessage = "Computer ID is a required field")]
        public string Id { get; set; }
    }
}
