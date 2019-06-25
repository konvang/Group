using System;
using System.Windows.Input;

namespace SaftStopSystem
{
    /// <summary>
    /// A class to represent the Delegate commands.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Command to act upon.
        /// </summary>
        private readonly Action<object> command;

        /// <summary>
        /// The can execute parameter.
        /// </summary>
        private readonly Predicate<object> canExecute;

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="command">The command to attach.</param>
        public DelegateCommand(Action<object> command) : this(command, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="command">The command to attach.</param>
        /// <param name="canExecute">A value indicating whether or not the command can be executed.</param>
        public DelegateCommand(Action<object> command, Predicate<object> canExecute)
        {
            if (command == null)
            {
                throw new Exception("Command was null");
            }

            this.command = command;

            this.canExecute = canExecute;
        }

        /// <summary>
        /// Can execute was changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Determines whether passed in object can be executed.
        /// </summary>
        /// <param name="parameter">The object to check.</param>
        /// <returns>A value indicating whether it is.</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        /// <summary>
        /// Executes the command on the object.
        /// </summary>
        /// <param name="parameter">The object to check.</param>
        public void Execute(object parameter)
        {
            this.command(parameter);
        }
    }
}
