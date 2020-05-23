using System.Collections.Generic;
using System.ComponentModel;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-03-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.GroupListBoxIO
{
    /// <summary>
    /// This class contain tree elements. A header to give a groupbox a title. A List
    /// or strings that populate a Listbox and finally a IsSelected property that holds
    /// a int value of a selected item in the listbox.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class GroupListBoxInteractive : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the group ListBox header.
        /// </summary>
        /// <value>
        /// The group ListBox header.
        /// </value>
        public string GroupListBoxHeader
        {
            get {
                return _groupListBoxHeader;
            }
            set {
                _groupListBoxHeader = value;
                OnPropertyChanged("GroupListBoxHeader");
            }
        }
        private string _groupListBoxHeader;

        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        public List<string> Contents
        {
            get {
                return _contents;
            }
            set {
                _contents = value;
                OnPropertyChanged("Contents");
            }
        }
        private List<string> _contents;

        /// <summary>
        /// The no item selected can be used to compare with IsSelected to confirm that
        /// an item has been selected from Contents.
        /// </summary>
        public readonly int noItemSelected = -1;

        /// <summary>
        /// Gets or sets what has been selected from Contents.
        /// </summary>
        /// <value>
        /// The is selected.
        /// </value>
        public abstract int IsSelected
        {
            get;
            set;
        }
       
        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get {
                return _isEnabled;
            }
            set {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        private bool _isEnabled;

        /// <summary>
        /// Clears the contents.
        /// </summary>
        public void ClearContents()
        {
            Contents = new List<string>();
        }

        /// <summary>
        /// Resets the is selected.
        /// </summary>
        public abstract void ResetIsSelected();
       
        /// <summary>
        /// Resets the is enabled.
        /// </summary>
        public void ResetIsEnabled()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Clears the header.
        /// </summary>
        public void ClearHeader()
        {
            GroupListBoxHeader = string.Empty;
        }

        /// <summary>
        /// Resets this instance to its empty state.
        /// </summary>
        public void Reset()
        {
            ClearContents();
            ResetIsSelected();
            ClearHeader();
            ResetIsEnabled();
        }

        /// <summary>
        /// Sets the header.
        /// </summary>
        public abstract void SetHeader();

        /// <summary>
        /// Populates the content.
        /// </summary>
        public abstract void PopulateContent();
       
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupListBoxInteractive"/> class.
        /// </summary>
        public GroupListBoxInteractive()
        {
            Contents = new List<string>();
            Reset();
        }

        #region INotifyPropertyChanged members
        /// <summary>
        /// This is boiler plate code that was added when one want to notify a change on a class
        /// property. This is where the code and the UI communicate through the event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise a notification that a property has been changed.
        /// </summary>
        /// <param name="propertyName">A string of the property name</param>
        internal void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
