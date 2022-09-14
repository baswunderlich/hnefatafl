namespace HnefataflServer.Models
{
    public class GameDto
    {
        private Guid Id { get; set; }
        private Guid YourPlayerNumber { get; set; }

        public GameDto(Guid Id, Guid player)
        {
            this.Id = Id;
            this.YourPlayerNumber = player;
        }
    }
}
