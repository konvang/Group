using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// The class which is used to hold the logic for the view of the library of a user.
    /// </summary>
    public class LibraryViewModel : MultiGameViewModel
    {
        /// <summary>
        /// Initializes a new instance of the LibraryViewModel class.
        /// </summary>
        /// <param name="repository">The repository of data.</param>
        /// <param name="account">The account of the user. If admin, will show all games so they can be edited.</param>
        public LibraryViewModel(Repository repository, Account account) : base(repository, account, "Library")
        {
        }
    }
}
