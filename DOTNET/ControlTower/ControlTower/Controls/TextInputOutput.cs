using ControlTower.Controls.LabelTextBoxIO;
using System.ComponentModel;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-25
/// </summary>
namespace ControlTower.Controls.TextInputOutput
{/// <summary>
/// This class implements the INotifyPropertyChanged members. The class performs a validation
/// check for strings. The validation method can be overridden to customise validation logic.
/// </summary>
    public class TextInputOutput : ILabelTextInputOutput
    {
        #region Delegate
        /// <summary>
        /// Delegate that determines if there is a flight to start.
        /// </summary>
        /// <param name="canStart">True or false.</param>
        /// <param name="msg">String from input.</param>
        public delegate void ToStart(bool canStart, string msg);

        #region Delegate Helper Functions
        /// <summary>
        /// Define member variables of this delegate.
        /// Declaring it private strengthening encapsulation.
        /// </summary>
        private ToStart listOfHandlers;

        /// <summary>
        /// A registration function for the caller.
        /// </summary>
        /// <param name="methodCall">The callers function</param>
        public void RegisterWithToStart(ToStart methodCall)
        {
            listOfHandlers += methodCall;
        }

        /// <summary>
        /// De-registration function for the caller.
        /// </summary>
        /// <param name="methodCall">The callers function</param>
        public void UnRegisterWithToStart(ToStart methodCall)
        {
            listOfHandlers -= methodCall;
        }

        #endregion
        /// <summary>
        /// Inform the delegate that something occurred.
        /// </summary>
        /// <param name="isValid">True if valid input otherwise false.</param>
        private void TriggerToStart(bool isValid)
        {
            if (listOfHandlers != null)
            {
                listOfHandlers(!isValid, TextIO);//inform call back methods
            }
        }
        #endregion

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label
        {
            get {
                return _label;

            }
            set {
                _label = value;
                OnPropertyChanged("Label");
            }
        }
        private string _label;
        /// <summary>
        /// Gets or sets a text input output.
        /// </summary>
        /// <value>
        /// The text input output.
        /// </value>
        public string TextIO
        {
            get {
                return _textIO;
            }
            set {
                _textIO = value;
                OnPropertyChanged("TextIO");
            }
        }
        private string _textIO;

        /// <summary>
        /// Clears the content in the text property.
        /// </summary>
        public void ClearTextIO()
        {
            TextIO = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputOutput"/> class.
        /// </summary>
        public TextInputOutput(string label)
        {
            Label = label;
            IsValid = false;
        }

        #region INotifyDataErrorInfo members
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage
        {
            get {
                return _errorMessage;
            }
            private set {
                _errorMessage = value;
            }
        }
        private string _errorMessage;

        public string Error => throw new System.NotImplementedException();
        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified column name.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get {
                if (columnName == "TextIO")
                {
                    if (ValidateInput(TextIO))
                    {
                        return ErrorMessage;
                    }
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// Sets the error message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void SetErrorMessage(string msg)
        {
            ErrorMessage = msg;
        }
        /// <summary>
        /// Returns true if the input is free from errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get {
                return !_isValid;
            }
            protected set {
                TriggerToStart(value);//trigger delegate
                _isValid = value;
            }
        }
        private bool _isValid;
        /// <summary>
        /// Validates the input. Can be overridden.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns></returns>
        public virtual bool ValidateInput(string msg)
        {
            bool checkInput = true;
            if (!string.IsNullOrWhiteSpace(msg))
            {
                checkInput = false;
            }
            IsValid = checkInput;
            return checkInput;
        }
        #endregion

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
