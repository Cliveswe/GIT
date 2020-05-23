using System.ComponentModel;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-25
/// </summary>
namespace ControlTower.Controls.LabelTextBoxIO
{
    /// <summary>
    /// An interface class used in the interaction between the View and ViewModel in the 
    /// design pattern Model View ViewModel. This is the base type and the subclasses that 
    /// implement the base type, implement different strategies on the base type.
    /// </summary>
    interface ILabelTextInputOutput : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        string Label
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a text input output.
        /// </summary>
        /// <value>
        /// The text input output.
        /// </value>
        string TextIO
        {
            get; set;
        }

        void ClearTextIO();
    }
}
