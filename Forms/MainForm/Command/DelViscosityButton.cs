using LabBook.Forms.MainForm.ModelView;
using System;
using System.Windows;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.Command
{
    public class DelViscosityButton : ICommand
    {
        private readonly ViscosityMV _modelView;

        public DelViscosityButton(ViscosityMV modelView)
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
            return _modelView.DgBrookfieldRowIndex < _modelView.GetBrookView.Count;
        }

        public void Execute(object parameter)
        {
            if (_modelView.ActualBrookfieldRow == null || _modelView.ActualBrookfieldRow.IsNew) return;

            if (MessageBox.Show("Czy usunąć zaznaczony rekord?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = Convert.ToInt64(_modelView.ActualBrookfieldRow.Row["id"]);
                _modelView.GetBrookView.Delete((int)_modelView.DgBrookfieldRowIndex);
                _modelView.Delete(id);
            }
        }
    }
}
