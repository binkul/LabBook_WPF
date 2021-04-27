using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.MainForm
{
    public class Filter
    {
        public static string SetFilter(LabBookForm form)
        {
            var filter = new StringBuilder("");

            if ((bool)form.ChbFilter.IsChecked)
            {

                #region id filer
                if (form.TxtNumerDFilter.Text.Length > 0)
                {
                    filter.Clear();
                    filter.Append("id >= ");
                    filter.Append(form.TxtNumerDFilter.Text);
                }
                #endregion

                #region title filter
                if (form.TxtTitleFilter.Text.Length > 0)
                {
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("title LIKE '");
                        filter.Append(form.TxtTitleFilter.Text);
                        filter.Append("*'");
                    }
                    else
                    {
                        filter.Append(" AND title LIKE '");
                        filter.Append(form.TxtTitleFilter.Text);
                        filter.Append("*'");
                    }
                }
                #endregion

                #region user filter
                if ((long)form.CmbUserFilter.SelectedValue > 0)
                {
                    long id = (long)form.CmbUserFilter.SelectedValue;
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("user_id = ");
                        filter.Append(id);
                    }
                    else
                    {
                        filter.Append(" AND user_id = ");
                        filter.Append(id);
                    }
                }
                #endregion

                #region cycle filter
                if ((long)form.CmbCycleFilter.SelectedValue > 1)
                {
                    long id = (long)form.CmbCycleFilter.SelectedValue;
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("cycle_id = ");
                        filter.Append(id);
                    }
                    else
                    {
                        filter.Append(" AND cycle_id = ");
                        filter.Append(id);
                    }
                }
                #endregion

                #region density filter
                if (!string.IsNullOrEmpty(form.TxtDensityFilter.Text))
                {
                    double.TryParse(form.TxtDensityFilter.Text, out double val);
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("density >= ");
                        filter.Append(val.ToString().Replace(",", "."));
                    }
                    else
                    {
                        filter.Append(" AND density >= ");
                        filter.Append(val.ToString().Replace(",", "."));
                    }
                }
                #endregion

                #region date filter
                if (!string.IsNullOrEmpty(form.DpDate.Text))
                {
                    if (filter.ToString().Length == 0)
                    {
                        filter.Clear();
                        filter.Append("created >= #");
                        filter.Append(form.DpDate.Text);
                        filter.Append("#");
                    }
                    else
                    {
                        filter.Append(" AND created >= #");
                        filter.Append(form.DpDate.Text);
                        filter.Append("#");
                    }
                }
                #endregion
            }

            return filter.ToString();
        }
    }
}
