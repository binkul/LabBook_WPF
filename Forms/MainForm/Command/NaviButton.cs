using LabBook.Forms.MainForm.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.Command
{
    public class NaviButtonRight : ICommand
    {
        private readonly WindowEditMV _modelView;

        public NaviButtonRight(WindowEditMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return _modelView.DgRowIndex < _modelView.GetLabBookView.Count - 1;
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
            var index = _modelView.DgRowIndex;
            var count = _modelView.GetLabBookView.Count;

            _ = index < count - 1 ? index++ : index = count - 1;

            _modelView.DgRowIndex = index;
            _modelView.UpdateRowIndex();
        }
    }

    public class NaviButtonLast : ICommand
    {
        private readonly WindowEditMV _modelView;

        public NaviButtonLast(WindowEditMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return _modelView.DgRowIndex < _modelView.GetLabBookView.Count - 1;
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
            long index = _modelView.GetLabBookView.Count - 1;

            _modelView.DgRowIndex = index;
            _modelView.UpdateRowIndex();
        }
    }

    public class NaviButtonFirst : ICommand
    {
        private readonly WindowEditMV _modelView;

        public NaviButtonFirst(WindowEditMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return _modelView.DgRowIndex > 0;
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
            _modelView.DgRowIndex = 0;
            _modelView.UpdateRowIndex();
        }
    }

    public class NaviButtonLeft : ICommand
    {
        private readonly WindowEditMV _modelView;

        public NaviButtonLeft(WindowEditMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return _modelView.DgRowIndex > 0;
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
            var index = _modelView.DgRowIndex;
            _ = index > 0 ? index-- : index = 0;

            _modelView.DgRowIndex = index;
            _modelView.UpdateRowIndex();
        }
    }
}
