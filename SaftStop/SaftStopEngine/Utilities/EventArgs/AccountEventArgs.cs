namespace SaftStopEngine
{
    /// <summary>
    /// A class used to represent the AccountEventArgs.
    /// </summary>
    public class AccountEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the AccountEventArgs class.
        /// </summary>
        /// <param name="account">The account to act upon.</param>
        public AccountEventArgs(Account account)
        {
            this.Account = account;
        }

        /// <summary>
        /// Gets the account.
        /// </summary>
        public Account Account { get; private set; }
    }
}