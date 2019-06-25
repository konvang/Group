using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaftStopEngine;

namespace SaftStopTest
{
    /// <summary>
    /// The testing class for the account.
    /// </summary>
    [TestClass]
    public class AccountTest
    {
        /// <summary>
        /// The unit test for username validation.
        /// </summary>
        [TestMethod]
        public void ValidateUsernameTest()
        {
            string test = "Username";
            string goodName = "George123";
            string badName1 = string.Empty;
            string badName2 = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            string badName3 = "George12#%";

            Account account = new Account();
            account.Username = goodName;

            Assert.AreEqual<string>(null, account[test], "Valid username did not pass!");

            account.Username = badName1;

            Assert.AreEqual<string>("Username is a required field.", account[test], "Inalid username passed string was empty!");

            account.Username = badName2;

            Assert.AreEqual<string>("Username must be less than 100 characters.", account[test], "Inalid username passed string was over 100 characters!");

            account.Username = badName3;

            Assert.AreEqual<string>("Username must only contain A-Z, 0-9, and these symbols !()&- ", account[test], "Inalid username passed string contained invalid characters!");
        }

        /// <summary>
        /// The unit test for validation of passwords.
        /// </summary>
        [TestMethod]
        public void ValidatePasswordTest()
        {
            string test = "Password";
            string goodPassword = "George123";
            string badPassword1 = string.Empty;
            string badPassword2 = "long12345678901234567";
            string badPassword3 = "short1";
            string badPassword4 = "George123#$";

            Account account = new Account();
            account.Password = goodPassword;

            Assert.AreEqual<string>(null, account[test], "Valid password did not pass!");

            account.Password = badPassword1;

            Assert.AreEqual<string>("Password is a required field.", account[test], "Inalid password passed string was empty!");

            account.Password = badPassword2;

            Assert.AreEqual<string>("Password must be between 8 and 20 characters.", account[test], "Inalid password passed string was not between 8 and 20 characters!");

            account.Password = badPassword3;

            Assert.AreEqual<string>("Password must be between 8 and 20 characters.", account[test], "Inalid password passed string was not between 8 and 20 characters!");

            account.Password = badPassword4;

            Assert.AreEqual<string>("Password must only contain A-Z, 0-9, and these symbols !()&- ", account[test], "Inalid username passed string contained invalid characters!");
        }

        /// <summary>
        /// The unit test for email validation.
        /// </summary>
        [TestMethod]
        public void ValidateEmailTest()
        {
            string test = "Email";
            string goodEmail = "George123@email.com";
            string badEmail1 = string.Empty;

            Account account = new Account();

            account.Email = goodEmail;
            Assert.AreEqual<string>(null, account[test], "Valid email did not pass!");

            account.Email = badEmail1;
            Assert.AreEqual<string>("Email is a required field.", account[test], "Invalid email passed string was empty or null.");
        }
    }
}