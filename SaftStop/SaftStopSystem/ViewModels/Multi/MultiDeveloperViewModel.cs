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
    /// A class to represent the multi-developer.
    /// </summary>
    public class MultiDeveloperViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The account that is currently logged in.
        /// </summary>
        private Account account;

        /// <summary>
        /// The repository of data.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// Initializes a new instance of the MultiDeveloperViewModel class.
        /// </summary>
        /// <param name="repository">The repository of data.</param>
        /// <param name="account">The account used in the view model.</param>
        public MultiDeveloperViewModel(Repository repository, Account account) : base("Developers")
        {
            this.account = account;

            this.repository = repository;

            this.CreateCommands();

            this.repository.DeveloperAdded += this.OnDeveloperAdded;

            this.AllDevelopers = new ObservableCollection<DeveloperViewModel>();

            List<DeveloperViewModel> developers =
                (from d in this.repository.GetDevelopers()
                 select new DeveloperViewModel(d, this.repository)).ToList();

            developers.ForEach(cvm => cvm.PropertyChanged += this.OnDeveloperViewModelPropertyChanged);

            this.AllDevelopers = new ObservableCollection<DeveloperViewModel>(developers);
        }

        /// <summary>
        /// Gets all the customers.
        /// </summary>
        public ObservableCollection<DeveloperViewModel> AllDevelopers { get; private set; }

        /// <summary>
        /// Gets the number of items selected.
        /// </summary>
        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllDevelopers.Count(vm => vm.IsSelected);
            }
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        protected override void CreateCommands()
        {
            if (this.account != null)
            {
                if (this.account.IsAdmin == true)
                {
                    this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewDeveloperExecute())));
                    this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditDeveloperExecute())));
                }
            }
        }

        /// <summary>
        /// On developer added.
        /// </summary>
        /// <param name="sender">A Button click.</param>
        /// <param name="e">The click of the button.</param>
        private void OnDeveloperAdded(object sender, DeveloperEventArgs e)
        {
            DeveloperViewModel viewModel = new DeveloperViewModel(e.Developer, this.repository);
            this.AllDevelopers.Add(viewModel);
        }

        /// <summary>
        /// On the developer view model property changed.
        /// </summary>
        /// <param name="sender">The view that was changed.</param>
        /// <param name="e">The event that .</param>
        private void OnDeveloperViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        /// <summary>
        /// Shows the item in the view.
        /// </summary>
        /// <param name="viewModel">The view model to show.</param>
        private void ShowDeveloper(DeveloperViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Height = 150;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            DeveloperView view = new DeveloperView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        /// <summary>
        /// Edit and execute.
        /// </summary>
        private void EditDeveloperExecute()
        {
            DeveloperViewModel viewModel = this.AllDevelopers.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                this.ShowDeveloper(viewModel);
                this.repository.SaveToDatabase();
            }
            else
            {
                MessageBox.Show("Please select only one developer.");
            }
        }

        /// <summary>
        /// Creates new item.
        /// </summary>
        private void CreateNewDeveloperExecute()
        {
            DeveloperViewModel viewModel = new DeveloperViewModel(new Developer(), this.repository);
            this.ShowDeveloper(viewModel);
        }
    }
}
