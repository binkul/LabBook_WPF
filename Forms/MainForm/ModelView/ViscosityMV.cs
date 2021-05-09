﻿using LabBook.ADO.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LabBook.Forms.MainForm.ModelView
{
    public class ViscosityMV : INotifyPropertyChanged
    {
        private ExperimentalVisService _service;
        private WindowEditMV _windowEditMV;
        private long _labBookId;
        private bool _profilStd = true;
        private bool _profilExt = false;
        private bool _profilFull = false;
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewItemCommand { get; set; }

        public ViscosityMV()
        {
            OnInitializingNewItemCommand = new RelayCommand<InitializingNewItemEventArgs>(this.OnInitializingNewItemCommandExecuted);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ExperimentalVisService ExpService
        {
            get
            {
                return _service;
            }
            set
            {
                _service = value;
                OnPropertyChanged(nameof(GetBrookView));
            }
        }

        public WindowEditMV SetWindowEditMV
        {
            set
            {
                _windowEditMV = value;
            }
        }

        public long LabBookId
        {
            get
            {
                return _labBookId;
            }
            set
            {
                _labBookId = value;
                if (_service != null)
                    _service.RefreshMainTable(_labBookId);
            }
        }

        public DataView GetBrookView
        {
            get
            {
                if (_service != null)
                    return _service.GetBrookfield;
                else
                    return null;
            }
        }

        public bool ProfilStandard
        {
            get
            {
                return _profilStd;
            }
            set
            {
                _profilStd = value;
                OnPropertyChanged(nameof(ProfilStandard), nameof(ProfilExtOrFull));
            }
        }

        public bool ProfilExtend
        {
            get
            {
                return _profilExt;
            }
            set
            {
                _profilExt = value;
                OnPropertyChanged(nameof(ProfilExtend), nameof(ProfilExtOrFull));
            }
        }

        public bool ProfilFull
        {
            get
            {
                return _profilFull;
            }
            set
            {
                _profilFull = value;
                OnPropertyChanged(nameof(ProfilFull), nameof(ProfilExtOrFull));
            }
        }

        public bool ProfilExtOrFull
        {
            get
            {
                return ProfilFull || ProfilExtend;
            }
        }

        public void OnInitializingNewItemCommandExecuted(InitializingNewItemEventArgs e)
        {
            var row = _windowEditMV.ActualRow;
            var id = Convert.ToInt64(row["id"].ToString());

            var view = e.NewItem as DataRowView;
            view.Row["id"] = -1;
            view.Row["labbook_id"] = id;
            view.Row["vis_type"] = "brookfield";
            view.Row["date_created"] = DateTime.Now;
            view.Row["date_update"] = DateTime.Now;
        }
    }
}