using LabBook.Forms.Composition.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.Composition.Command
{
    public class AddFirstButton : ICommand
    {
        private readonly CompositionFormMV _modelView;

        public AddFirstButton(CompositionFormMV modelView)
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
            return true;
        }

        public void Execute(object parameter)
        {
            _modelView.AddFirst();
        }
    }
}
