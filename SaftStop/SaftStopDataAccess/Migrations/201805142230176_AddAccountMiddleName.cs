namespace SaftStopDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// A migration to add a middle name to an account.
    /// </summary>
    public partial class AddAccountMiddleName : DbMigration
    {
        /// <summary>
        /// The function that is used to complete the migration.
        /// </summary>
        public override void Up()
        {
            this.AddColumn("dbo.Accounts", "MiddleName", c => c.String());
        }

        /// <summary>
        /// The function that is used to complete the migration.
        /// </summary>
        public override void Down()
        {
            this.DropColumn("dbo.Accounts", "MiddleName");
        }
    }
}
