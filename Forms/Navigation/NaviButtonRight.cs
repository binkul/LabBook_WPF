using System;
using System.Windows.Input;

namespace LabBook.Forms.Navigation
{
    public class NaviButtonRight : ICommand
    {
        private readonly NavigationMV _navigation;

        public NaviButtonRight(NavigationMV navigation)
        {
            _navigation = navigation ?? throw new ArgumentNullException("Model widoku jest null");
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _navigation.DgRowIndex < _navigation.GetRowCount - 1;
        }

        public void Execute(object parameter)
        {
            long index = _navigation.DgRowIndex;
            long count = _navigation.GetRowCount;

            _ = index < count - 1 ? index++ : index = count - 1;

            _navigation.DgRowIndex = index;
        }
    }
}
