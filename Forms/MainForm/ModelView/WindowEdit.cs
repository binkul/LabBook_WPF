using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.MainForm.ModelView
{
    using LabBook.ADO.Service;
    using LabBook.Security;
    using Model;
    using System.ComponentModel;
    using System.Data;
    using System.Windows.Controls;

    public class WindowEdit : INotifyPropertyChanged
    {
        private readonly WindowData windowData = WindowSetting.Read();
        //private readonly User _user;
        //private readonly LabBookService _labBookService;
        //private DataView _labBookView;

        //public WindowEdit(User user)
        //{
        //    _user = user;
        //    _labBookService = new LabBookService(_user);
        //    _labBookView = _labBookService.GetAll();
        //}

        public WindowEdit()
        { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public double FormXpos
        {
            get
            {
                return windowData.FormXpos;
            }
            set
            {
                windowData.FormXpos = value;
                OnPropertyChanged(nameof(FormXpos));
            }
        }

        public double FormYpos
        {
            get
            {
                return windowData.FormYpos;
            }
            set
            {
                windowData.FormYpos = value;
                OnPropertyChanged(nameof(FormYpos));
            }
        }

        public double FormWidth
        {
            get
            {
                return windowData.FormWidth;
            }
            set
            {
                windowData.FormWidth = value;
                OnPropertyChanged(nameof(FormWidth));
            }
        }

        public double FormHeight
        {
            get
            {
                return windowData.FormHeight;
            }
            set
            {
                windowData.FormHeight = value;
                OnPropertyChanged(nameof(FormHeight));
            }
        }

        public double ColumnId
        {
            get
            {
                return windowData.ColId;
            }
            set
            {
                windowData.ColId = value;
                OnPropertyChanged(nameof(ColumnId));
            }
        }

        public double ColumnTitle
        {
            get
            {
                return windowData.ColTitle;
            }
            set
            {
                windowData.ColTitle = value;
                OnPropertyChanged(nameof(ColumnTitle));
            }
        }

        public double ColumnUser
        {
            get
            {
                return windowData.ColUser;
            }
            set
            {
                windowData.ColUser = value;
                OnPropertyChanged(nameof(ColumnUser));
            }
        }

        public double ColumnCycle
        {
            get
            {
                return windowData.ColCycle;
            }
            set
            {
                windowData.ColCycle = value;
                OnPropertyChanged(nameof(ColumnCycle));
            }
        }

        public double ColumnDensity
        {
            get
            {
                return windowData.ColDensity;
            }
            set
            {
                windowData.ColDensity = value;
                OnPropertyChanged(nameof(ColumnDensity));
            }
        }

        public double ColumnDate
        {
            get
            {
                return windowData.ColDate;
            }
            set
            {
                windowData.ColDate = value;
                OnPropertyChanged(nameof(ColumnDate));
            }
        }

        //public DataView GetLabBookView
        //{
        //    get
        //    {
        //        return _labBookView;
        //    }
        //}


        public void Save()
        {
            WindowSetting.Save(windowData);
        }
    }
}
