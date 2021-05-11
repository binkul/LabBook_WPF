using LabBook.Forms.MainForm.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.Command
{
    public class SaveButton : ICommand
    {
        private readonly WindowEditMV _modelView;

        public SaveButton(WindowEditMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return _modelView.Modified;
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
            _modelView.SaveAll();
        }
    }
}
