namespace SaftStopDataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// The class which is used to set up the Konfiguration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<SaftStopDataAccess.SaftStopContext>
    {
        /// <summary>
        /// Initializes a new instance of the Configuration class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.ContextKey = "SaftStopDataAccess.SaftStopContext";
        }

        /// <summary>
        /// An override of the seed.
        /// </summary>
        /// <param name="context">The data context.</param>
        protected override void Seed(SaftStopDataAccess.SaftStopContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method 
            // to avoid creating duplicate seed data.
        }
    }
}
