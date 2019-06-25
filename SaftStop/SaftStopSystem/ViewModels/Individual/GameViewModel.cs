using System;
using System.Collections.Generic;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// A class used to represent the GameViewModel.
    /// </summary>
    public class GameViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The game within the view model.
        /// </summary>
        private Game game;

        /// <summary>
        /// The repository within the view model.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// A value indicating whether item is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the GameViewModel class.
        /// </summary>
        /// <param name="game">The game to be passed.</param>
        /// <param name="repository">The repository of the Game view model.</param>
        public GameViewModel(Game game, Repository repository) : base("Game")
        {
            this.game = game;
            this.repository = repository;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the is selected value.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        public string Name
        {
            get
            {
                return this.game.Name;
            }

            set
            {
                this.game.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the publisher of the game.
        /// </summary>
        public Publisher Publisher
        {
            get
            {
                return this.game.Publisher;
            }

            set
            {
                this.game.Publisher = value;
                this.OnPropertyChanged("Publisher");
            }
        }

        /// <summary>
        /// Gets the list of publishers.
        /// </summary>
        public IEnumerable<Publisher> Publishers
        {
            get
            {
                return this.repository.GetPublishers();
            }
        }

        /// <summary>
        /// Gets or sets the developer of the game.
        /// </summary>
        public Developer Developer
        {
            get
            {
                return this.game.Developer;
            }

            set
            {
                this.game.Developer = value;
                this.OnPropertyChanged("Developer");
            }
        }

        /// <summary>
        /// Gets the list of developers.
        /// </summary>
        public IEnumerable<Developer> Developers
        {
            get
            {
                return this.repository.GetDevelopers();
            }
        }

        /// <summary>
        /// Gets or sets the price of the game.
        /// </summary>
        public decimal Price
        {
            get
            {
                return this.game.Price;
            }

            set
            {
                this.game.Price = value;
                this.OnPropertyChanged("Price");
            }
        }

        /// <summary>
        /// Gets or sets the used price of the game.
        /// </summary>
        public decimal UsedPrice
        {
            get
            {
                return this.game.UsedPrice;
            }

            set
            {
                this.game.UsedPrice = value;
                this.OnPropertyChanged("UsedPrice");
            }
        }

        /// <summary>
        /// Gets the sell price of the game.
        /// </summary>
        public decimal SellPrice
        {
            get
            {
                return Math.Round(this.UsedPrice * .75m, 2);
            }
        }

        /// <summary>
        /// Gets or sets the used price of the game.
        /// </summary>
        public int UsedQuantity
        {
            get
            {
                return this.game.UsedQuantity;
            }

            set
            {
                this.game.UsedQuantity = value;
                this.OnPropertyChanged("UsedQuantity");
            }
        }

        /// <summary>
        /// Gets the game for the view model.
        /// </summary>
        public Game Game
        {
            get
            {
                return this.game;
            }
        }

        /// <summary>
        /// Method to add the game.
        /// </summary>
        public void Save()
        {
            this.repository.AddGame(this.game);

            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// Creates a new command for the product view.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("OK", new DelegateCommand(p => this.OkExecute())));

            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Execute the okay command.
        /// </summary>
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Execute the cancel command.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}
