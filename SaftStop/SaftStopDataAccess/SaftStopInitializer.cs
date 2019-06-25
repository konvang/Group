using System.Collections.Generic;
using System.Data.Entity;
using SaftStopEngine;

namespace SaftStopDataAccess
{
    /// <summary>
    /// A class used to represent the saft stop initializer.
    /// </summary>
    public class SaftStopInitializer : DropCreateDatabaseIfModelChanges<SaftStopContext>
    {
        /// <summary>
        /// The seed data for the database.
        /// </summary>
        /// <param name="context">The database to populate with seed data.</param>
        protected override void Seed(SaftStopContext context)
        {
            var accounts = new List<Account>
            {
                new Account { Username = "Admin", FirstName = "Admin", LastName = "Admin", Password = "Password", IsAdmin = true, AccountFunds = 10000.00m },
                new Account { Username = "Konflict", FirstName = "Kon", LastName = "Vang", Password = "Password1", IsAdmin = true, AccountFunds = 100.00m },
                new Account { Username = "Marshmellow", FirstName = "Marcus", LastName = "Fitzgerald", Password = "Password2", IsAdmin = true, AccountFunds = 0.11m },
                new Account { Username = "Trevoring", FirstName = "Trevor", LastName = "Huven", Password = "Password3", IsAdmin = true, AccountFunds = 100.00m },
                new Account { Username = "Random", FirstName = "Null", LastName = "Reference", Password = "random", IsAdmin = false, AccountFunds = 10.00m }
            };

            context.Accounts.AddRange(accounts);

            context.SaveChanges();

            var developers = new List<Developer>
            {
                new Developer { Name = "Nintendo EPD" },
                new Developer { Name = "Psyonix" },
                new Developer { Name = "Nifflas Games" },
                new Developer { Name = "CAPCOM" },
                new Developer { Name = "Ubisoft" },
                new Developer { Name = "MachineGames" },
                new Developer { Name = "Bungie" },
                new Developer { Name = "Sega" },
                new Developer { Name = "BioWare" },
                new Developer { Name = "Team Cherry" },
                new Developer { Name = "Rebellion Developments" },
                new Developer { Name = "Bethesda Softworks" },
                new Developer { Name = "Platinum Games" },
                new Developer { Name = "Overhype Studios" },
                new Developer { Name = "Red Barrels Studios" }
            };

            context.Developers.AddRange(developers);

            context.SaveChanges();

            var publisher = new List<Publisher>
            {
                new Publisher { Name = "Nintendo" },
                new Publisher { Name = "Psyonix" },
                new Publisher { Name = "Raw Fury" },
                new Publisher { Name = "CAPCOM" },
                new Publisher { Name = "Ubisoft" },
                new Publisher { Name = "Bethesda Softworks" },
                new Publisher { Name = "Activision" },
                new Publisher { Name = "Sega" },
                new Publisher { Name = "Electronic Arts" },
                new Publisher { Name = "Team Cherry" },
                new Publisher { Name = "Rebellion Developments" },
                new Publisher { Name = "Square Enix" },
                new Publisher { Name = "Overhype Studios" },
                new Publisher { Name = "Red Barrels Studios" }
            };

            context.Publishers.AddRange(publisher);

            context.SaveChanges();

            var games = new List<Game>
            {
                new Game { Name = "Super Mario Odyssey", PublisherId = 1, DeveloperId = 1, Price = 60.00m, UsedPrice = 40.00m, UsedQuantity = 2 },
                new Game { Name = "Rocket League", PublisherId = 2, DeveloperId = 2, Price = 30.00m, UsedPrice = 18.00m, UsedQuantity = 1 },
                new Game { Name = "The Legend of Zelda", PublisherId = 1, DeveloperId = 1, Price = 60.00m, UsedPrice = 52.00m, UsedQuantity = 1 },
                new Game { Name = "Uurnong Uurnlimited", PublisherId = 3, DeveloperId = 3, Price = 30.00m, UsedPrice = 15.00m, UsedQuantity = 4 },
                new Game { Name = "Okami HD", PublisherId = 4, DeveloperId = 4, Price = 20.00m, UsedPrice = 15.00m, UsedQuantity = 2 },
                new Game { Name = "Assassin’s Creed Origins", PublisherId = 5, DeveloperId = 5, Price = 60.00m, UsedPrice = 40.00m, UsedQuantity = 5 },
                new Game { Name = "Wolfenstien 2: The New Colossus", PublisherId = 6, DeveloperId = 6, Price = 30.00m, UsedPrice = 12.00m, UsedQuantity = 3 },
                new Game { Name = "Destiny 2", PublisherId = 7, DeveloperId = 7, Price = 60.00m, UsedPrice = 15.00m, UsedQuantity = 56 },
                new Game { Name = "Sonic Mania", PublisherId = 8, DeveloperId = 8, Price = 20.00m, UsedPrice = 13.00m, UsedQuantity = 6 },
                new Game { Name = "Mass Effect Andromeda", PublisherId = 9, DeveloperId = 9, Price = 5.00m, UsedPrice = 2.50m, UsedQuantity = 36 },
                new Game { Name = "Hollow Knight", PublisherId = 10, DeveloperId = 10, Price = 20.00m, UsedPrice = 10.00m, UsedQuantity = 2 },
                new Game { Name = "Resident Evil 7", PublisherId = 4, DeveloperId = 4, Price = 30.00m, UsedPrice = 18.99m, UsedQuantity = 7 },
                new Game { Name = "Sniper Elite 4", PublisherId = 11, DeveloperId = 11, Price = 30.00m, UsedPrice = 10.00m, UsedQuantity = 5 },
                new Game { Name = "The Elder Scrolls: Legends", PublisherId = 6, DeveloperId = 12, Price = 0.00m, UsedPrice = 0.00m, UsedQuantity = 7 },
                new Game { Name = "Nier: Automata", PublisherId = 12, DeveloperId = 13, Price = 60.00m, UsedPrice = 30.00m, UsedQuantity = 1 },
                new Game { Name = "Battle Brothers", PublisherId = 13, DeveloperId = 14, Price = 15.00m, UsedPrice = 8.99m, UsedQuantity = 3 },
                new Game { Name = "Bayonetta", PublisherId = 1, DeveloperId = 8, Price = 20.00m, UsedPrice = 15.00m, UsedQuantity = 5 },
                new Game { Name = "Outlast 2", PublisherId = 14, DeveloperId = 15, Price = 30.00m, UsedPrice = 25.00m, UsedQuantity = 7 }
            };

            context.Games.AddRange(games);

            context.SaveChanges();

            var receipts = new List<Receipt>
            {
                new Receipt { AccountId = 1, GameId = 1,  DateIssued = "04/29/2018", InvoiceNumber = "1111111111", PaymentMethod = "VISA" }
            };

            context.Receipts.AddRange(receipts);
            context.SaveChanges();
        }
    }
    }