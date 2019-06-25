using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaftStopEngine
{
    /// <summary>
    /// A class used to represent the Current page change event arguments.
    /// </summary>
    public class CurrentPageChangeEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CurrentPageChangeEventArgs class.
        /// </summary>
        /// <param name="startIndex">The start of the page.</param>
        /// <param name="itemCount">The count of the page.</param>
        public CurrentPageChangeEventArgs(int startIndex, int itemCount)
        {
            this.StartIndex = startIndex;
            this.ItemCount = itemCount;
        }

        /// <summary>
        /// Gets the start index of the view model.
        /// </summary>
        public int StartIndex { get; private set; }

        /// <summary>
        /// Gets the item count of the view model.
        /// </summary>
        public int ItemCount { get; private set; }
    }
}