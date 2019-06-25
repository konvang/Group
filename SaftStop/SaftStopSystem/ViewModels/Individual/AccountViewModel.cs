using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// A class used to represent an account view model.
    /// </summary>
    public class AccountViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        /// <summary>
        /// The account within the view model.
        /// </summary>
        private Account account;

        /// <summary>
        /// The repository within the view model.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// The main window of the program.
        /// </summary>
        private MainWindow mainWindow;

        /// <summary>
        /// A collection of commands of the command view model.
        /// </summary>
        private ObservableCollection<CommandViewModel> displayCommands = new ObservableCollection<CommandViewModel>();

        /// <summary>
        /// A collection of commands used by the public account view.
        /// </summary>
        private ObservableCollection<CommandViewModel> publicAccountCommand = new ObservableCollection<CommandViewModel>();

        /// <summary>
        /// A value indicating whether item is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the AccountViewModel class.
        /// </summary>
        /// <param name="account">The account to create view based upon.</param>
        /// <param name="repository">The repository of data.</param>
        /// <param name="mainWindow">The main window to close when the user logs out.</param>
        public AccountViewModel(Account account, Repository repository, MainWindow mainWindow) : base("Account")
        {
            this.account = account;

            this.repository = repository;

            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// Gets a command from the command view model.
        /// </summary>
        public ObservableCollection<CommandViewModel> DisplayCommand
        {
            get
            {
                return this.displayCommands;
            }
        }

        /// <summary>
        /// Gets the public account commands.
        /// </summary>
        public ObservableCollection<CommandViewModel> PublicAccountCommand
        {
            get
            {
                return this.publicAccountCommand;
            }
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
        /// Gets the account ID.
        /// </summary>
        public int Id
        {
            get
            {
                return this.account.Id;
            }
        }

        /// <summary>
        /// Gets the account.
        /// </summary>
        public Account Account
        {
            get
            {
                return this.account;
            }
        }

        /// <summary>
        /// Gets or sets the first name of the account.
        /// </summary>
        public string FirstName
        {
            get
            {
                return this.account.FirstName;
            }

            set
            {
                this.account.FirstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Gets or sets the last name of the account.
        /// </summary>
        public string LastName
        {
            get
            {
                return this.account.LastName;
            }

            set
            {
                this.account.LastName = value;
                this.OnPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Gets or sets the date of birth of the account.
        /// </summary>
        public string DateOfBirth
        {
            get
            {
                return this.account.DateOfBirth;
            }

            set
            {
                this.account.DateOfBirth = value;
                this.OnPropertyChanged("DateOfBirth");
            }
        }

        /// <summary>
        /// Gets or sets the email of the account.
        /// </summary>
        public string Email
        {
            get
            {
                return this.account.Email;
            }

            set
            {
                this.account.Email = value;
                this.OnPropertyChanged("Email");
            }
        }

        /// <summary>
        /// Gets or sets the credit card number of the account.
        /// </summary>
        public string CreditCardNumber
        {
            get
            {
                return this.account.CreditCardNumber;
            }

            set
            {
                this.account.CreditCardNumber = value;
                this.OnPropertyChanged("CreditCardNumber");
            }
        }

        /// <summary>
        /// Gets or sets the credit card number of the account.
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return this.account.PhoneNumber;
            }

            set
            {
                this.account.PhoneNumber = value;
                this.OnPropertyChanged("PhoneNumber");
            }
        }

        /// <summary>
        /// Gets or sets the account funds.
        /// </summary>
        public decimal AccountFunds
        {
            get
            {
                return this.account.AccountFunds;
            }

            set
            {
                this.account.AccountFunds = value;
                this.OnPropertyChanged("AccountFunds");
            }
        }

        /// <summary>
        /// Gets or sets the username of the account.
        /// </summary>
        public string Username
        {
            get
            {
                return this.account.Username;
            }

            set
            {
                this.account.Username = value;
                this.OnPropertyChanged("Username");
            }
        }

        /// <summary>
        /// Gets or sets the password of the account.
        /// </summary>
        public string Password
        {
            get
            {
                return this.account.Password;
            }

            set
            {
                this.account.Password = value;
                this.OnPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Gets the error on the account.
        /// </summary>
        public string Error
        {
            get
            {
                return this.account.Error;
            }
        }

        /// <summary>
        /// Gets the name of the account.
        /// </summary>
        /// <param name="propertyName">The property name of the account view.</param>
        /// <returns>The property name of the account.</returns>
        public string this[string propertyName]
        {
            get
            {
                return this.account[propertyName];
            }
        }

        /// <summary>
        /// Method to add the game.
        /// </summary>
        /// <returns>The status of whether the save was successful or not.</returns>
        public bool Save()
        {
            bool result = true;

            if (this.account.IsValid)
            {
                this.repository.AddAccount(this.account);
                this.repository.SaveToDatabase();
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Creates a new command for the account view.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));

            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));

            this.DisplayCommand.Add(new CommandViewModel("Edit Account", new DelegateCommand(p => this.EditAccount())));
            this.DisplayCommand.Add(new CommandViewModel("View Receipts", new DelegateCommand(p => this.ShowReceipts())));
            this.DisplayCommand.Add(new CommandViewModel("Log out", new DelegateCommand(p => this.LogOut())));

            // this.PublicAccountCommand.Add(new CommandViewModel("", new DelegateCommand(p => this.())));
        }

        /// <summary>
        /// Shows the receipts tied to the logged in account.
        /// </summary>
        private void ShowReceipts()
        {
            MultiReceiptViewModel viewModel = new MultiReceiptViewModel(this.repository, this.account);

            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 500;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            MultiReceiptView view = new MultiReceiptView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        /// <summary>
        /// Logs the user out an launches the login window.
        /// </summary>
        private void LogOut()
        {
            MessageBoxResult m = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButton.YesNo);
            if (m == MessageBoxResult.Yes)
            {
                LoginViewModel viewModel = new LoginViewModel();
                WorkspaceWindow window = new WorkspaceWindow();

                this.mainWindow.Close();

                window.Width = 400;
#if DEBUG
                window.Width = 550;
#endif
                window.Title = viewModel.DisplayName;

                viewModel.CloseAction = b => window.DialogResult = b;

                LoginView view = new LoginView();
                view.DataContext = viewModel;

                window.Content = view;
                window.ShowDialog();
            }
        }

        /// <summary>
        /// Edit the current account that is being viewed.
        /// </summary>
        private void EditAccount()
        {
            AccountViewModel viewModel = new AccountViewModel(this.account, this.repository, null);
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 650;
            window.Height = 425;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            AccountView view = new AccountView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        /// <summary>
        /// Execute the okay command.
        /// </summary>
        private void OkExecute()
        {
            if (this.Save())
            {
                this.CloseAction(true);
            }
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