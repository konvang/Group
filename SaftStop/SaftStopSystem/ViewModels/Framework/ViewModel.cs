using System.ComponentModel;

namespace SaftStopSystem
{
    /// <summary>
    /// The class which is used to represent a view  model.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the ViewModel class.
        /// </summary>
        /// <param name="displayName">The name to set the views display name to.</param>
        public ViewModel(string displayName)
        {
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Event that occurs when property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Method that occurs when a property is changed.
        /// </summary>
        /// <param name="propertyName">The changed property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}