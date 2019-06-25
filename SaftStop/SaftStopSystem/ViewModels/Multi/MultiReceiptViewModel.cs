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
    /// A class to represents a list of the user's receipts.
    /// </summary>
    public class MultiReceiptViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The repository of data.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// The account of each row in the view model.
        /// </summary>
        private Account account;

        /// <summary>
        /// Initializes a new instance of the MultiReceiptViewModel class.
        /// </summary>
        /// <param name="repository">The repository of data.</param>
        /// <param name="account">The account to show the receipts of.</param>
        public MultiReceiptViewModel(Repository repository, Account account) : base("Receipts")
        {
            this.repository = repository;

            this.account = account;

            this.repository.ReceiptAdded += this.OnReceiptAdded;

            List<ReceiptViewModel> receipts =
                (from r in this.repository.GetReceipts()
                 where r.AccountName.Id == account.Id
                 select new ReceiptViewModel(r, this.repository)).ToList();

            this.AllReceipts = new ObservableCollection<ReceiptViewModel>(receipts);

            this.Update();
        }

        /// <summary>
        /// Gets or sets all the receipts.
        /// </summary>
        public ObservableCollection<ReceiptViewModel> AllReceipts { get; set; }

        /// <summary>
        /// Gets the number of items selected.
        /// </summary>
        public int NumberOfItemsSelected
        {
            get
            {
                return this.AllReceipts.Count(vm => vm.IsSelected);
            }
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        protected override void CreateCommands()
        {
                this.Commands.Add(new CommandViewModel("View", new DelegateCommand(p => this.ShowOneReceipt())));         
        }

        /// <summary>
        /// Shows all the receipt within the view model.
        /// </summary>
        private void ShowOneReceipt()
        {
            ReceiptViewModel viewModel = this.AllReceipts.SingleOrDefault(vm => vm.IsSelected);
            if (viewModel != null)
            {
                this.ShowReceipt(viewModel);
                this.repository.SaveToDatabase();
            }
            else
            {
                MessageBox.Show("Please select a receipt.");
            }
        }

        /// <summary>
        /// The account to update within the receipts view.
        /// </summary>
        private void Update()
        {
        }

        /// <summary>
        /// On the receipt view model property changed.
        /// </summary>
        /// <param name="sender">The view that was changed.</param>
        /// <param name="e">The event that .</param>
        private void OnReceiptViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                this.OnPropertyChanged("NumberOfItemsSelected");
            }
        }

        /// <summary>
        /// On receipt added.
        /// </summary>
        /// <param name="sender">A Button click.</param>
        /// <param name="e">The click of the button.</param>
        private void OnReceiptAdded(object sender, ReceiptEventArgs e)
        {
            ReceiptViewModel viewModel = new ReceiptViewModel(e.Receipt, this.repository);
            viewModel.PropertyChanged += this.OnReceiptViewModelPropertyChanged;
            this.AllReceipts.Add(viewModel);
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
    }
}