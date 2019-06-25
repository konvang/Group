using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SaftStopEngine;

namespace SaftStopDataAccess
{
    /// <summary>
    /// A class used to represent the saft stop context for the database.
    /// </summary>
    public class SaftStopContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the SaftStopContext class.
        /// </summary>
        public SaftStopContext() : base("SaftStopContext")
        {
        }

        /// <summary>
        /// Gets or sets the stored products.
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// Gets or sets the stored account.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the stored products.
        /// </summary>
        public DbSet<Developer> Developers { get; set; }

        /// <summary>
        /// Gets or sets the stored publishers.
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Gets or sets the stored friends.
        /// </summary>
        public DbSet<Friends> Friends { get; set; }

        /// <summary>
        /// Gets or sets the stored detailed transaction.
        /// </summary>
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        /// <summary>
        /// Gets or sets the stored receipts.
        /// </summary>
        public DbSet<Receipt> Receipts { get; set; }

        /// <summary>
        /// Prevents cascading when removing someone as a friend so their account isn't deleted.
        /// </summary>
        /// <param name="modelBuilder">The model builder used.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}