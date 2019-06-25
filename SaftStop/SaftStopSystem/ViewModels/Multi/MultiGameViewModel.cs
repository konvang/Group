using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// A class used to represent the multigame.
    /// </summary>
    public class MultiGameViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The repository of data.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// The account that is currently logged in.
        /// </summary>
        private Account account;

        /// <summary>
        /// The store specific commands.
        /// </summary>
        private ObservableCollection<CommandViewModel> storeCommands = new ObservableCollection<CommandViewModel>();

        /// <summary>
        /// The library specific commands.
        /// </summary>
        private ObservableCollection<CommandViewModel> libraryCommands = new ObservableCollection<CommandViewModel>();

        /// <summary>
        /// Initializes a new instance of the MultiGameViewModel class.
        /// </summary>
        /// <param name="repository">The repository of data.</param>
        /// <param name="account">The account of the user. If admin, will show all games so they can be edited.</param>
        /// <param name="name">The name of the multi view model.</param>
        public MultiGameViewModel(Repository repository, Account account, string name) : base(name)
        {
            this.account = account;

            this.repository = repository;

            this.CreateCommands();

            this.repository.GameAdded += this.OnGameAdded;

            this.AllGames = new ObservableCollection<GameViewModel>();

            List<GameViewModel> games = this.GetAllGames();

            this.AllGames = new ObservableCollection<GameViewModel>(games);
        }

        /// <summary>
        /// Gets all the games.
        /// </summary>
        public ObservableCollection<GameViewModel> AllGames { get; private set; }

        /// <summary>
        /// Gets all the user's games.
        /// </summary>
        public ObservableCollection<GameViewModel> UserGames
        {
            get
            {
                List<GameViewModel> games =
                    (from d in this.account.Library
                     select new GameViewModel(d, this.repository)).ToList();

                games.ForEach(cvm => cvm.PropertyChanged += this.OnUserGameViewModelPropertyChanged);

                if (this is LibraryViewModel)
                {
                    this.AllGames = new ObservableCollection<GameViewModel>(games);
                }

                return new ObservableCollection<GameViewModel>(games);
            }
        }

        /// <summary>
        /// Gets the number of items selected.
        /// </summary>
        public int NumberOfItemsSelected
        {
            get
            {
                if (this is LibraryViewModel)
                {
                    return this.AllGames.Count(vm => vm.IsSelected);
                }
                else
                {
                    return this.AllGames.Count(vm => vm.IsSelected);
                }
            }
        }

        /// <summary>
        /// Gets the library commands.
        /// </summary>
        public ObservableCollection<CommandViewModel> LibraryCommands
        {
            get
            {
                return this.libraryCommands;
            }
        }

        /// <summary>
        /// Gets the store commands.
        /// </summary>
        public ObservableCollection<CommandViewModel> StoreCommands
        {
            get
            {
                return this.storeCommands;
            }
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        protected override void CreateCommands()
        {
            if (this.account != null)
            {
                this.LibraryCommands.Add(new CommandViewModel("Sell", new DelegateCommand(p => this.SellGame(), p => this.NumberOfItemsSelected == 1)));

                if (this.account.IsAdmin == true)
                {
                    this.StoreCommands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewGameExecute(), p => this.account.IsAdmin == true)));
                    this.StoreCommands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditGameExecute(), p => this.NumberOfItemsSelected == 1 && this.account.IsAdmin == true)));
                }

                this.StoreCommands.Add(new CommandViewModel("Buy New", new DelegateCommand(p => this.BuyGame("New"), p => this.NumberOfItemsSelected == 1 && !this.account.Library.Contains(this.AllGames.SingleOrDefault(vm => vm.IsSelected).Game))));
                this.StoreCommands.Add(new CommandViewModel("Buy Used", new DelegateCommand(p => this.BuyGame("Used"), p => this.NumberOfItemsSelected == 1 && this.AllGames.SingleOrDefault(vm => vm.IsSelected).UsedQuantity > 0 && !this.account.Library.Contains(this.AllGames.SingleOrDefault(vm => vm.IsSelected).Game))));
            }
        }

        /// <summary>
        /// Updates the views.
        /// </summary>
        private void UpdateViews()
        {
            this.OnPropertyChanged("AllGames");
            this.OnPropertyChanged("UserGames");
        }

        /// <summary>
        /// On game added.
        /// </summary>
        /// <param name="sender">A Button click.</param>
        /// <param name="e">The click of the button.</param>
        private void OnGameAdded(object sender, GameEventArgs e)
        {
            GameViewModel viewModel = new GameViewModel(e.Game, this.repository);
            this.AllGames.Add(viewModel);
        }

        /// <summary>
        /// On the game view model property changed.
        /// </summary>
        /// <param name="sender">The view that was changed.</param>
        /// <param name="e">The event that .</param>
        private void OnGameViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        /// <summary>
        /// On the game view model property changed.
        /// </summary>
        /// <param name="sender">The view that was changed.</param>
        /// <param name="e">The event that .</param>
        private void OnUserGameViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        /// <summary>
        /// Shows the game in the view.
        /// </summary>
        /// <param name="viewModel">The view model to show.</param>
        private void ShowGame(GameViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            GameView view = new GameView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        /// <summary>
        /// Edit and execute.
        /// </summary>
        private void EditGameExecute()
        {
            GameViewModel viewModel = this.AllGames.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                this.ShowGame(viewModel);
                this.repository.SaveToDatabase();
            }
            else
            {
                MessageBox.Show("Please select only one game.");
            }
        }

        /// <summary>
        /// Purchase a game.
        /// </summary>
        /// <param name="quality">The quality of the game.</param>
        private void BuyGame(string quality)
        {
            GameViewModel viewModel = this.AllGames.SingleOrDefault(vm => vm.IsSelected);

            if (viewModel != null)
            {
                switch (quality)
                {
                    case "New":
                        if (this.account.AccountFunds > viewModel.Price)
                        {
                            MessageBoxResult result = MessageBox.Show("Are you sure you want to buy this game?", "Purchase confirmation", MessageBoxButton.YesNo);

                            if (result == MessageBoxResult.Yes)
                            {
                                if (this.account.Library.Contains(viewModel.Game))
                                {
                                    MessageBox.Show("You already own this game!");
                                    break;
                                }

                                this.account.AccountFunds -= viewModel.Price;
                                this.account.Library.Add(viewModel.Game);
                                this.UserGames.Add(viewModel);
                                Receipt receipt = new Receipt
                                {
                                    AccountId = this.account.Id,
                                    GameId = viewModel.Game.Id,
                                    DateIssued = DateTime.Now.ToShortDateString(),
                                    TransactionAmount = viewModel.Game.Price,
                                    InvoiceNumber = Guid.NewGuid().ToString(),
                                    PaymentMethod = "VISA"
                                };

                                this.repository.AddReceipt(receipt);

                                ReceiptViewModel receiptViewModel = new ReceiptViewModel(receipt, this.repository);
                                this.ShowReceipt(receiptViewModel);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Account funds unavailable.");
                        }

                        break;
                    case "Used":
                        if (this.account.AccountFunds > viewModel.UsedPrice && viewModel.UsedQuantity > 0)
                        {
                            MessageBoxResult result = MessageBox.Show("Are you sure you want to buy this game?", "Purchase confirmation", MessageBoxButton.YesNo);

                            if (result == MessageBoxResult.Yes)
                            {
                                if (this.account.Library.Contains(viewModel.Game))
                                {
                                    MessageBox.Show("You already own this game!");
                                    break;
                                }

                                this.account.AccountFunds -= viewModel.UsedPrice;
                                this.account.Library.Add(viewModel.Game);
                                this.UserGames.Add(viewModel);
                                viewModel.UsedQuantity--;

                                Receipt receipt = new Receipt
                                {
                                    AccountId = this.account.Id,
                                    GameId = viewModel.Game.Id,
                                    DateIssued = DateTime.Now.ToShortDateString(),
                                    TransactionAmount = viewModel.Game.UsedPrice,
                                    InvoiceNumber = Guid.NewGuid().ToString(),
                                    PaymentMethod = "VISA"
                                };

                                this.repository.AddReceipt(receipt);

                                ReceiptViewModel receiptViewModel = new ReceiptViewModel(receipt, this.repository);
                                this.ShowReceipt(receiptViewModel);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Account funds unavailable.");
                        }

                        break;
                }

                this.repository.SaveToDatabase();
                this.UpdateViews();
            }
        }

        /// <summary>
        /// Shows the item in the view.
        /// </summary>
        /// <param name="viewModel">The view model to show.</param>
        private void ShowReceipt(ReceiptViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Height = 300;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            ReceiptView view = new ReceiptView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        /// <summary>
        /// A method to sell games within the view model.
        /// </summary>
        private void SellGame()
        {
            GameViewModel viewModel = this.AllGames.SingleOrDefault(vm => vm.IsSelected);

            if (viewModel != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to sell this game?", "Purchase confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Receipt receipt = new Receipt
                    {
                        AccountId = this.account.Id,
                        GameId = viewModel.Game.Id,
                        DateIssued = DateTime.Now.ToShortDateString(),
                        TransactionAmount = viewModel.SellPrice,
                        InvoiceNumber = Guid.NewGuid().ToString(),
                        PaymentMethod = "Account Funds Added To Account"
                    };

                    this.repository.AddReceipt(receipt);

                    this.account.AccountFunds += viewModel.SellPrice;
                    this.account.Library.Remove(viewModel.Game);
                    this.UserGames.Remove(viewModel);

                    List<GameViewModel> games = this.GetAllGames();

                    foreach (GameViewModel g in games)
                    {
                        if (viewModel.Name == g.Name)
                        {
                            viewModel.UsedQuantity++;
                            this.repository.SaveToDatabase();
                        }
                    }

                    this.UpdateViews();

                    ReceiptViewModel receiptViewModel = new ReceiptViewModel(receipt, this.repository);
                    this.ShowReceipt(receiptViewModel);
                }
            }
        }

        /// <summary>
        /// Gets all the games.
        /// </summary>
        /// <returns>ALL THE GAMES.</returns>
        private List<GameViewModel> GetAllGames()
        {
            List<GameViewModel> games =
                (from d in this.repository.GetGames()
                 select new GameViewModel(d, this.repository)).ToList();

            games.ForEach(cvm => cvm.PropertyChanged += this.OnGameViewModelPropertyChanged);

            return games;
        }

        /// <summary>
        /// Creates new game.
        /// </summary>
        private void CreateNewGameExecute()
        {
            GameViewModel viewModel = new GameViewModel(new Game(), this.repository);
            this.ShowGame(viewModel);
        }
    }
}