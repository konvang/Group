using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SaftStopSystem
{
    /// <summary>
    /// A class used to represent a workspace view model.
    /// </summary>
    public abstract class WorkspaceViewModel : ViewModel
    {
        /// <summary>
        /// A command to be closed within the workspace view model.
        /// </summary>
        private DelegateCommand closeCommand;

        /// <summary>
        /// A collection of commands of the command view model.
        /// </summary>
        private ObservableCollection<CommandViewModel> commands = new ObservableCollection<CommandViewModel>();

        /// <summary>
        /// Initializes a new instance of the WorkspaceViewModel class.
        /// </summary>
        /// <param name="displayName">A display name.</param>
        public WorkspaceViewModel(string displayName)
            : base(displayName)
        {
            this.CreateCommands();
        }

        /// <summary>
        /// A request to close within the view model.
        /// </summary>
        public event EventHandler RequestClose;

        /// <summary>
        /// Gets or sets action to close the model.
        /// </summary>
        public Action<bool> CloseAction { get; set; }

        /// <summary>
        /// Gets a command from the command view model.
        /// </summary>
        public ObservableCollection<CommandViewModel> Commands
        {
            get
            {
                return this.commands;
            }
        }

        /// <summary>
        /// Gets a closed command from the view model.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (this.closeCommand == null)
                {
                    this.closeCommand = new DelegateCommand(p => this.OnRequestClose());
                }

                return this.closeCommand;
            }
        }

        /// <summary>
        /// A command created. 
        /// </summary>
        protected abstract void CreateCommands();

        /// <summary>
        /// A request to be closed on a view model.
        /// </summary>
        private void OnRequestClose()
        {
            if (this.RequestClose != null)
            {
                this.RequestClose(this, EventArgs.Empty);
            }
        }
    }
}
