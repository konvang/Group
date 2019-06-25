namespace SaftStopEngine
{
    /// <summary>
    /// A class used to represent the FriendEventArgs.
    /// </summary>
    public class FriendEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the FriendEventArgs class.
        /// </summary>
        /// <param name="friends">The friend to act upon.</param>
        public FriendEventArgs(Friends friends)
        {
            this.Friends = friends;
        }

        /// <summary>
        /// Gets the friend.
        /// </summary>
        public Friends Friends { get; private set; }
    }
}