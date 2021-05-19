using LabBook.Forms.MainForm.ModelView;
using System;
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

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
