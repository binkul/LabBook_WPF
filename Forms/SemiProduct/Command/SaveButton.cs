using LabBook.Forms.SemiProduct.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.SemiProduct.Command
{
    public class SaveButton : ICommand
    {
        private readonly SemiProductFormMV _modelView;

        public SaveButton(SemiProductFormMV modelView)
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
            return _modelView.Modified;
        }

        public void Execute(object parameter)
        {
            _modelView.SaveAll();
        }
    }
}
