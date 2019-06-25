using System;
using System.Collections.Generic;
using System.Linq;
using SaftStopEngine;

namespace SaftStopDataAccess
{
    /// <summary>
    /// A class used to represent the repository.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// The saftstop context.
        /// </summary>
        private SaftStopContext context = new SaftStopContext();

        /// <summary>
        /// Event that is triggered on game added.
        /// </summary>
        public event EventHandler<GameEventArgs> GameAdded;

        /// <summary>
        /// Event that is triggered on account added.
        /// </summary>
        public event EventHandler<AccountEventArgs> AccountAdded;

        /// <summary>
        /// Event that is triggered on account added.
        /// </summary>
        public event EventHandler<DeveloperEventArgs> DeveloperAdded;

        /// <summary>
        /// Event that is triggered on publisher added.
        /// </summary>
        public event EventHandler<PublisherEventArgs> PublisherAdded;

        /////// <summary>
        /////// Event that is triggered on friend added.
        /////// </summary>
        ////public event EventHandler<FriendEventArgs> FriendAdded;

        /// <summary>
        /// Event that is triggered on transaction detail added.
        /// </summary>
        public event EventHandler<TransactionDetailEventArgs> TransactionDetailAdded;

        /// <summary>
        /// Event that is triggered on receipt added.
        /// </summary>
        public event EventHandler<ReceiptEventArgs> ReceiptAdded;

        /////// <summary>
        /////// Adds a friend to the list.
        /////// </summary>
        /////// <param name="friends">The friend to add.</param>
        ////public void AddFriend(Friends friends)
        ////{
        ////    if (!this.ContainsFriend(friends))
        ////    {
        ////        this.context.Friends.Add(friends);

        ////        if (this.FriendAdded != null)
        ////        {
        ////            this.FriendAdded(this, new FriendEventArgs(friends));
        ////        }
        ////    }
        ////}

        /// <summary>
        /// Adds a game to the list.
        /// </summary>
        /// <param name="game">The game to add.</param>
        public void AddGame(Game game)
        {
            if (!this.ContainsGame(game))
            {
                this.context.Games.Add(game);

                if (this.GameAdded != null)
                {
                    this.GameAdded(this, new GameEventArgs(game));
                }
            }
        }

        /// <summary>
        /// Adds an account to the list.
        /// </summary>
        /// <param name="account">The account to add.</param>
        public void AddAccount(Account account)
        {
            if (!this.ContainsAccount(account))
            {
                this.context.Accounts.Add(account);
                {
                    if (this.AccountAdded != null)
                    {
                        this.AccountAdded(this, new AccountEventArgs(account));
                    }
                }
            }
        }

        /// <summary>
        /// Adds an developer to the list.
        /// </summary>
        /// <param name="developer">The developer to add.</param>
        public void AddDeveloper(Developer developer)
        {
            if (!this.ContainsDeveloper(developer))
            {
                this.context.Developers.Add(developer);
                {
                    if (this.DeveloperAdded != null)
                    {
                        this.DeveloperAdded(this, new DeveloperEventArgs(developer));
                    }
                }
            }
        }

        /// <summary>
        /// Adds a publisher to the list.
        /// </summary>
        /// <param name="publisher">The publisher to add.</param>
        public void AddPublisher(Publisher publisher)
        {
            if (!this.ContainsPublisher(publisher))
            {
                this.context.Publishers.Add(publisher);
                {
                    if (this.PublisherAdded != null)
                    {
                        this.PublisherAdded(this, new PublisherEventArgs(publisher));
                    }
                }
            }
        }

        /// <summary>
        /// Adds a transaction detail to the list within the database.
        /// </summary>
        /// <param name="transactionDetail">The transaction detail to add.</param>
        public void AddTransactionDetail(TransactionDetail transactionDetail)
        {
            if (!this.ContainsTransactionDetail(transactionDetail))
            {
                this.context.TransactionDetails.Add(transactionDetail);
                {
                    if (this.TransactionDetailAdded != null)
                    {
                        this.TransactionDetailAdded(this, new TransactionDetailEventArgs(transactionDetail));
                    }
                }
            }
        }

        /// <summary>
        /// Adds a receipt to the list within the database. 
        /// </summary>
        /// <param name="receipt">The receipt to be added.</param>
        public void AddReceipt(Receipt receipt)
        {
            if (!this.ContainsReceipt(receipt))
            {
                this.context.Receipts.Add(receipt);
                {
                    if (this.ReceiptAdded != null)
                    {
                        this.ReceiptAdded(this, new ReceiptEventArgs(receipt));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the list of friends.
        /// </summary>
        /// <returns>Returns the list of friends.</returns>
        public List<Friends> GetFriends()
        {
            return this.context.Friends.ToList();
        }

        /// <summary>
        /// Gets the list of receipts.
        /// </summary>
        /// <returns>The list of receipts to return.</returns>
        public List<Receipt> GetReceipts()
        {
            return this.context.Receipts.ToList();
        }

        /// <summary>
        /// Get the list of games.
        /// </summary>
        /// <returns>Returns the list of games.</returns>
        public List<Game> GetGames()
        {
            return this.context.Games.ToList();
        }

        /// <summary>
        /// Get the list of Developers.
        /// </summary>
        /// <returns>Returns the list of Developers.</returns>
        public List<Developer> GetDevelopers()
        {
            return this.context.Developers.ToList();
        }

        /// <summary>
        /// Get the list of Publishers.
        /// </summary>
        /// <returns>Returns the list of publishers.</returns>
        public List<Publisher> GetPublishers()
        {
            return this.context.Publishers.ToList();
        }

        /// <summary>
        /// Get the list of account.
        /// </summary>
        /// <returns>Returns the list of account.</returns>
        public List<Account> GetAccounts()
        {
            return this.context.Accounts.ToList();
        }

        /// <summary>
        /// Gets the list of the transaction details.
        /// </summary>
        /// <returns>The desired transaction detail.</returns>
        public List<TransactionDetail> GetTransactionDetails()
        {
            return this.context.TransactionDetails.ToList();
        }

        /// <summary>
        /// Saves the changes made to the database.
        /// </summary>
        public void SaveToDatabase()
        {
            this.context.SaveChanges();
        }

        /////// <summary>
        /////// Saves the changes made to the database.
        /////// </summary>
        /////// <param name="id">The id of the entry desired.</param>
        /////// <returns>The desired friend.</returns>
        ////private Friends GetFriend(int id)
        ////{
        ////    return this.context.Friends.Find(id);
        ////}

        /// <summary>
        /// Gets a game from the database by id.
        /// </summary>
        /// <param name="id">The id of the entry desired.</param>
        /// <returns>The desired game.</returns>
        private Game GetGame(int id)
        {
            return this.context.Games.Find(id);
        }

        /// <summary>
        /// Gets a game from the database by id.
        /// </summary>
        /// <param name="id">The id of the entry desired.</param>
        /// <returns>The desired game.</returns>
        private Developer GetDeveloper(int id)
        {
            return this.context.Developers.Find(id);
        }

        /// <summary>
        /// Gets a publisher from the database by id.
        /// </summary>
        /// <param name="id">The id of the entry desired.</param>
        /// <returns>The desired publisher.</returns>
        private Publisher GetPublisher(int id)
        {
            return this.context.Publishers.Find(id);
        }

        /// <summary>
        /// Gets the receipt from the database by id.
        /// </summary>
        /// <param name="id">The id of the receipt.</param>
        /// <returns>The desired receipt.</returns>
        private Receipt GetReceipt(int id)
        {
            return this.context.Receipts.Find(id);
        }

        /// <summary>
        /// Gets the account from the database by id.
        /// </summary>
        /// <param name="id">The id of the entry desired.</param>
        /// <returns>The desired account.</returns>
        private Account GetAccount(int id)
        {
            return this.context.Accounts.Find(id);
        }

        /// <summary>
        /// Gets the transaction detail from the database by id.
        /// </summary>
        /// <param name="id">The id of the entry.</param>
        /// <returns>The desired transaction detail.</returns>
        private TransactionDetail GetTransactionDetail(int id)
        {
            return this.context.TransactionDetails.Find(id);
        }

        /// <summary>
        /// Determines whether the passed in receipt is in the list of receipt.
        /// </summary>
        /// <param name="receipt">The receipt id.</param>
        /// <returns>The id of the receipt.</returns>
        private bool ContainsReceipt(Receipt receipt)
        {
            return this.GetReceipt(receipt.Id) != null;
        }

        /// <summary>
        /// Determines whether the passed in transaction detail is in the list of transaction details.
        /// </summary>
        /// <param name="transactionDetail">The transaction detail id.</param>
        /// <returns>The transaction detail.</returns>
        private bool ContainsTransactionDetail(TransactionDetail transactionDetail)
        {
            return this.GetTransactionDetail(transactionDetail.Id) != null;
        }

        /// <summary>
        /// Determines whether passed in game is in the list of games.
        /// </summary>
        /// <param name="game">The game to check.</param>
        /// <returns>A value indicating whether it is.</returns>
        private bool ContainsGame(Game game)
        {
            return this.GetGame(game.Id) != null;
        }

        /// <summary>
        /// Determines whether passed in developer is in the list of developers.
        /// </summary>
        /// <param name="developer">The developer to check.</param>
        /// <returns>A value indicating whether it is.</returns>
        private bool ContainsDeveloper(Developer developer)
        {
            return this.GetDeveloper(developer.Id) != null;
        }

        /// <summary>
        /////// Determines whether passed in friend is in the list of friends.
        /////// </summary>
        /////// <param name="friend">The friend to check.</param>
        /////// <returns>A value indication whether it is.</returns>
        ////private bool ContainsFriend(Friends friend)
        ////{
        ////    return this.GetFriend(friend.Id) != null;
        ////}

        /// <summary>
        /// Determines whether passed in publisher is in the list of publisher.
        /// </summary>
        /// <param name="publisher">The publisher to check.</param>
        /// <returns>A value indicating whether it is.</returns>
        private bool ContainsPublisher(Publisher publisher)
        {
            return this.GetPublisher(publisher.Id) != null;
        }

        /// <summary>
        /// Determines whether passed in account is in the list of accounts.
        /// </summary>
        /// <param name="account">The account to check.</param>
        /// <returns>A value indicating whether it is.</returns>
        private bool ContainsAccount(Account account)
        {
            return this.GetAccount(account.Id) != null;
        }
    }
}