using System;
using System.Windows.Input;

namespace SaftStopSystem
{
    /// <summary>
    /// A class to represent the command view model.
    /// </summary>
    public class CommandViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the CommandViewModel class.
        /// </summary>
        /// <param name="displayName">The display name to call the view.</param>
        /// <param name="command">The command to set.</param>
        public CommandViewModel(string displayName, ICommand command) : base(displayName)
        {
            if (command == null)
            {
                throw new Exception("Command was null.");
            }

            this.Command = command;
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command { get; set; }
    }
}
