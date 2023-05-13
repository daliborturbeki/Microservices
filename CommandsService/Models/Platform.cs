using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    public class Platform 
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ExternalID { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}