namespace SaftStopEngine
{
    /// <summary>
    /// A class used to represent the Transaction Detail Event Arguments.
    /// </summary>
    public class TransactionDetailEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the TransactionDetailEventArgs class.
        /// </summary>
        /// <param name="transactionDetail">The transaction to act upon.</param>
        public TransactionDetailEventArgs(TransactionDetail transactionDetail)
        {
            this.TransactionDetail = transactionDetail;
        }

        /// <summary>
        /// Gets the transaction detail.
        /// </summary>
        public TransactionDetail TransactionDetail { get; private set; }
    }
}