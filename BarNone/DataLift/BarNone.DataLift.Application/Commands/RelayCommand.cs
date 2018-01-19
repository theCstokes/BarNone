using System;
using System.Diagnostics;
using System.Windows.Input;

namespace BarNone.DataLift.UI.Commands
{
    /// <summary> 
    /// Command which executes a given action based on a specified predicate
    /// http://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields
        /// <summary>
        /// Private field which references the action to run when <see cref="_canExecute"/> is true
        /// </summary>
        private readonly Action<object> _execute;
        /// <summary>
        /// Private field which references the predicate to test if the command can execute
        /// </summary>
        private readonly Predicate<object> _canExecute;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor which always alows the action to be invoked
        /// </summary>
        /// <param name="execute">Action which the command will be able to invoke <see cref="ICommand.Execute(object)"/></param>
        public RelayCommand(Action<object> execute)
        : this(execute, null)
        {
        }

        /// <summary>
        /// Constructor which always alows the action to be invoked iff canExecute evaluates to true
        /// </summary>
        /// <param name="execute">Action which the command will be able to invoke <see cref="ICommand.Execute(object)"/></param>
        /// <param name="canExecute">Predicate which evaluates to determine if execute can be invoked</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Members
        /// <summary>
        /// Determines if the command can execute
        /// </summary>
        /// <param name="parameter">Optional parameter to send to test if the command can execute</param>
        /// <returns>If the command is executable</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Control all events to check can execute with
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">Optional parameter to send to the command</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
        
    }
}
