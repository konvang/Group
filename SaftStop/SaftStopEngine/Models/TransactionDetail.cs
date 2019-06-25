namespace SaftStopEngine
{
    /// <summary>
    /// A class used to represent the detail of the transaction.
    /// </summary>
    public class TransactionDetail
    {
        /// <summary>
        /// Gets or sets the Id number of the transaction detail.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the game within the transaction detail.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the game in the transaction detail.
        /// </summary>
        public Game Game { get; set; }

        /// <summary>
        /// Gets or sets the game seller within the account detail.
        /// </summary>
        public Account SellerAccountId { get; set; }

        /// <summary>
        /// Gets or sets the account that purchased the game within the account detail.
        /// </summary>
        public Account BuyerAccountId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game is new.
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets or sets the sale price of the game within the transaction detail.
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// Gets or sets the date of the transaction detail.
        /// </summary>
        public string Date { get; set; }
    }
}