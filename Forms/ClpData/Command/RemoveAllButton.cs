using LabBook.Forms.ClpData.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.ClpData.Command
{
    public class RemoveAllButton : ICommand
    {
        private readonly ClpListMV _modelView;

        public RemoveAllButton(ClpListMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
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
            return _modelView.ClpSelectedCount > 0;
        }

        public void Execute(object parameter)
        {
            _modelView.RemoveAllSelected();
        }
    }
}
