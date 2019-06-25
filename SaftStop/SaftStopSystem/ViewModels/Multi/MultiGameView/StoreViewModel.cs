using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// The class which is used to hold the logic for the store view.
    /// </summary>
    public class StoreViewModel : MultiGameViewModel
    {
        /////// <summary>
        /////// The database for the multiple product view model.
        /////// </summary>
        ////private Repository repository;

        ////private CollectionViewSource gameViewSource;

        ////private List<GameViewModel> allGamesSorted;
        
        ////private ObservableCollection<GameViewModel> displayedGames;

        /// <summary>
        /// Initializes a new instance of the StoreViewModel class.
        /// </summary>
        /// <param name="repository">The repository of data.</param>
        /// <param name="account">The account of the user. If admin, will show all games so they can be edited.</param>
        public StoreViewModel(Repository repository, Account account) : base(repository, account, "Store")
        {
          ////  this.repository = repository;
          ////  this.repository.GameAdded += this.OnProductAdded;

          ////  List<GameViewModel> games=
          ////(from p in this.repository.GetGames()
          //// select new GameViewModel(p, this.repository)).ToList();
          ////  this.allGamesSorted = new List<GameViewModel>(games);
          ////  games.ForEach(pvm => pvm.PropertyChanged += this.OnGameViewModelPropertyChanged);

          ////  this.displayedGames = new ObservableCollection<GameViewModel>();
          ////  this.AllGames = new ObservableCollection<GameViewModel>(games);

          ////  this.Pager = new PagingViewModel(games.Count());
          ////  this.Pager.CurrentPageChanged += this.OnPageChanged;
          ////  this.RebuildPageData();

          ////  this.gameViewSource = new CollectionViewSource();
          ////  this.gameViewSource.Source = this.DisplayedGames;
        }

        ////public ObservableCollection<GameViewModel> AllGames { get; set; }

        ////public ObservableCollection<GameViewModel> DisplayedGames
        ////{
        ////    get
        ////    {
        ////        return this.displayedGames;
        ////    }

        ////    set
        ////    {
        ////        this.displayedGames = value;
        ////        this.gameViewSource = new CollectionViewSource();
        ////        this.gameViewSource.Source = this.displayedGames;
        ////        this.OnPropertyChanged("DisplayedGames");
        ////    }
        ////}

        /////// <summary>
        /////// The pager of the store.
        /////// </summary>
        ////public PagingViewModel Pager { get; private set; }

        ////public void RebuildPageData()
        ////{
        ////    this.DisplayedGames.Clear();
        ////    var index = this.Pager.PageSize * (this.Pager.CurrentPage - 1);
        ////    this.Pager.ItemCount = this.AllGames.Count();

        ////    IEnumerable<GameViewModel> games = this.AllGames.Skip(this.Pager.CurrentPageStartIndex).Take(this.Pager.PageSize);

        ////    foreach (GameViewModel p in games)
        ////    {
        ////        this.DisplayedGames.Add(p);
        ////    }
        ////}

        /////// <summary>
        /////// The product to be added within the view model.
        /////// </summary>
        /////// <param name="sender">The object sender.</param>
        /////// <param name="e">The product event arguments.</param>
        ////private void OnProductAdded(object sender, GameEventArgs e)
        ////{
        ////    GameViewModel viewModel = new GameViewModel(e.Game, this.repository);
        ////    viewModel.PropertyChanged += this.OnGameViewModelPropertyChanged;
        ////    this.AllGames.Add(viewModel);
        ////}

        /////// <summary>
        /////// Property change when invoked to the bindings.
        /////// </summary>
        /////// <param name="sender">The object sender.</param>
        /////// <param name="e">The arguments.</param>
        ////private void OnGameViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        ////{
        ////    if (e.PropertyName == "IsSelected")
        ////    {
        ////        this.OnPropertyChanged("NumberOfItemsSelected");
        ////    }
        ////}

        ////private void OnPageChanged(object sender, CurrentPageChangeEventArgs e)
        ////{
        ////    this.RebuildPageData();
        ////}
    }
}