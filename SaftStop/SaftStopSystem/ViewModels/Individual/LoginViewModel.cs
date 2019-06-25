using System.Collections.Generic;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// A class used to represent the log in view model.
    /// </summary>
    public class LoginViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The user name for the log in.
        /// </summary>
        private string username;

        /// <summary>
        /// The password for the log in.
        /// </summary>
        private string password;

        /// <summary>
        /// The repository where information is stored.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// Initializes a new instance of the LoginViewModel class.
        /// </summary>
        public LoginViewModel() : base("Log in")
        {
            this.repository = new Repository();
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                this.username = value;
                this.OnPropertyChanged("Username");
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
                this.OnPropertyChanged("Username");
            }
        }

        /// <summary>
        /// Attempts login.
        /// </summary>
        /// <returns>Indicating whether the log in is successful or not.</returns>
        public Account Login()
        {
            Account account = null;

            List<Account> accounts = this.repository.GetAccounts();

            foreach (Account a in accounts)
            {
                if (a.Username == this.username && a.Password == this.password)
                {
                    return account = a;
                }
            }

            return account;
        }

        /// <summary>
        /// Creates the commands for this view model.
        /// </summary>
        protected override void CreateCommands()
        {
#if DEBUG
            this.Commands.Add(new CommandViewModel("Quick Admin", new DelegateCommand(p => this.QuickLog("Admin", "Password"))));
            this.Commands.Add(new CommandViewModel("Quick User", new DelegateCommand(p => this.QuickLog("Random", "random"))));
#endif
            this.Commands.Add(new CommandViewModel("Login", new DelegateCommand(p => this.LoginExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
            this.Commands.Add(new CommandViewModel("Sign Up", new DelegateCommand(p => this.SignUpExecute())));
        }

        /// <summary>
        /// Ok to execute save and close.
        /// </summary>
        private void LoginExecute()
        {
            Account result = this.Login();
            if (result != null)
            {
                MainWindow window = new MainWindow();

                window.Show();
                window.DataContext = new MainWindowViewModel(this.repository, result, window);
                if (result.IsAdmin == false)
                {
                    window.Grid.Children.Remove(window.AdminPanel);
                }

                this.CloseAction(true);
            }
        }

        /// <summary>
        /// Ok to execute save and close.
        /// </summary>
        /// <param name="username">The username of the account to quick log.</param>
        /// <param name="password">The password of the account to quick log.</param>
        private void QuickLog(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            Account result = this.Login();
            if (result != null)
            {
                MainWindow window = new MainWindow();
                window.Show();
                window.DataContext = new MainWindowViewModel(this.repository, result, window);
                if (result.IsAdmin == false)
                {
                    window.Grid.Children.Remove(window.AdminPanel);
                }

                this.CloseAction(true);
            }
        }

        /// <summary>
        /// Cancels the execution.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }

        /// <summary>
        /// Lets the user sign up.
        /// </summary>
        private void SignUpExecute()
        {
            Account account = new Account();
            //// AccountViewModel viewModel = new AccountViewModel(account, this.repository);
            SignUpViewModel sviewModel = new SignUpViewModel(account, this.repository);
            AccountView view = new AccountView();
            SignUpView sview = new SignUpView();

            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 550;
            window.Height = 350;
            window.Title = sviewModel.DisplayName;

            sviewModel.CloseAction = b => window.DialogResult = b;
            account.IsAdmin = false;

            sview.DataContext = sviewModel;

            window.Content = sview;
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                this.username = account.Username;
                this.password = account.Password;

                this.LoginExecute();
            }
        }
    }
}
