namespace SaftStopEngine
{
    /// <summary>
    /// The class which is used to represent a game developer.
    /// </summary>
    public class Developer
    {
        /// <summary>
        /// Gets or sets the id of the developer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the developer name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Overrides the base to string method.
        /// </summary>
        /// <returns>The desired string.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
