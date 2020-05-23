using ApusAnimalHotel.ViewModel.Commands;
using System;
using System.ComponentModel;
/// <summary>
/// Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-25
/// </summary>
namespace ApusAnimalHotel.ViewModel.ListRegisterAnimalsCheckBox
{
    interface ICheckBoxAnimal : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the content of the checkbox.
        /// </summary>
        /// <value>
        /// The content of the button.
        /// </value>
        string CheckBoxContent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the checkbox command.
        /// </summary>
        /// <value>
        /// The button command.
        /// </value>
        RelayCommands CheckBoxCommand
            {
                get;
                set;
            }
        /// <summary>
        /// Checkbox is selected execute method.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void CheckBoxExecute(Object parameter);
        /// <summary>
        /// Can the button be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        bool CheckBoxCanExecute(Object parameter);
        }
    }
