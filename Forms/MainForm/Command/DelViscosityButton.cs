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
            return _modelView.DataGriddRowIndex < _modelView.GetViscosityView.Count;
        }

        public void Execute(object parameter)
        {
            if (_modelView.ActualDataGridRow == null || _modelView.ActualDataGridRow.IsNew) return;

            if (MessageBox.Show("Czy usunąć zaznaczony rekord?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = Convert.ToInt64(_modelView.ActualDataGridRow.Row["id"]);
                _modelView.GetViscosityView.Delete((int)_modelView.DataGriddRowIndex);
                _modelView.Delete(id);
            }
        }
    }
}
