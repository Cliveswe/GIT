using System;
using System.Windows.Input;
///<summary>
/// Created by Created by Clive Leddy
/// clive.leddy@gmail.com
/// Date 2019-02-06
/// </summary>
namespace InvoiceMaker.ButtonControls
{
    class RelayCommands : ICommand
    {

        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        /// <summary>
        /// Class constructor is an implementation of the ICommand.
        /// </summary>
        /// <param name="execute">An Action is the execute method<object></param>
        /// <param name="canExecute">An Predicate that is a delegate for the CanExecute method<object></param>
        public RelayCommands(Action<object> execute, Predicate<object> canExecute)
        {
            //the constructor signature must contain parameter that are not null
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// When an event handler is raised by the Command application when the CanExecute()
        /// needs to be re-evaluated.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add {
                CommandManager.RequerySuggested += value;
            }

            remove {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// This is not used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Boolean, true if </param>
        private void CommandManager_RequerySuggested(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check to see if a command can be executed.
        /// </summary>
        /// <param name="parameter">An object pass information from the caller command.</param>
        /// <returns>Boolean true if a command could be executed otherwise false.</returns>
        bool ICommand.CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute(parameter);
            }

        }

        /// <summary>
        /// This is  called when the command is actuated.
        /// </summary>
        /// <param name="parameter">An object pass information from the caller command.</param>
        void ICommand.Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
