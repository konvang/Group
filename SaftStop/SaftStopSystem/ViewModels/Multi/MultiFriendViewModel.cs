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
    /// A class to represents a list of friends.
    /// </summary>
    public class MultiFriendViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The repository of data.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// The account of each row in the view model.
        /// </summary>
        private Account account;

        /// <summary>
        /// Initializes a new instance of the MultiFriendViewModel class.
        /// </summary>
        /// <param name="repository">The repository of data.</param>
        /// <param name="account">The account to show the friends of.</param>
        public MultiFriendViewModel(Repository repository, Account account) : base("Friends")
        {
            this.repository = repository;

            this.account = account;

            //this.repository.FriendAdded += this.OnFriendAdded;

            this.AllFriends = new ObservableCollection<FriendViewModel>();

            // this.AllFriends = account.FriendsList;

            this.Update();
        }

        /// <summary>
        /// Gets or sets all the friends.
        /// </summary>
        public ObservableCollection<FriendViewModel> AllFriends { get; set; }

        /// <summary>
        /// Gets the number of items selected.
        /// </summary>
        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllFriends.Count(vm => vm.IsSelected);

            }
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("Add Friend", new DelegateCommand(p => this.AddNewFriend())));
            this.Commands.Add(new CommandViewModel("Remove Friend", new DelegateCommand(p => this.RemoveFriend(), p => this.NumberOfItemsSelected == 1)));
            this.Commands.Add(new CommandViewModel("View Friend", new DelegateCommand(p => this.ViewFriend(), p => this.NumberOfItemsSelected == 1)));
        }

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
        /// On friend added.
        /// </summary>
        /// <param name="sender">A Button click.</param>
        /// <param name="e">The click of the button.</param>
        private void OnFriendAdded(object sender, FriendEventArgs e)
        {
            FriendViewModel viewModel = new FriendViewModel(e.Friends, this.repository);
            viewModel.PropertyChanged += this.OnFriendViewModelPropertyChanged;
            this.AllFriends.Add(viewModel);
        }

        /// <summary>
        /// Shows the item in the view.
        /// </summary>
        /// <param name="viewModel">The view model to show.</param>
        private void ShowFriend(AddFriendViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 500;
            window.Height = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            FriendView view = new FriendView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                this.Update();
            }
        }

        /// <summary>
        /// Opens the window to add a friend.
        /// </summary>
        private void AddNewFriend()
        {
            AddFriendViewModel viewModel = new AddFriendViewModel(this.account, this.repository);
            this.ShowFriend(viewModel);
        }

        /// <summary>
        /// Removes a friend.
        /// </summary>
        private void RemoveFriend()
        {
            FriendViewModel viewModel = this.AllFriends.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to remove " + viewModel.Username + " from your friends?", "Delete confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    this.account.FriendsList.Remove(viewModel.Friend);
                    string username = viewModel.Username;

                    Friends friendAccount = null;

                    foreach (Friends g in this.repository.GetFriends())
                    {
                        if (g.Id == viewModel.Friend.FriendId)
                        {
                            friendAccount = g;

                            break;
                        }
                    }

                    if (account != null)
                    {
                        AccountViewModel accountViewModel = new AccountViewModel(account, repository, null);
                        accountViewModel.Account.FriendsList.Remove(friendAccount);
                    }

                    this.Update();
                }
            }
            else
            {
                MessageBox.Show("Please select only one developer.");
            }
        }

        /// <summary>
        /// Views the friend's public account.
        /// </summary>
        private void ViewFriend()
        {
            string username = this.AllFriends.SingleOrDefault(vm => vm.IsSelected).Username;

            ////IEnumerable<AccountViewModel> friend =
            ////    from a in this.repository.GetAccounts()
            ////    where a.Username == username
            ////    select new AccountViewModel(a, this.repository, null);
            
            List<Account> friends = this.repository.GetAccounts();
            AccountViewModel viewModel = null;

            for (int i = 0; i < friends.Count(); i++)
            {
                if (friends[i].Username == username)
                {
                    viewModel = new AccountViewModel(friends[i], this.repository, null);
                }
            }

            if (viewModel != null)
            {
                WorkspaceWindow window = new WorkspaceWindow();
                window.Width = 500;
                window.Height = 300;
                window.Title = viewModel.DisplayName;

                viewModel.CloseAction = b => window.DialogResult = b;

                PublicAccountView view = new PublicAccountView();
                view.DataContext = viewModel;

                window.Content = view;
                window.ShowDialog();

                if (window.DialogResult == true)
                {
                    this.Update();
                }
            }
        }
    }
}