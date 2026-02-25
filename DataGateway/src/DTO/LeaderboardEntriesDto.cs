namespace DataGateway.DTO
{
    public class LeaderboardEntriesDto
    {
        public string CharacterName { get; set; }
        public int Rank { get; set; }
        public int Rating { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
    }
}
