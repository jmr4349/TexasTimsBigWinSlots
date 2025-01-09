namespace TexasTimsBigWinSlots.Models
{
    public class ApiResponse
    {
        public required string[] slotData { get; set; }
        public required int credits { get; set; }

        public bool winning { get; set; }

    }
}