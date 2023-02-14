using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models.DTOs.Computer
{
    public class ComputerUpdateDto
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }
    }
}
