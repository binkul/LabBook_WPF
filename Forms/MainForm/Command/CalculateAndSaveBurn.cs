using LabBook.Forms.MainForm.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.Command
{
    public class CalculateAndSaveBurn : ICommand
    {
        private readonly WindowEditMV _modelView;

        public CalculateAndSaveBurn(WindowEditMV modelView)
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
            return _modelView.IsPermited;
        }

        public void Execute(object parameter)
        {
            _modelView.CalculatAshAndBurnAndSave();
        }
    }
}
