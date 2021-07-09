using LabBook.Forms.Materials.ModelView;
using System;
using System.Windows.Input;

namespace LabBook.Forms.Materials.Command
{
    public class NaviButtonRight : ICommand
    {
        private readonly MaterialFormMV _modelView;

        public NaviButtonRight(MaterialFormMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return _modelView.DgRowIndex < _modelView.GetMaterialView.Count - 1;
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
            var count = _modelView.GetMaterialView.Count;

            _ = index < count - 1 ? index++ : index = count - 1;

            _modelView.DgRowIndex = index;
        }
    }

    public class NaviButtonLast : ICommand
    {
        private readonly MaterialFormMV _modelView;

        public NaviButtonLast(MaterialFormMV modelView)
        {
            if (modelView == null) throw new ArgumentNullException("Model widoku jest null");
            _modelView = modelView;
        }

        public bool CanExecute(object parameter)
        {
            return _modelView.DgRowIndex < _modelView.GetMaterialView.Count - 1;
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
            long index = _modelView.GetMaterialView.Count - 1;
            _modelView.DgRowIndex = index;
        }
    }

    public class NaviButtonFirst : ICommand
    {
        private readonly MaterialFormMV _modelView;

        public NaviButtonFirst(MaterialFormMV modelView)
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
        }
    }

    public class NaviButtonLeft : ICommand
    {
        private readonly MaterialFormMV _modelView;

        public NaviButtonLeft(MaterialFormMV modelView)
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
        }
    }

}
