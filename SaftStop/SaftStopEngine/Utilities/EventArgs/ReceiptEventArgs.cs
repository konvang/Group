namespace SaftStopEngine
{
    /// <summary>
    /// The class which is used to collect event arguments for the receipt.
    /// </summary>
    public class ReceiptEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ReceiptEventArgs class.
        /// </summary>
        /// <param name="receipt">The receipt to act upon.</param>
        public ReceiptEventArgs(Receipt receipt)
        {
            this.Receipt = receipt;
        }

        /// <summary>
        /// Gets the receipt.
        /// </summary>
        public Receipt Receipt { get; private set; }
    }
}