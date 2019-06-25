namespace SaftStopDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditFriendMoel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "FriendId", "dbo.Accounts");
            DropIndex("dbo.Friends", new[] { "FriendId" });
            AddColumn("dbo.Friends", "Username", c => c.String());
            AddColumn("dbo.Friends", "Account_Id", c => c.Int());
            CreateIndex("dbo.Friends", "Account_Id");
            AddForeignKey("dbo.Friends", "Account_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Friends", new[] { "Account_Id" });
            DropColumn("dbo.Friends", "Account_Id");
            DropColumn("dbo.Friends", "Username");
            CreateIndex("dbo.Friends", "FriendId");
            AddForeignKey("dbo.Friends", "FriendId", "dbo.Accounts", "Id");
        }
    }
}
