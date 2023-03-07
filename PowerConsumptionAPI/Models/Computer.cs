using System.ComponentModel.DataAnnotations;

namespace PowerConsumptionAPI.Models
{
    public class Computer
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Inactivity { get; set; }

        public ICollection<PowerConsumption> PowerConsumptionData { get; set; }
    }
}
