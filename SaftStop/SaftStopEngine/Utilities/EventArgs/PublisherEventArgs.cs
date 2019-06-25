namespace SaftStopEngine
{
    /// <summary>
    /// The publisher event args.
    /// </summary>
    public class PublisherEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PublisherEventArgs class.
        /// </summary>
        /// <param name="publisher">The publisher to act upon.</param>
        public PublisherEventArgs(Publisher publisher)
        {
            this.Publisher = publisher;
        }

        /// <summary>
        /// Gets the publisher.
        /// </summary>
        public Publisher Publisher { get; private set; }
    }
}
