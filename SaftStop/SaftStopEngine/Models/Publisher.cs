namespace SaftStopEngine
{
    /// <summary>
    /// A class to represent the publisher.
    /// </summary>
    public class Publisher
    {
        /// <summary>
        /// Gets or sets the id of the publisher.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the publisher name.
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
