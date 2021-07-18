using LabBook.Forms.ClpData.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.ClpData.Command
{
    public class AddButton : ICommand
    {
        private readonly ClpListMV _modelView;

        public AddButton(ClpListMV modelView)
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
            return _modelView.ClpDataSelectedCount > 0;
        }

        public void Execute(object parameter)
        {
            _modelView.AddClpToList();
        }
    }
}
