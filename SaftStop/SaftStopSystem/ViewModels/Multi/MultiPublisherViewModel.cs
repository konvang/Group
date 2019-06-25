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
    /// A class to represent the multiple publisher view model.
    /// </summary>
    public class MultiPublisherViewModel : WorkspaceViewModel
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
        /// Initializes a new instance of the MultiPublisherViewModel class.
        /// </summary>
        /// <param name="repository">The repository of data.</param>
        /// <param name="account">The account used integer the view model.</param>
        public MultiPublisherViewModel(Repository repository, Account account) : base("Publishers")
        {
            this.account = account;

            this.repository = repository;

            this.CreateCommands();

            this.repository.PublisherAdded += this.OnPublisherAdded;

            this.AllPublishers = new ObservableCollection<PublisherViewModel>();

            List<PublisherViewModel> publishers;

            if (this.repository.GetPublishers() != null)
            {
                publishers =
                (from d in this.repository.GetPublishers()
                 select new PublisherViewModel(d, this.repository)).ToList();

                publishers.ForEach(cvm => cvm.PropertyChanged += this.OnPublisherViewModelPropertyChanged);

                this.AllPublishers = new ObservableCollection<PublisherViewModel>(publishers);
            }
        }

        /// <summary>
        /// Gets all the customers.
        /// </summary>
        public ObservableCollection<PublisherViewModel> AllPublishers { get; private set; }

        /// <summary>
        /// Gets the number of items selected.
        /// </summary>
        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllPublishers.Count(vm => vm.IsSelected);
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
                    this.Commands.Add(new CommandViewModel("New...", new DelegateCommand(p => this.CreateNewCategoryExecute())));
                    this.Commands.Add(new CommandViewModel("Edit...", new DelegateCommand(p => this.EditCategoryExecute())));
                }
            }
        }

        /// <summary>
        /// On publisher added.
        /// </summary>
        /// <param name="sender">A Button click.</param>
        /// <param name="e">The click of the button.</param>
        private void OnPublisherAdded(object sender, PublisherEventArgs e)
        {
            PublisherViewModel viewModel = new PublisherViewModel(e.Publisher, this.repository);
            this.AllPublishers.Add(viewModel);
        }

        /// <summary>
        /// On the category view model property changed.
        /// </summary>
        /// <param name="sender">The view that was changed.</param>
        /// <param name="e">The event that .</param>
        private void OnPublisherViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
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
        private void ShowPublisher(PublisherViewModel viewModel)
        {
            WorkspaceWindow window = new WorkspaceWindow();
            window.Width = 400;
            window.Height = 150;
            window.Title = viewModel.DisplayName;

            viewModel.CloseAction = b => window.DialogResult = b;

            PublisherView view = new PublisherView();
            view.DataContext = viewModel;

            window.Content = view;
            window.ShowDialog();
        }

        /// <summary>
        /// Edit and execute.
        /// </summary>
        private void EditCategoryExecute()
        {
            PublisherViewModel viewModel = this.AllPublishers.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                this.ShowPublisher(viewModel);
                this.repository.SaveToDatabase();
            }
            else
            {
                MessageBox.Show("Please select only one publisher.");
            }
        }

        /// <summary>
        /// Creates new item.
        /// </summary>
        private void CreateNewCategoryExecute()
        {
            PublisherViewModel viewModel = new PublisherViewModel(new Publisher(), this.repository);
            this.ShowPublisher(viewModel);
        }
    }
}
