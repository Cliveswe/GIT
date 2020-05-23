using ApusAnimalHotel.ViewModel.Commands;
using ApusAnimalHotel.ViewModel.LabelTextBoxIO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace ApusAnimalHotel.ViewModel.FoodDetails.RegisterDetailWindow
{
    class RegisterHeader : INotifyPropertyChanged
    {
        /// <summary>
        /// Properties used in the UI
        /// </summary>
        /// <value>
        /// The no item selected is a value that indicates that there was 
        /// noting selected in the UI.
        /// </value>
        #region Headers               
        public int NoItemSelected
        {
            get {
                return _noItemSelected;
            }
            private set {
                _noItemSelected = value;
            }
        }
        int _noItemSelected;
        /// <summary>
        /// Sets the no item selected property.
        /// </summary>
        private void SetNoItemSelected()
        {
            NoItemSelected = -1;
        }
        #region Static Header Names
        public string Add
        {
            get {
                return "Add";
            }
        }

        public string Change
        {
            get {
                return "Change";
            }
        }
        public string Delete
        {
            get {
                return "Delete";
            }
        }

        public string Ok
        {
            get {
                return "OK";
            }
        }
        public string Cancel
        {
            get {
                return "Cancel";
            }
        }
        #endregion        
      
        /// <summary>
        /// Gets or sets a value indicating whether [title is active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [title is active]; otherwise, <c>false</c>.
        /// </value>
        public bool TitleIsActive
        {
            get {
                return _titleIsActive;
            }
            set {
                _titleIsActive = value;
                OnPropertyChanged("TitleIsActive");
            }
        }
        private bool _titleIsActive;
        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get {
                return _title;
            }
            set {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        private string _title;
        /// <summary>
        /// Gets or sets the group title of the groupbox.
        /// </summary>
        /// <value>
        /// The group title.
        /// </value>
        public string GroupTitle
        {
            get {
                return _groupTitle;
            }
            set {
                _groupTitle = value;
                OnPropertyChanged("GroupTitle");
            }
        }
        private string _groupTitle;

        /// <summary>
        /// Gets or sets the name of the group title input.
        /// Hold an instance of a object that manipulates the name of an 
        /// group title input name, used in the UI.
        /// </summary>
        /// <value>
        /// The name of the group title input.
        /// </value>
        public TextInputOutput GroupTitleInputName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the title.
        /// Hold an instance of a object that manipulates the name of an 
        /// group title name, used in the UI.
        /// </summary>
        /// <value>
        /// The name of the title.
        /// </value>
        public TextInputOutput TitleName
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Gets or sets the food details as list.
        /// </summary>
        /// <value>
        /// The food details as list.
        /// </value>
        public ObservableCollection<string> RegisterDetailsAsList
        {
            get {
                return _registerDetailsAsList;
            }
            set {
                _registerDetailsAsList = value;
                OnPropertyChanged("RegisterDetailsAsList");
            }
        }
        private ObservableCollection<string> _registerDetailsAsList;


        /// <summary>
        /// Gets or sets the content to be edited.
        /// </summary>
        /// <value>
        /// The content of the edit.
        /// </value>
        public string EditContent
        {
            get {
                return _editContent;
            }
            set {
                _editContent = value;
                OnPropertyChanged("EditContent;");
            }
        }
        private string _editContent;
        /// <summary>
        /// Gets or sets the selected item as int.
        /// </summary>
        /// <value>
        /// The item selected.
        /// </value>
        public int ItemSelected
        {
            get {
                return _itemSelected;
            }
            set {
                _itemSelected = value;
                UpdateGroupTitleInputName();
                OnPropertyChanged("ItemSelected");
            }
        }
        private int _itemSelected;

        /// <summary>
        /// Updates the name of the group title input from the observable 
        /// collection RegisterDetatlsAsList.
        /// </summary>
        private void UpdateGroupTitleInputName()
        {
            int index = 0;
            if (ItemSelected > NoItemSelected)
            {
                foreach (string item in RegisterDetailsAsList)
                {
                    if (index == ItemSelected)
                    {
                        GroupTitleInputName.TextIO = item;
                        break;
                    }
                    index++;
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the register has been modified.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is modified; otherwise, <c>false</c>.
        /// </value>
        public bool IsModified
        {
            get 
                {
                return _isModifed;
            }
            protected set {
                _isModifed = value;
                OnPropertyChanged("IsModified");
            }
        }
        private bool _isModifed;
        /// <summary>
        /// Sets the is modified property.
        /// </summary>
        protected void SetIsModified()
        {
            IsModified = true;
        }
        /// <summary>
        /// Reset the is modified property.
        /// </summary>
        protected void ResetIsModified()
        {
            IsModified = false;
        }
        #region Button Logic

        #region Editing
        /// <summary>
        /// Compares the item selected to length of the register.
        /// </summary>
        /// <returns>True if the selected item is grater than -1 and less than the register length, otherwise false</returns>
        private bool ValidSelection()
        {
            if ((ItemSelected > NoItemSelected) && (ItemSelected < RegisterDetailsAsList.Count))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if an item has been chosen.
        /// </summary>
        /// <returns>True if an item has been selected, otherwise false.</returns>
        private bool HasItemBeenChosen()
        {
            if (ItemSelected > NoItemSelected)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// If an item from the observable collection has been selected.
        /// </summary>
        /// <param name="parameter">Object parameter from UI</param>
        /// <returns>True if an item has been selected otherwise false.</returns>
        private bool ItemSelectedCanExecute(object parameter)
        {
            return HasItemBeenChosen();
        }

        #region Add Button        
        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        /// <value>
        /// The add command.
        /// </value>
        public RelayCommands AddCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Validate if the add button can be activated.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Button activated if true otherwise deactivated if false</returns>
        private bool AddItemCanExecute(object parameter)
        {
            if (GroupTitleInputName != null)
            {
                if ((GroupTitleInputName.IsValid) && !HasItemBeenChosen())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update the register with new data.
        /// </summary>
        /// <param name="parameter"></param>
        private void AddItem(object parameter)
        {
            RegisterDetailsAsList.Add(GroupTitleInputName.TextIO);
            Reset();
            SetIsModified();
        }
        #endregion

        #region Delete Button        
        /// <summary>
        /// Gets or sets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        public RelayCommands DeleteCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Delete a selected from the register.
        /// </summary>
        /// <param name="parameter"></param>
        private void DeleteItem(object parameter)
        {
            if (ValidSelection())
            {
                RegisterDetailsAsList.RemoveAt(ItemSelected);
                SetIsModified();
            }

            Reset();

        }
        #endregion

        #region Change Button        
        /// <summary>
        /// Gets or sets the change command.
        /// </summary>
        /// <value>
        /// The change command.
        /// </value>
        public RelayCommands ChangeCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Add the changed ingredient to the register.
        /// </summary>
        /// <param name="parameter"></param>
        private void ChangeItem(object parameter)
        {
            if (ValidSelection())
            {
                RegisterDetailsAsList.Insert(ItemSelected + 1, GroupTitleInputName.TextIO);
                RegisterDetailsAsList.RemoveAt(ItemSelected);
                SetIsModified();
            }
            Reset();
        }
        #endregion

        #region Cancel Button
        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>
        /// The change command.
        /// </value>
        public RelayCommands CancelCommand
        {
            get;
            set;
        }
        /// <summary>
        /// Close the ingredients window.
        /// </summary>
        /// <param name="parameter">An object as a generic parameter</param>
        protected void CloseWindow(object parameter)
        {
            Window win = parameter as Window;
            win.Close();
        }
        /// <summary>
        /// Can the cancel button be executed.
        /// </summary>
        /// <param name="parameter">The parameter. Not used in this case.</param>
        /// <returns>True</returns>
        public virtual bool CancelCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Logic behind the cancel button.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public virtual void CancelExecute(object parameter)
        {
            ResetIsModified();
            CloseWindow(parameter);

        }
        #endregion

        #region OK button        
        /// <summary>
        /// Gets or sets the ok command.
        /// </summary>
        /// <value>
        /// The ok command.
        /// </value>
        public RelayCommands OKCommand
        {
            get;
            set;
        }
        /// <summary>
        /// Ok button can execute.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public virtual bool OkCanExecute(object parameter)
        {
            if (IsModified)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Ok button perform task.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public virtual void OkExecute(object parameter)
        {
            CloseWindow(parameter);
        }
        #endregion
        #endregion

        #region Controller
        /// <summary>
        /// Reset the UI
        /// </summary>
        private void Reset()
        {
            ClearGroupTitleInputName();
            ResetItemSelected();
        }

        /// <summary>
        /// Clear the Group Title input from content.
        /// </summary>
        private void ClearGroupTitleInputName()
        {
            GroupTitleInputName.ClearTextIO();
        }

        /// <summary>
        /// Reset the property ItemSelected to -1.
        /// </summary>
        private void ResetItemSelected()
        {
            ItemSelected = NoItemSelected;
        }

        /// <summary>
        /// Add all the commands used by this application.
        /// </summary>
        private void AddRelayCommands()
        {
            AddCommand = new RelayCommands(AddItem, AddItemCanExecute);
            DeleteCommand = new RelayCommands(DeleteItem, ItemSelectedCanExecute);
            ChangeCommand = new RelayCommands(ChangeItem, ItemSelectedCanExecute);
            CancelCommand = new RelayCommands(CancelExecute, CancelCanExecute);
            OKCommand = new RelayCommands(OkExecute, OkCanExecute);
        }

        #endregion
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterHeader"/> class.
        /// </summary>
        public RegisterHeader()
        {
            SetNoItemSelected();
            ResetItemSelected();
            RegisterDetailsAsList = new ObservableCollection<string>();
            AddRelayCommands();
            ResetIsModified();
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
        private void OnPropertyChanged(string propertyName)
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
