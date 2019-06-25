namespace SaftStopDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// The migration for moving over our database from the initializer.
    /// </summary>
    public partial class InitialCreate : DbMigration
    {
        /// <summary>
        /// The function that is used to complete the migration.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        CreditCardNumber = c.String(),
                        AccountFunds = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.FriendId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PublisherId = c.Int(nullable: false),
                        DeveloperId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsedQuantity = c.Int(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Developers", t => t.DeveloperId)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.PublisherId)
                .Index(t => t.DeveloperId)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateIssued = c.String(),
                        InvoiceNumber = c.String(),
                        AccountId = c.Int(nullable: false),
                        PaymentMethod = c.String(),
                        TransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .Index(t => t.AccountId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.TransactionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.String(),
                        BuyerAccountId_Id = c.Int(),
                        SellerAccountId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.BuyerAccountId_Id)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Accounts", t => t.SellerAccountId_Id)
                .Index(t => t.GameId)
                .Index(t => t.BuyerAccountId_Id)
                .Index(t => t.SellerAccountId_Id);
        }
        
        /// <summary>
        /// The function that is used to undo the migration.
        /// </summary>
        public override void Down()
        {
            this.DropForeignKey("dbo.TransactionDetails", "SellerAccountId_Id", "dbo.Accounts");
            this.DropForeignKey("dbo.TransactionDetails", "GameId", "dbo.Games");
            this.DropForeignKey("dbo.TransactionDetails", "BuyerAccountId_Id", "dbo.Accounts");
            this.DropForeignKey("dbo.Receipts", "GameId", "dbo.Games");
            this.DropForeignKey("dbo.Receipts", "AccountId", "dbo.Accounts");
            this.DropForeignKey("dbo.Games", "Account_Id", "dbo.Accounts");
            this.DropForeignKey("dbo.Games", "PublisherId", "dbo.Publishers");
            this.DropForeignKey("dbo.Games", "DeveloperId", "dbo.Developers");
            this.DropForeignKey("dbo.Friends", "FriendId", "dbo.Accounts");
            this.DropIndex("dbo.TransactionDetails", new[] { "SellerAccountId_Id" });
            this.DropIndex("dbo.TransactionDetails", new[] { "BuyerAccountId_Id" });
            this.DropIndex("dbo.TransactionDetails", new[] { "GameId" });
            this.DropIndex("dbo.Receipts", new[] { "GameId" });
            this.DropIndex("dbo.Receipts", new[] { "AccountId" });
            this.DropIndex("dbo.Games", new[] { "Account_Id" });
            this.DropIndex("dbo.Games", new[] { "DeveloperId" });
            this.DropIndex("dbo.Games", new[] { "PublisherId" });
            this.DropIndex("dbo.Friends", new[] { "FriendId" });
            this.DropTable("dbo.TransactionDetails");
            this.DropTable("dbo.Receipts");
            this.DropTable("dbo.Publishers");
            this.DropTable("dbo.Developers");
            this.DropTable("dbo.Games");
            this.DropTable("dbo.Friends");
            this.DropTable("dbo.Accounts");
        }
    }
}
