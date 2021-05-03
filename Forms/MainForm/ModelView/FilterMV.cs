using System;
using System.Text;

namespace LabBook.Forms.MainForm.ModelView
{
    public class FilterMV
    {
        private WindowEditMV _windowEdit;
        private bool _on = false;
        private string _id;
        private string _title;
        private string _user;
        private string _cycle;
        private string _density;
        private string _date;

        public void SetWindowEdit(WindowEditMV mv)
        {
            _windowEdit = mv;
        }

        public bool FilterOn
        {
            private get
            {
                return _on;
            }
            set
            {
                _on = value;
                if (_windowEdit != null)
                    _windowEdit.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string Id
        {
            private get
            {
                return _id;
            }
            set
            {
                _id = value;
                if (_windowEdit != null)
                    _windowEdit.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string Title
        {
            private get
            {
                return _title;
            }
            set
            {
                _title = value;
                if (_windowEdit != null)
                    _windowEdit.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string User
        {
            private get
            {
                return _user;
            }
            set
            {
                _user = value;
                if (_windowEdit != null)
                    _windowEdit.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string ExpCycle
        {
            private get
            {
                return _cycle;
            }
            set
            {
                _cycle = value;
                if (_windowEdit != null)
                    _windowEdit.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string Density
        {
            private get
            {
                return _density;
            }
            set
            {
                _density = value;
                if (_windowEdit != null)
                    _windowEdit.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string Date
        {
            private get
            {
                return _date;
            }
            set
            {
                _date = value;
                if (_windowEdit != null)
                    _windowEdit.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string GetFilterString()
        {
            var filter = new StringBuilder("");

            if (FilterOn)
            {

                #region id filer
                if (!string.IsNullOrEmpty(Id))
                {
                    filter.Clear();
                    filter.Append("id >= ");
                    filter.Append(Id);
                }
                #endregion

                #region title filter
                if (!string.IsNullOrEmpty(Title))
                {
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("title LIKE '");
                        filter.Append(Title);
                        filter.Append("*'");
                    }
                    else
                    {
                        filter.Append(" AND title LIKE '");
                        filter.Append(Title);
                        filter.Append("*'");
                    }
                }
                #endregion

                #region user filter
                if (!string.IsNullOrEmpty(User))
                {
                    long id = Convert.ToInt64(User);
                    if (id > 0)
                    {
                        if (filter.ToString().Length == 0)
                        {
                            filter.Clear();
                            filter.Append("user_id = ");
                            filter.Append(User);
                        }
                        else
                        {
                            filter.Append(" AND user_id = ");
                            filter.Append(User);
                        }
                    }
                }
                #endregion

                #region cycle filter
                if (!string.IsNullOrEmpty(ExpCycle))
                {
                    long id = Convert.ToInt64(ExpCycle);
                    if (id > 1)
                    {
                        if (filter.ToString().Length == 0)
                        {
                            filter.Clear();
                            filter.Append("cycle_id = ");
                            filter.Append(ExpCycle);
                        }
                        else
                        {
                            filter.Append(" AND cycle_id = ");
                            filter.Append(ExpCycle);
                        }
                    }
                }
                #endregion

                #region density filter
                if (!string.IsNullOrEmpty(Density))
                {
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("density >= ");
                        filter.Append(Density.Replace(",", "."));
                    }
                    else
                    {
                        filter.Append(" AND density >= ");
                        filter.Append(Density.Replace(",", "."));
                    }
                }
                #endregion

                #region date filter
                if (!string.IsNullOrEmpty(Date))
                {
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("created >= #");
                        filter.Append(Date);
                        filter.Append("#");
                    }
                    else
                    {
                        filter.Append(" AND created >= #");
                        filter.Append(Date);
                        filter.Append("#");
                    }
                }
                #endregion
            }

            return filter.ToString();
        }

    }
}
