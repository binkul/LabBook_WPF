using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace LabBook.Forms.Tools
{
    public class UserToNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            long id = values[0] != null ? long.Parse(values[0].ToString()) : -1;
            DataView view = values[1] != null ? (DataView)values[1] : new DataView();
            DataTable table = view.ToTable();

            var result = table.AsEnumerable()
                .FirstOrDefault(row => row.Field<long>("id") == id);
                
            return result != null ? result["identifier"] : "Brak";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
