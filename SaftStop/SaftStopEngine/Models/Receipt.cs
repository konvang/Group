namespace SaftStopEngine
{
    /// <summary>
    /// A class used to represent a receipt.
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Gets or sets the Id of the receipt.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date of purchase.
        /// </summary>
        public string DateIssued { get; set; }

        /// <summary>
        /// Gets or sets the KONFIRMATION number.
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the account on the receipt.
        /// </summary>
        public virtual Account AccountName { get; set; }

        /// <summary>
        /// Gets or sets the account id of the user.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the payment method on the receipt.
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        public decimal TransactionAmount { get; set; }

        /// <summary>
        /// Gets or sets the game title on the receipt.
        /// </summary>
        public virtual Game GameTitle { get; set; }

        /// <summary>
        /// Gets or sets the game id in the receipt.
        /// </summary>
        public int GameId { get; set; }
    }
}