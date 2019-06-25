using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// A class to represent a list of users.
    /// </summary>
    public class MultiAccountViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The repository of data.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// Initializes a new instance of the MultiAccountViewModel class.
        /// </summary>
        /// <param name="account">The account that we are currently using.</param>
        /// <param name="repository">The repository of data.</param>
        public MultiAccountViewModel(Account account, Repository repository) : base("Users")
        {
            this.repository = repository;

            this.repository.AccountAdded += this.OnAccountAdded;

            this.AllAccounts = new ObservableCollection<AccountViewModel>();

            List<AccountViewModel> accounts =
                (from a in this.repository.GetAccounts()
                 where a != account
                 select new AccountViewModel(a, this.repository, null)).ToList();

            accounts.ForEach(cvm => cvm.PropertyChanged += this.OnAccountViewModelPropertyChanged);

            this.AllAccounts = new ObservableCollection<AccountViewModel>(accounts);
        }

        /// <summary>
        /// Gets or sets all the accounts.
        /// </summary>
        public ObservableCollection<AccountViewModel> AllAccounts { get; set; }

        /// <summary>
        /// Gets or sets the search text of the accounts.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets the number of items selected.
        /// </summary>
        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllAccounts.Count(vm => vm.IsSelected);
            }
        }

        /// <summary>
        /// The account to update.
        /// </summary>
        public void Update()
        {
            this.OnPropertyChanged("AllAccounts");
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        protected override void CreateCommands()
        {
        }

        /// <summary>
        /// On account added.
        /// </summary>
        /// <param name="sender">A Button click.</param>
        /// <param name="e">The click of the button.</param>
        private void OnAccountAdded(object sender, AccountEventArgs e)
        {
            AccountViewModel viewModel = new AccountViewModel(e.Account, this.repository, null);
            this.AllAccounts.Add(viewModel);
        }

        /// <summary>
        /// On the account view model property changed.
        /// </summary>
        /// <param name="sender">The view that was changed.</param>
        /// <param name="e">The event that .</param>
        private void OnAccountViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        /// <summary>
        /// Shows the item in the view.
        /// </summary>
        /// <param name="viewModel">The view model to show.</param>
        private void ShowAccount(AccountViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            AccountView view = new AccountView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }
    }
}
