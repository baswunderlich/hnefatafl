namespace HnefataflServer.Games
{
    public class GameStorage
    {
        private static List<Game> gamelist = new List<Game>();
        private static List<Game> gameListNoEnemy = new List<Game>();
        private static Dictionary<Guid, Game> playingPlayers = new Dictionary<Guid, Game>();
        private static object olock = new object();

        public GameStorage()
        {
        }

        public static List<Game> GetGameList()
        {
            return gamelist;
        }

        private static bool IsGameAvailable()
        {
            if(gameListNoEnemy.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static Game CreateNewGame()
        {
            var game = new Game();
            gameListNoEnemy.Add(game);
            return game;
        }

        public static Game JoinPlayer(Guid player)
        {
            if(IsGameAvailable())
            {
                //Joining a player
                Console.WriteLine("Joining a game");
                lock (olock)
                {
                    var game = gameListNoEnemy.FirstOrDefault();
                    gameListNoEnemy.Remove(game);
                    gamelist.Add(game);
                    game.Player2 = player;
                    playingPlayers[player] = game;
                    return game;
                }
            }
            else
            {
                //Creating a game
                Console.WriteLine("Just created a game");
                lock (olock)
                {
                    var game = CreateNewGame();
                    gameListNoEnemy.Add(game);
                    game.Player1 = player;
                    playingPlayers[player] = game;
                    return game;
                }
            }
        }

        public static Game? GetGameOfPlayer(Guid player)
        {
            if(playingPlayers.ContainsKey(player))
                return playingPlayers[player];
            else
                return null;
        }
    }
}
