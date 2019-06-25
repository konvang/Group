namespace SaftStopEngine
{
    /// <summary>
    /// The developer event args.
    /// </summary>
    public class DeveloperEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DeveloperEventArgs class.
        /// </summary>
        /// <param name="developer">The developer to act upon.</param>
        public DeveloperEventArgs(Developer developer)
        {
            this.Developer = developer;
        }

        /// <summary>
        /// Gets the Game.
        /// </summary>
        public Developer Developer { get; private set; }
    }
}
