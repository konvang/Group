using System.ComponentModel;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// A class used to represent a view model for signing up.
    /// </summary>
    public class SignUpViewModel : WorkspaceViewModel, IDataErrorInfo
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
        /// A value indicating whether item is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the SignUpViewModel class.
        /// </summary>
        /// <param name="account">The account to create view based upon.</param>
        /// <param name="repository">The repository of data.</param>
        public SignUpViewModel(Account account, Repository repository) : base("Sign Up")
        {
            this.account = account;

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
        /// Gets the property name of the string.
        /// </summary>
        /// <param name="propertyName">The name of the property name to change.</param>
        /// <returns>The value of the account.</returns>
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