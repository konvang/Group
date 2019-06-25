using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// The class which is used to represent the MainWindowViewModel.
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The repository of data.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// The account that is being currently accessed by the main window.
        /// </summary>
        private Account account;

        /// <summary>
        /// The main window of the program.
        /// </summary>
        private MainWindow mainWindow;

        /// <summary>
        /// The observable collection of view models.
        /// </summary>
        private ObservableCollection<WorkspaceViewModel> workspaceViewModel;

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class.
        /// </summary>
        /// <param name="repository">The repository for the main window.</param>
        /// <param name="account">The account in use by the main window.</param>
        /// <param name="mainWindow">The main window used to close when a user logs out.</param>
        public MainWindowViewModel(Repository repository, Account account, MainWindow mainWindow)
            : base("SaftStop")
        {
            this.account = account;
            this.repository = repository;
            this.mainWindow = mainWindow;

            if (this.account.IsAdmin == false)
            {
                this.ShowStore();
                this.ShowLibrary();
                this.ShowAccount();
                this.ShowAllFriends();
            }
        }

        /// <summary>
        /// Gets the observable collection of view models.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> ViewModels
        {
            get
            {
                if (this.workspaceViewModel == null)
                {
                    this.workspaceViewModel = new ObservableCollection<WorkspaceViewModel>();
                }

                return this.workspaceViewModel;
            }
        }

        /// <summary>
        /// Shows the accounts library.
        /// </summary>
        public void ShowLibrary()
        {
            LibraryViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is LibraryViewModel) as LibraryViewModel;

            if (viewModel == null)
            {
                viewModel = new LibraryViewModel(this.repository, this.account);

                if (this.account.IsAdmin == true)
                {
                    viewModel.RequestClose += this.OnWorkspaceRequestClose;
                }

                this.ViewModels.Add(viewModel);
            }

            this.ActivateViewModel(viewModel);
        }

        /// <summary>
        /// Show all Friends.
        /// </summary>
        public void ShowAllFriends()
        {
            MultiFriendViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiFriendViewModel) as MultiFriendViewModel;

            if (viewModel == null)
            {
                viewModel = new MultiFriendViewModel(this.repository, this.account);

                if (this.account.IsAdmin == true)
                {
                    viewModel.RequestClose += this.OnWorkspaceRequestClose;
                }

                this.ViewModels.Add(viewModel);
            }

            this.ActivateViewModel(viewModel);
        }

        /// <summary>
        /// Shows the account that is currently logged in.
        /// </summary>
        public void ShowAccount()
        {
            AccountViewModel vm = new AccountViewModel(this.account, this.repository, this.mainWindow);

            if (this.account.IsAdmin == true)
            {
                vm.RequestClose += this.OnWorkspaceRequestClose;
            }

            this.ViewModels.Add(vm);

            this.ActivateViewModel(vm);
        }

        /// <summary>
        /// Show all products.
        /// </summary>
        public void ShowAllDevelopers()
        {
            MultiDeveloperViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiDeveloperViewModel) as MultiDeveloperViewModel;

            if (viewModel == null)
            {
                viewModel = new MultiDeveloperViewModel(this.repository, this.account);

                if (this.account.IsAdmin == true)
                {
                    viewModel.RequestClose += this.OnWorkspaceRequestClose;
                }

                this.ViewModels.Add(viewModel);
            }

            this.ActivateViewModel(viewModel);
        }

        /// <summary>
        /// Show all Publishers.
        /// </summary>
        public void ShowAllPublishers()
        {
            MultiPublisherViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is MultiPublisherViewModel) as MultiPublisherViewModel;

            if (viewModel == null)
            {
                viewModel = new MultiPublisherViewModel(this.repository, this.account);

                if (this.account.IsAdmin == true)
                {
                    viewModel.RequestClose += this.OnWorkspaceRequestClose;
                }

                this.ViewModels.Add(viewModel);
            }

            this.ActivateViewModel(viewModel);
        }

        /// <summary>
        /// Show all games.
        /// </summary>
        public void ShowStore()
        {
            StoreViewModel viewModel = this.ViewModels.FirstOrDefault(vm => vm is StoreViewModel) as StoreViewModel;

            if (viewModel == null)
            {
                viewModel = new StoreViewModel(this.repository, this.account);

                if (this.account.IsAdmin == true)
                {
                    viewModel.RequestClose += this.OnWorkspaceRequestClose;
                }

                this.ViewModels.Add(viewModel);
            }

            this.ActivateViewModel(viewModel);
        }

        /// <summary>
        /// Creates the commands for creating new tabs.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("List of developers", new DelegateCommand(p => this.ShowAllDevelopers())));
            this.Commands.Add(new CommandViewModel("Show Store", new DelegateCommand(p => this.ShowStore())));
            this.Commands.Add(new CommandViewModel("List of publishers", new DelegateCommand(p => this.ShowAllPublishers())));
            this.Commands.Add(new CommandViewModel("List of friends", new DelegateCommand(p => this.ShowAllFriends())));
            this.Commands.Add(new CommandViewModel("Show account", new DelegateCommand(p => this.ShowAccount())));
            this.Commands.Add(new CommandViewModel("Show Library", new DelegateCommand(p => this.ShowLibrary())));
        }

        /// <summary>
        /// Sets the active view model to the passed in view model.
        /// </summary>
        /// <param name="workspaceViewModel">The view model to set to active.</param>
        private void ActivateViewModel(WorkspaceViewModel workspaceViewModel)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.workspaceViewModel);

            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(workspaceViewModel);
            }
        }

        /////// <summary>
        /////// Closes the main window.
        /////// </summary>
        ////private void CloseMainWindow()
        ////{
        ////    this.mainWindow.Close();
        ////}

        /// <summary>
        /// Actions taken when clicking close.
        /// </summary>
        /// <param name="sender">The button click.</param>
        /// <param name="e">The click of the button.</param>
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            this.ViewModels.Remove(sender as WorkspaceViewModel);
        }

        ////public static void LogOut()
        ////{
        ////    MessageBoxResult m = MessageBox.Show("Are you sure you want to log out?", "Log out", MessageBoxButton.YesNo);
        ////    if (m == MessageBoxResult.Yes)
        ////    {
        ////        LoginViewModel viewModel = new LoginViewModel();
        ////        WorkspaceWindow window = new WorkspaceWindow();

        ////        window.Width = 400;
        ////        window.Title = viewModel.DisplayName;

        ////        viewModel.CloseAction = b => window.DialogResult = b;

        ////        LoginView view = new LoginView();
        ////        view.DataContext = viewModel;

        ////        window.Content = view;
        ////        window.ShowDialog();
        ////    }
        ////}
    }
}
