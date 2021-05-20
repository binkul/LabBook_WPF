using LabBook.Forms.MainForm.ModelView;
using System;
using System.Windows;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.Command
{
    public class DelGlossButton : ICommand
    {
        private readonly GlossMV _modelView;

        public DelGlossButton(GlossMV modelView)
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
            return _modelView.DataGriddRowIndex < _modelView.GetGlossView.Count;
        }

        public void Execute(object parameter)
        {
            _modelView.Delete();
        }
    }
}
