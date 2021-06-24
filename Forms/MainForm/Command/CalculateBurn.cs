using LabBook.Forms.MainForm.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.Command
{
    class CalculateBurn : ICommand
    {
        private readonly WindowEditMV _modelView;

        public CalculateBurn(WindowEditMV modelView)
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
            _modelView.CalculateAshAndBurn();
        }
    }
}
