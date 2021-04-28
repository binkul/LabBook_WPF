using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.MainForm.ModelView
{
    using Model;
    using System.ComponentModel;

    public class WindowEdit : INotifyPropertyChanged
    {
        private readonly WindowData windowData = WindowSetting.Read();
        
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

        public double ColId
        {
            get
            {
                return windowData.ColId;
            }
            set
            {
                windowData.ColId = value;
                OnPropertyChanged(nameof(ColId));
            }
        }

        public double ColTitle
        {
            get
            {
                return windowData.ColTitle;
            }
            set
            {
                windowData.ColTitle = value;
                OnPropertyChanged(nameof(ColTitle));
            }
        }

        public double ColUser
        {
            get
            {
                return windowData.ColUser;
            }
            set
            {
                windowData.ColUser = value;
                OnPropertyChanged(nameof(ColUser));
            }
        }

        public double ColCycle
        {
            get
            {
                return windowData.ColCycle;
            }
            set
            {
                windowData.ColCycle = value;
                OnPropertyChanged(nameof(ColCycle));
            }
        }

        public double ColDensity
        {
            get
            {
                return windowData.ColDensity;
            }
            set
            {
                windowData.ColDensity = value;
                OnPropertyChanged(nameof(ColDensity));
            }
        }

        public double ColDate
        {
            get
            {
                return windowData.ColDate;
            }
            set
            {
                windowData.ColDate = value;
                OnPropertyChanged(nameof(ColDate));
            }
        }

        public void Save()
        {
            WindowSetting.Save(windowData);
        }
    }
}
