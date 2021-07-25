using System;
using System.Text;

namespace LabBook.Forms.Materials.ModelView
{
    public class FilterMV
    {
        private MaterialFormMV _materialFormMV;
        private bool _on = false;
        private string _name;
        private string _functionId;
        private bool _active;
        private bool _danger;
        private bool _production;

        public void SetWindowEdit(MaterialFormMV mv)
        {
            _materialFormMV = mv;
        }

        public bool FilterOn
        {
            private get => _on;
            set
            {
                _on = value;
                if (_materialFormMV != null)
                    _materialFormMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string Name
        {
            private get => _name;
            set
            {
                _name = value;
                if (_materialFormMV != null)
                    _materialFormMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string Function
        {
            private get => _functionId;
            set
            {
                _functionId = value;
                if (_materialFormMV != null)
                    _materialFormMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public bool Active
        {
            private get => _active;
            set
            {
                _active = value;
                if (_materialFormMV != null)
                    _materialFormMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public bool Danger
        {
            private get => _danger;
            set
            {
                _danger = value;
                if (_materialFormMV != null)
                    _materialFormMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public bool Production
        {
            private get => _production;
            set
            {
                _production = value;
                if (_materialFormMV != null)
                    _materialFormMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string GetFilterString()
        {
            StringBuilder filter = new StringBuilder("");

            if (FilterOn)
            {
                #region name filter
                if (!string.IsNullOrEmpty(Name))
                {
                    filter.Clear();
                    filter.Append("name LIKE '");
                    filter.Append(Name);
                    filter.Append("*'");
                }
                #endregion

                #region function filter
                if (!string.IsNullOrEmpty(Function))
                {
                    long id = Convert.ToInt64(Function);
                    if (id > 1)
                    {
                        if (filter.ToString().Length == 0)
                        {
                            filter.Clear();
                            filter.Append("function_id = ");
                            filter.Append(Function);
                        }
                        else
                        {
                            filter.Append(" AND function_id = ");
                            filter.Append(Function);
                        }
                    }
                }
                #endregion

                #region active filter
                if (_active)
                {
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("is_active = 'true'");
                    }
                    else
                    {
                        filter.Append(" AND is_active = 'true'");
                    }
                }
                #endregion

                #region danger filter
                if (_danger)
                {
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("is_danger = 'true'");
                    }
                    else
                    {
                        filter.Append(" AND is_danger = 'true'");
                    }
                }
                #endregion

                #region production filter
                if (_production)
                {
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("is_production = 'true'");
                    }
                    else
                    {
                        filter.Append(" AND is_production = 'true'");
                    }
                }
                #endregion

            }

            return filter.ToString();
        }

    }
}
