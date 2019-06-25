using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// The view model for a friends list.
    /// </summary>
    public class FriendViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The friend within the view model.
        /// </summary>
        private Friends friend;

        /// <summary>
        /// The repository within the view model.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// A value indicating whether item is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the FriendViewModel class.
        /// </summary>
        /// <param name="friend">The account to create view based upon.</param>
        /// <param name="repository">The repository of data.</param>
        public FriendViewModel(Friends friend, Repository repository) : base("Friend")
        {
            this.friend = friend;
            this.repository = repository;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the is selected value.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Gets the username of the account.
        /// </summary>
        public string Username
        {
            get
            {
                return this.friend.Username;
            }
        }

        /// <summary>
        /// Gets the friend.
        /// </summary>
        public Friends Friend
        {
            get
            {
                return this.friend;
            }
        }

        /// <summary>
        /// Method to add the game.
        /// </summary>
        public void Save()
        {
            //this.repository.AddFriend(this.friend);
            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// Creates a new command for the friend view.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("Add", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Execute the okay command.
        /// </summary>
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Execute the cancel command.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}