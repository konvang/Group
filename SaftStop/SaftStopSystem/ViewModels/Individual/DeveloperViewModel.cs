using SaftStopDataAccess;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// The developer view model.
    /// </summary>
    public class DeveloperViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The developer to view.
        /// </summary>
        private Developer developer;

        /// <summary>
        /// The repository where information is stored.
        /// </summary>
        private Repository repository;

        /// <summary>
        /// Is this view model selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the DeveloperViewModel class.
        /// </summary>
        /// <param name="developer">The developer.</param>
        /// <param name="repository">The repository where information is stored.</param>
        public DeveloperViewModel(Developer developer, Repository repository) : base("Developer")
        {
            this.repository = repository;
            this.developer = developer;
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
        /// Gets or sets the Developer Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.developer.Name;
            }

            set
            {
                this.developer.Name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Saves the developer.
        /// </summary>
        public void Save()
        {
            this.repository.AddDeveloper(this.developer);
            this.repository.SaveToDatabase();
        }

        /// <summary>
        /// Creates the commands for this view model.
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
