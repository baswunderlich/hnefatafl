namespace HnefataflServer.Games
{
    public class Game
    {
        public Guid GameId { get; set; }
        public Guid Player1 { get; set; }
        public Guid Player2 { get; set; }

        public Game()
        {
            GameId = Guid.NewGuid();
        }
    }
}