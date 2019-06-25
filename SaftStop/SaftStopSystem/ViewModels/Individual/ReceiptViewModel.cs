using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// The class which holds the logic for the receipt view.
    /// </summary>
    public class ReceiptViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The receipt within the view model.
        /// </summary>
        private Receipt receipt;

        /// <summary>
        /// The repository within the view model.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// A value indicating whether item is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the ReceiptViewModel class.
        /// </summary>
        /// <param name="receipt">The receipt to be passed.</param>
        /// <param name="repository">The repository of the receipt view model.</param>
        public ReceiptViewModel(Receipt receipt, Repository repository) : base("Receipt")
        {
            this.receipt = receipt;
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
        /// Gets or sets the date of the receipt within the view model.
        /// </summary>
        public string DateIssued
        {
            get
            {
                return this.receipt.DateIssued;
            }

            set
            {
                this.receipt.DateIssued = value;
                this.OnPropertyChanged("DateIssued");
            }
        }

        /// <summary>
        /// Gets or sets the invoice number of the receipt.
        /// </summary>
        public string InvoiceNumber
        {
            get
            {
                return this.receipt.InvoiceNumber;
            }

            set
            {
                this.receipt.InvoiceNumber = value;
                this.OnPropertyChanged("InvoiceNumber");
            }
        }

        /// <summary>
        /// Gets the account name of the user.
        /// </summary>
        public string AccountName
        {
            get
            {
                string game = string.Empty;

                foreach (Account g in this.repository.GetAccounts())
                {
                    if (g.Id == this.receipt.AccountId)
                    {
                        game = g.FirstName + " " + g.LastName;

                        break;
                    }
                }

                return game;
            }
        }

        /// <summary>
        /// Gets or sets the payment method of the receipt.
        /// </summary>
        public string PaymentMethod
        {
            get
            {
                return this.receipt.PaymentMethod;
            }

            set
            {
                this.receipt.PaymentMethod = value;
                this.OnPropertyChanged("PaymentMethod");
            }
        }

        /// <summary>
        /// Gets the price of the game.
        /// </summary>
        public decimal Price
        {
            get
            {
                return this.receipt.TransactionAmount;
            }
        }

        /// <summary>
        /// Gets the game title in the receipt.
        /// </summary>
        public string GameTitle
        {
            get
            {
                string game = string.Empty;

                foreach (Game g in this.repository.GetGames())
                {
                    if (g.Id == this.receipt.GameId)
                    {
                        game = g.Name;

                        break;
                    }
                }

                return game;
            }
        }

        /// <summary>
        /// Method to add the receipt.
        /// </summary>
        public void Save()
        {
            this.repository.AddReceipt(this.receipt);

            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// Writes a receipt to the text file.
        /// </summary>
        /// <returns>String to write the txt file.</returns>
        public string PrintReceipt()
        {
            string receiptDesktop = this.InvoiceNumber + ".txt";

            using (var receipt = new StreamWriter(receiptDesktop, true))
            {
                receipt.WriteLine("Invoice Number: " + this.InvoiceNumber);
                receipt.WriteLine("Date of Transaction: " + DateTime.Now.ToShortDateString());
                receipt.WriteLine("Account: " + this.AccountName);
                receipt.WriteLine("Card Type: " + this.PaymentMethod);
                receipt.WriteLine("Transaction Amount: " + this.Price);
                receipt.WriteLine("Game Title: " + this.GameTitle);
                receipt.WriteLine(" ");
            }

            MessageBox.Show("Your Receipt has been printed to your desktop! Enjoy your game!" + Environment.NewLine + Environment.NewLine + " Receipt Number: " + this.InvoiceNumber);

            return receiptDesktop;
        }

        /// <summary>
        /// Creates a new command for the product view.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("Print Receipt", new DelegateCommand(p => this.PrintReceipt())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
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