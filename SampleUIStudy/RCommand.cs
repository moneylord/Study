using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SampleUIStudy
{
    public class RelayCommand<T> : ICommand
    {
        #region Declarations
        readonly Predicate<T> canExecute;
        readonly Action<T> execute;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/>
        /// class and the command can always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion
        #region ICommand 멤버
        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        void ICommand.Execute(object parameter)
        {
            execute((T)parameter);
        }

        #endregion
    }

    /// <summary>
    /// A command whose sole purpos is to relay its functionality to other object by
    /// invoking delegates. The default return valiue for the CanExecute emethod is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region  Declarations
        readonly Func<bool> canExecute;
        readonly Action execute;
        private Action<object> showMessage;
        private Func<object, bool> p;
        private Action<object> changeCanExecute;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> 
        /// class and the command can always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> p)
        {
            this.showMessage = execute;
            this.p = p;
        }

        public RelayCommand(Action<object> execute)
        {
            this.showMessage = execute;
        }
        #endregion

        #region ICommand 멤버
        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        void ICommand.Execute(object parameter)
        {
            showMessage(parameter);
        }
        #endregion
    }
}
