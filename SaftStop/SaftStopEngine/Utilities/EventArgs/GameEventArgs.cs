namespace SaftStopEngine
{
    /// <summary>
    /// A class to represent the game event arguments.
    /// </summary>
    public class GameEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the GameEventArgs class.
        /// </summary>
        /// <param name="game">The game to act upon.</param>
        public GameEventArgs(Game game)
        {
            this.Game = game;
        }

        /// <summary>
        /// Gets the Game.
        /// </summary>
        public Game Game { get; private set; }
    }
}
