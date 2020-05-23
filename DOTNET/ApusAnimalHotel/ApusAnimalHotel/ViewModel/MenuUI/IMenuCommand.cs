using ApusAnimalHotel.ViewModel.Commands;
using System;
using System.ComponentModel;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-05-05
/// </summary>
namespace ApusAnimalHotel.ViewModel.MenuUI
{
    interface MenuCommand : INotifyPropertyChanged, IDataErrorInfo
    {

        /// <summary>
        /// Gets or sets the content of the Menu item.
        /// </summary>
        /// <value>
        /// The content of the button.
        /// </value>
        string MenuItemContent
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
        bool IsEnabled
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the Menu item command.
        /// </summary>
        /// <value>
        /// The button command.
        /// </value>
        RelayCommands MenuItemCommand
        {
            get;
            set;
        }
        /// <summary>
        /// Menu item selected, execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void MenuItemExecute(Object parameter);
        /// <summary>
        /// Can the Menu item be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        bool MenuItemCanExecute(Object parameter);
    }
}
