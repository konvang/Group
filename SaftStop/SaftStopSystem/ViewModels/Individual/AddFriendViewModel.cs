using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// A class used to represent the friend view model.
    /// </summary>
    public class AddFriendViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The friend within the view model.
        /// </summary>
        private Account account;

        /// <summary>
        /// The repository within the view model.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// A value indicating whether item is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// A filtered version of the account view model.
        /// </summary>
        private MultiAccountViewModel filteredAccounts;

        /// <summary>
        /// Initializes a new instance of the AddFriendViewModel class.
        /// </summary>
        /// <param name="account">The account to create view based upon.</param>
        /// <param name="repository">The repository of data.</param>
        public AddFriendViewModel(Account account, Repository repository) : base("Friend")
        {
            this.account = account;

            this.repository = repository;

            this.filteredAccounts = new MultiAccountViewModel(this.account, this.repository);

            this.filteredAccounts.AllAccounts = this.FilterResults;

            this.AllFriends = new ObservableCollection<FriendViewModel>();
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
        /// Gets or sets the result of the search.
        /// </summary>
        public string SearchText
        {
            get
            {
                return this.filteredAccounts.SearchText;
            }

            set
            {
                this.filteredAccounts.SearchText = value;
                this.OnPropertyChanged("SearchText");
            }
        }

        /// <summary>
        /// Gets the filtered order view model.
        /// </summary>
        public MultiAccountViewModel FilteredAccountViewModel
        {
            get
            {
                return this.filteredAccounts;
            }
        }

        /// <summary>
        /// Gets or sets the results when filtering.
        /// </summary>
        public ObservableCollection<AccountViewModel> FilterResults
        {
            get
            {
                return this.filteredAccounts.AllAccounts;
            }

            set
            {
                this.filteredAccounts.AllAccounts = value;
            }
        }

        /// <summary>
        /// Creates the commands for the view model.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("Search", new DelegateCommand(p => this.Search())));
            this.Commands.Add(new CommandViewModel("Add Friend", new DelegateCommand(p => this.AddFriend())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Searches all accounts for the specified username.
        /// </summary>
        private void Search()
        {
            if (this.SearchText != null || this.SearchText != string.Empty)
            {
                IEnumerable<AccountViewModel> accounts =
                    from a in this.repository.GetAccounts()
                    where a.Username.ToLower().Contains(this.SearchText.ToLower())
                    select new AccountViewModel(a, this.repository, null);

                this.filteredAccounts.AllAccounts = new ObservableCollection<AccountViewModel>(accounts);

                this.filteredAccounts.Update();
            }
        }

        /// <summary>
        /// Creates new item.
        /// </summary>
        private void AddFriend()
        {
            AccountViewModel result = this.FilterResults.SingleOrDefault(vm => vm.IsSelected);
            bool isFriend = false;
            if (result != null)
            {
                foreach (Friends f in this.account.FriendsList)
                {
                    if (f.Username == result.Username)
                    {
                        MessageBox.Show("You are already friends");
                        isFriend = true;
                    }
                }

                if (isFriend == false)
                {
                    Friends friends = new Friends() { FriendId = result.Account.Id, Username = result.Account.Username };
                    this.account.FriendsList.Add(friends);
                    this.repository.SaveToDatabase();
                    friends = new Friends() { FriendId = this.account.Id, Username = this.account.Username };
                    result.Account.FriendsList.Add(friends);
                    this.repository.SaveToDatabase();
                    this.CloseAction(true);
                    this.Update();
                }
            }
            else
            {
                MessageBox.Show("Please select only one person");
            }
        }

        /// <summary>
        /// Gets or sets all the friends.
        /// </summary>
        public ObservableCollection<FriendViewModel> AllFriends { get; set; }

        /// <summary>
        /// The account to update within the friends view.
        /// </summary>
        private void Update()
        {
            List<FriendViewModel> friends =
            (from a in this.account.FriendsList
             select new FriendViewModel(a, this.repository)).ToList();

            friends.ForEach(cvm => cvm.PropertyChanged += this.OnFriendViewModelPropertyChanged);

            this.AllFriends = new ObservableCollection<FriendViewModel>(friends);

            this.OnPropertyChanged("AllFriends");
        }

        /// <summary>
        /// On the friend view model property changed.
        /// </summary>
        /// <param name="sender">The view that was changed.</param>
        /// <param name="e">The event that .</param>
        private void OnFriendViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        /// <summary>
        /// Cancels the execution.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}