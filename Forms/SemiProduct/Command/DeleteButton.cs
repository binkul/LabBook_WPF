using LabBook.Forms.SemiProduct.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.SemiProduct.Command
{
    public class DeleteButton : ICommand
    {
        private readonly SemiProductFormMV _modelView;

        public DeleteButton(SemiProductFormMV modelView)
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
            return _modelView.CanDelete;
        }

        public void Execute(object parameter)
        {
            _modelView.DeleteSemiProduct();
        }
    }
}
