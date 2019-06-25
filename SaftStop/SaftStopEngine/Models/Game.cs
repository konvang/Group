namespace SaftStopEngine
{
    /// <summary>
    /// A class used to represent a game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Gets or sets the id of the game.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Publisher id of the game.
        /// </summary>
        public int PublisherId { get; set; }

        /// <summary>
        /// Gets or sets the publisher of the game.
        /// </summary>
        public virtual Publisher Publisher { get; set; }

        /// <summary>
        /// Gets or sets Developer id of the game.
        /// </summary>
        public int DeveloperId { get; set; }

        /// <summary>
        /// Gets or sets the developer of the game.
        /// </summary>
        public virtual Developer Developer { get; set; }

        /// <summary>
        /// Gets or sets the price of the game.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the used price of the game.
        /// </summary>
        public decimal UsedPrice { get; set; }

        /// <summary>
        /// Gets or sets the used quantity of the game.
        /// </summary>
        public int UsedQuantity { get; set; }
    }
}
