namespace CommandsService.DTOs
{
    public class CommandReadDTO
    {
        public int ID { get; set; }
        public string? HowTo { get; set; }
        public string? CommandLine { get; set; }
        public int PlatformID { get; set; }
    }
}