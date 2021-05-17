using LabBook.Forms.MainForm.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.Command
{
    public class NewSerieButton : ICommand
    {
        private readonly WindowEditMV _modelView;

        public NewSerieButton(WindowEditMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return true;
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

        public void Execute(object parameter)
        {
            _modelView.AddNewSeriesRecords();
        }
    }
}
