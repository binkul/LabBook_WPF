using LabBook.Forms.Materials.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.Materials.Command
{
    public class SaveButton : ICommand
    {
        private readonly MaterialFormMV _modelView;

        public SaveButton(MaterialFormMV modelView)
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
