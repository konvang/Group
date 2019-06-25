using System.ComponentModel.DataAnnotations.Schema;

namespace SaftStopEngine
{
    /// <summary>
    /// A class use to represent a friend account.
    /// </summary>
    public class Friends
    {
        /// <summary>
        /// Gets or sets the id of the person.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the friend Id who is friends with user.
        /// </summary>
        public int FriendId { get; set; }

        /////// <summary>
        /////// Gets or sets the friend who is friends with user.
        /////// </summary>
        ////[ForeignKey("FriendId")]
        ////public Account Friend { get; set; }

        /// <summary>
        /// Gets or sets the friend who is friends with user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Returns the username of the friend as an override to ToString.
        /// </summary>
        /// <returns>The username of the friend.</returns>
        public override string ToString()
        {
            return this.Username;
        }
    }
}