using LabBook.Forms.SemiProduct.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabBook.Forms.SemiProduct.Command
{
    public class ClpButton : ICommand
    {
        private readonly SemiProductFormMV _modelView;

        public ClpButton(SemiProductFormMV modelView)
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
            return _modelView.IsDanger;
        }

        public void Execute(object parameter)
        {
            _modelView.OpenClpForm();
        }
    }
}
