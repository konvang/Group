using System;
using System.Diagnostics.Contracts;
using SaftStopEngine;

namespace SaftStopSystem
{
    /// <summary>
    /// A class used to represent the page view model.
    /// </summary>
    public class PagingViewModel : ViewModel
    {
        /// <summary>
        /// Counts for the pages.
        /// </summary>
        private int itemCount;

        /// <summary>
        /// The current page within the view model.
        /// </summary>
        private int currentPage;

        /// <summary>
        /// The page size of the view model.
        /// </summary>
        private int pageSize;

        /// <summary>
        /// An event handler for the current page changing.
        /// </summary>
        private EventHandler<CurrentPageChangeEventArgs> currentPageChanged;

        /// <summary>
        /// Initializes a new instance of the PagingViewModel class.
        /// </summary>
        /// <param name="itemCount">The page to count.</param>
        public PagingViewModel(int itemCount)
           : base(string.Empty)
        {
            Contract.Requires(itemCount >= 0);
            Contract.Requires(this.pageSize > 0);
            this.itemCount = itemCount;
            this.pageSize = 5;
            if (this.itemCount == 0)
            {
                this.currentPage = 0;
            }
            else
            {
                this.currentPage = 1;
            }

            if (this.ItemCount > 0 && this.CurrentPage >= 1)
            {
                this.GoToFirstPageCommand = new DelegateCommand(p => this.CurrentPage = 1, p => this.CurrentPage != 1);
                this.GoToPreviousPageCommand = new DelegateCommand(p => this.CurrentPage -= 1, p => this.CurrentPage != 1);
            }

            if (this.ItemCount > 0 && this.CurrentPage < this.PageCount)
            {
                this.GoToNextPageCommand = new DelegateCommand(p => this.CurrentPage += 1, p => this.CurrentPage != this.PageCount);
                this.GoToLastPageCommand = new DelegateCommand(p => this.CurrentPage = this.PageCount, p => this.CurrentPage != this.PageCount);
            }
        }

        /// <summary>
        /// Gets or sets the current page changed event handler.
        /// </summary>
        public EventHandler<CurrentPageChangeEventArgs> CurrentPageChanged { get; set; }

        /// <summary>
        /// Gets or sets the go to first page command.
        /// </summary>
        public DelegateCommand GoToFirstPageCommand { get; set; }

        /// <summary>
        /// Gets or sets the go to previous page command.
        /// </summary>
        public DelegateCommand GoToPreviousPageCommand { get; set; }

        /// <summary>
        /// Gets or sets the go to next page command.
        /// </summary>
        public DelegateCommand GoToNextPageCommand { get; set; }

        /// <summary>
        /// Gets or sets the go to last page command.
        /// </summary>
        public DelegateCommand GoToLastPageCommand { get; set; }

        /// <summary>
        /// Gets or sets the item count of the view model.
        /// </summary>
        public int ItemCount
        {
            get
            {
                return this.itemCount;
            }

            set
            {
                this.OnPropertyChanged("PageSize");
                this.OnPropertyChanged("ItemCount");
                this.OnPropertyChanged("PageCount");
            }
        }

        /// <summary>
        /// Gets or sets the page size of the view model.
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }

            set
            {
                this.pageSize = value;
                this.OnPropertyChanged("PageSize");
            }
        }

        /// <summary>
        /// Gets the page count of the view model.
        /// </summary>
        public int PageCount
        {
            get
            {
                int count = (int)Math.Ceiling((double)this.itemCount / this.pageSize);
                return count;
            }
        }

        /// <summary>
        /// Gets or sets the current page of the view model.
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return this.currentPage;
            }

            set
            {
                this.currentPage = value;
                this.OnPropertyChanged("CurrentPage");
                var x = this.CurrentPageChanged;
                if (x != null)
                {
                    this.CurrentPageChanged(this, new CurrentPageChangeEventArgs(this.CurrentPageStartIndex, this.PageSize));
                }
            }
        }

        /// <summary>
        /// Gets the page start index.
        /// </summary>
        public int CurrentPageStartIndex
        {
            get
            {
                return this.PageCount == 0 ? -1 : (this.CurrentPage - 1) * this.PageSize;
            }
        }
    }
}