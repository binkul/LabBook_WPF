using LabBook.Forms.MainForm.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.Command
{
    public class RefreshButton : ICommand
    {
        private readonly WindowEditMV _modelView;
        public event EventHandler CanExecuteChanged;

        public RefreshButton(WindowEditMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _modelView.RefreshAll();
        }
    }
}
