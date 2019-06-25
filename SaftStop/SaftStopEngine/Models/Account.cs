using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SaftStopEngine
{
    /// <summary>
    /// The class which is used to represent an account.
    /// </summary>
    public class Account : IDataErrorInfo
    {
        /// <summary>
        /// The properties to validate.
        /// </summary>
        public static readonly string[] PropertiesToValidate = new string[] { "Username", "Password", "FirstName", "LastName", "DateOfBirth", "Email", "PhoneNumber" };

        /// <summary>
        /// Gets or sets the id of the person.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the persons library.
        /// </summary>
        public virtual ICollection<Game> Library { get; set; }

        /// <summary>
        /// Gets or sets The product categories associated with the order.
        /// </summary>
        public virtual ICollection<Friends> FriendsList { get; set; }

        /// <summary>
        /// Gets or sets the person's Credit Card Number on file.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the person's Credit Card Number on file.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the person's First Name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the person's Last Name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the person's date of birth on file.
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the person's Email on file.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the person's phone number on file.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the person's Credit Card Number on file.
        /// </summary>
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the person's Credit Card Number on file.
        /// </summary>
        public decimal AccountFunds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the account is admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets the middle of the account.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets null to show that there is an error.
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current product valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string s in Account.PropertiesToValidate)
                {
                    string validation = this.GetValidationError(s);

                    if (this.GetValidationError(s) != null)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Gets the error of the property name.
        /// </summary>
        /// <param name="propertyName">The property error name.</param>
        /// <returns>Gets the validation of the error.</returns>
        public string this[string propertyName]
        {
            get
            {
                return this.GetValidationError(propertyName);
            }
        }

        /// <summary>
        /// Handles validation for each field of the account.
        /// </summary>
        /// <param name="propertyName">The property that is being validated.</param>
        /// <returns>The result of the validation.</returns>
        private string GetValidationError(string propertyName)
        {
            string result = null;
            switch (propertyName)
            {
                case "Username":
                    result = this.ValidateUsername();
                    break;
                case "Password":
                    result = this.ValidatePassword();
                    break;
                case "FirstName":
                    result = this.ValidateFirstName();
                    break;
                case "LastName":
                    result = this.ValidateLastName();
                    break;
                case "DateOfBirth":
                    result = this.ValidateDateOfBirth();
                    break;
                case "Email":
                    result = this.ValidateEmail();
                    break;
                case "PhoneNumber":
                    result = this.ValidatePhoneNumber();
                    break;
            }

            return result;
        }

        /// <summary>
        /// Validates the username.
        /// </summary>
        /// <returns>The issue of the validator.</returns>
        private string ValidateUsername()
        {
            string result = null;
            if (this.Username == string.Empty || this.Username == null)
            {
                result = "Username is a required field.";
            }
            else if (this.Username.Length > 100)
            {
                result = "Username must be less than 100 characters.";
            }
            else if (!Regex.IsMatch(this.Username, "^[a-zA-Z0-9!()&-]*$"))
            {
                result = "Username must only contain A-Z, 0-9, and these symbols !()&- ";
            }

            return result;
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <returns>The issue of the validator.</returns>
        private string ValidatePassword()
        {
            string result = null;
            if (this.Password == string.Empty || this.Password == null)
            {
                result = "Password is a required field.";
            }
            else if (this.Password.Length > 20 || this.Password.Length < 8)
            {
                result = "Password must be between 8 and 20 characters.";
            }
            else if (!Regex.IsMatch(this.Password, "^[a-zA-Z0-9!()&-]*$"))
            {
                result = "Password must only contain A-Z, 0-9, and these symbols !()&- ";
            }

            return result;
        }

        /// <summary>
        /// Validates the first name.
        /// </summary>
        /// <returns>The issue of the validator.</returns>
        private string ValidateFirstName()
        {
            string result = null;
            if (this.FirstName == string.Empty || this.FirstName == null)
            {
                result = "Name is a required field.";
            }
            else if (this.FirstName.Length > 100)
            {
                result = "Name must be less than 100 characters.";
            }
            else if (!Regex.IsMatch(this.FirstName, "^[a-zA-Z]*$"))
            {
                result = "Name must only contain A-Z";
            }

            return result;
        }

        /// <summary>
        /// Validates the last name.
        /// </summary>
        /// <returns>The issue of the validator.</returns>
        private string ValidateLastName()
        {
            string result = null;
            if (this.LastName == string.Empty || this.LastName == null)
            {
                result = "Last name is a required field.";
            }
            else if (this.LastName.Length > 100)
            {
                result = "Last name must be less than 100 characters.";
            }
            else if (!Regex.IsMatch(this.LastName, "^[a-zA-Z]*$"))
            {
                result = "Last name must only contain A-Z";
            }

            return result;
        }

        /// <summary>
        /// Validates the date of birth.
        /// </summary>
        /// <returns>The issue of the validator.</returns>
        private string ValidateDateOfBirth()
        {
            string result = null;
            if (this.DateOfBirth == string.Empty || this.DateOfBirth == null)
            {
                result = "Date of birth is a required field.";
            }
            else if (!Regex.IsMatch(this.DateOfBirth, "^(((0[13-9]|1[012])[-/]?(0[1-9]|[12][0-9]|30)|(0[13578]|1[02])[-/]?31|02[-/]?(0[1-9]|1[0-9]|2[0-8]))[-/]?[0-9]{4}|02[-/]?29[-/]?([0-9]{2}(([2468][048]|[02468][48])|[13579][26])|([13579][26]|[02468][048]|0[0-9]|1[0-6])00))$"))
            {
                result = "Birth date must be a valid date and in mm-dd-yyy format";
            }

            return result;
        }

        /// <summary>
        /// Validates the email address.
        /// </summary>
        /// <returns>The issue of the validator.</returns>
        private string ValidateEmail()
        {
            string result = null;
            if (this.Email == string.Empty || this.Email == null)
            {
                result = "Email is a required field.";
            }
            else if (!Regex.IsMatch(this.Email, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$"))
            {
                result = "Email must be proper format";
            }

            return result;
        }

        /// <summary>
        /// Validates the phone number.
        /// </summary>
        /// <returns>The issue of the validator.</returns>
        private string ValidatePhoneNumber()
        {
            string result = null;
            if (this.PhoneNumber == string.Empty || this.PhoneNumber == null)
            {
                result = "PhoneNumber is a required field.";
            }
            else if (!Regex.IsMatch(this.PhoneNumber, "(1-)?\\p{N}{3}-\\p{N}{3}-\\p{N}{4}\\b"))
            {
                result = "Phone number must be proper format";
            }

            return result;
        }
    }
}