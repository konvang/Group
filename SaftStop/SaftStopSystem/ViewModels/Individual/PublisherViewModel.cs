using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// The publisher view model.
    /// </summary>
    public class PublisherViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The publisher to view.
        /// </summary>
        private Publisher publisher;

        /// <summary>
        /// The repository where information is stored.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// Is this view model selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the PublisherViewModel class.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <param name="repository">The repository where information is stored.</param>
        public PublisherViewModel(Publisher publisher, Repository repository) : base("Publisher")
        {
            this.repository = repository;
            this.publisher = publisher;
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
        /// Gets or sets the Publisher Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.publisher.Name;
            }

            set
            {
                this.publisher.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Saves the publisher.
        /// </summary>
        public void Save()
        {
            this.repository.AddPublisher(this.publisher);
            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        protected override void CreateCommands()
        {
            this.Commands.Add(new CommandViewModel("Ok", new DelegateCommand(p => this.OkExecute())));
            this.Commands.Add(new CommandViewModel("Cancel", new DelegateCommand(p => this.CancelExecute())));
        }

        /// <summary>
        /// Ok to execute save and close.
        /// </summary>
        private void OkExecute()
        {
            this.Save();
            this.CloseAction(true);
        }

        /// <summary>
        /// Cancels the execution.
        /// </summary>
        private void CancelExecute()
        {
            this.CloseAction(false);
        }
    }
}
