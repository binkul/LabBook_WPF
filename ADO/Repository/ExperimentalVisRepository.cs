using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using LabBook.Security;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class ExperimentalVisRepository : IRepository<ExperimentalVisDto>
    {
        private static readonly string _allQuery = "Select * from labbook.dbo.ExpViscosity";
        private static readonly string _allQueryByLabId = "Select id, labbook_id, date_created, date_update, " +
            "DATEDIFF(DAY, date_created, date_update) as days, pH, vis_type, brook_1, brook_5, brook_10, brook_20, " +
            "brook_30, brook_40, brook_50, brook_60, brook_70, brook_80, brook_90, brook_100, brook_comment, " +
            "brook_disc, brook_x_vis, brook_x_rpm, brook_x_disc, krebs, krebs_comment, ici, ici_disc, ici_comment, temp " +
            "From labbook.dbo.ExpViscosity Where labbook_id = ";
        private static readonly string _saveQuery = "Insert Into LabBook.dbo.ExpViscosity(labbook_id, date_created, date_update, " +
            "pH, vis_type, brook_1, brook_5, brook_10, brook_20, brook_30, brook_40, brook_50, brook_60, brook_70, brook_80, " +
            "brook_90, brook_100, brook_comment, brook_disc, brook_x_vis, brook_x_rpm, brook_x_disc, krebs, krebs_comment, " +
            "ici, ici_disc, ici_comment, temp) Values(@labbook_id, @date_created, @date_update, @pH, @vis_type, @brook_1, " +
            "@brook_5, @brook_10, @brook_20, @brook_30, @brook_40, @brook_50, @brook_60, @brook_70, @brook_80, " +
            "@brook_90, @brook_100, @brook_comment, @brook_disc, @brook_x_vis, @brook_x_rpm, @brook_x_disc, @krebs, " +
            "@krebs_comment, @ici, @ici_disc, @ici_comment, @temp)";
        private static readonly string _updateQuery = "Update LabBook.dbo.ExpViscosity(labbook_id=@labbook_id, date_created=@date_created, " +
            "date_update=@date_update, pH=@pH, vis_type=@vis_type, brook_1=@brook_1, brook_5=@brook_5, brook_10=@brook_10, " +
            "brook_20=@brook_20, brook_30=@brook_30, brook_40=@brook_40, brook_50=@brook_50, brook_60=@brook_60, brook_70=@brook_70, " +
            "brook_80=@brook_80, brook_90=@brook_90, brook_100=@brook_100, brook_comment=@brook_comment, brook_disc=@brook_disc, " +
            "brook_x_vis=@brook_x_vis, brook_x_rpm=@brook_x_rpm, brook_x_disc=@brook_x_disc, krebs=@krebs,, krebs_comment=@krebs_comment, " +
            "ici=@ici, ici_disc=@ici_disc, ici_comment=@ici_comment, temp=@temp Where id=@id ";
        private readonly User _user;

        public ExperimentalVisRepository(User user)
        {
            _user = user;
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public DataTable CreateTable()
        {
            var query = _allQueryByLabId + "-1";
            DataTable table = new DataTable();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, _user.Connection);

                adapter.Fill(table);

                DataColumn[] klucz = new DataColumn[1];
                DataColumn id = table.Columns["id"];
                klucz[0] = id;
                table.PrimaryKey = klucz;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                    "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                    "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return table;
        }

        public void RefreshMainTable(DataTable dataTable, long labBooklid)
        {
            var query = _allQueryByLabId + labBooklid;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, _user.Connection);

                adapter.Fill(dataTable);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                    "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                    "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public DataTable GetAll()
        {
            var query = _allQuery;
            var table = new DataTable();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, _user.Connection);

                adapter.Fill(table);

                DataColumn[] klucz = new DataColumn[1];
                DataColumn id = table.Columns["id"];
                klucz[0] = id;
                table.PrimaryKey = klucz;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                    "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                    "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return table;
        }

        public DataTable GetAllxViscosity()
        {
            return null;
        }

        public DataTable GetAllKrebs()
        {
            return null;
        }

        public DataTable GetAllICI()
        {
            return null;
        }

        public ExceptionCode Save(DataRow row)
        {
            ExceptionCode error = ExceptionCode.NoError;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _user.Connection;
            cmd.CommandText = _saveQuery;

            try
            {
                cmd.Parameters.AddWithValue("@labbook_id", row["labbook_id"]);
                cmd.Parameters.AddWithValue("@date_created", row["date_created"]);
                cmd.Parameters.AddWithValue("@date_update", row["date_update"]);
                cmd.Parameters.AddWithValue("@pH", row["pH"]);
                cmd.Parameters.AddWithValue("@vis_type", row["vis_type"]);
                cmd.Parameters.AddWithValue("@brook_1", row["brook_1"]);
                cmd.Parameters.AddWithValue("@brook_5", row["brook_5"]);
                cmd.Parameters.AddWithValue("@brook_10", row["brook_10"]);
                cmd.Parameters.AddWithValue("@brook_20", row["brook_20"]);
                cmd.Parameters.AddWithValue("@brook_30", row["brook_30"]);
                cmd.Parameters.AddWithValue("@brook_40", row["brook_40"]);
                cmd.Parameters.AddWithValue("@brook_50", row["brook_50"]);
                cmd.Parameters.AddWithValue("@brook_60", row["brook_60"]);
                cmd.Parameters.AddWithValue("@brook_70", row["brook_70"]);
                cmd.Parameters.AddWithValue("@brook_80", row["brook_80"]);
                cmd.Parameters.AddWithValue("@brook_90", row["brook_90"]);
                cmd.Parameters.AddWithValue("@brook_100", row["brook_100"]);
                cmd.Parameters.AddWithValue("@brook_comment", row["brook_comment"]);
                cmd.Parameters.AddWithValue("@brook_disc", row["brook_disc"]);
                cmd.Parameters.AddWithValue("@brook_x_vis", row["brook_x_vis"]);
                cmd.Parameters.AddWithValue("@brook_x_rpm", row["brook_x_rpm"]);
                cmd.Parameters.AddWithValue("@brook_x_disc", row["brook_x_disc"]);
                cmd.Parameters.AddWithValue("@krebs", row["krebs"]);
                cmd.Parameters.AddWithValue("@krebs_comment", row["krebs_comment"]);
                cmd.Parameters.AddWithValue("@ici", row["ici"]);
                cmd.Parameters.AddWithValue("@ici_disc", row["ici_disc"]);
                cmd.Parameters.AddWithValue("@ici_comment", row["ici_comment"]);
                cmd.Parameters.AddWithValue("@temp", row["temp"]);
                _user.Connection.Open();
                cmd.ExecuteNonQuery();
                _user.Connection.Close();
            }
            catch (SqlException ex)
            {
                error = ExceptionCode.SqlError;
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                    "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                error = ExceptionCode.SqlConnectionError;
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                    "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _user.Connection.Close();
            }

            return error;
        }

        public ExperimentalVisDto Save(ExperimentalVisDto data)
        {
            throw new NotImplementedException();
        }

        public ExceptionCode Update(DataRow row)
        {
            ExceptionCode error = ExceptionCode.NoError;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _user.Connection;
            cmd.CommandText = _updateQuery;

            try
            {
                cmd.Parameters.AddWithValue("@labbook_id", row["labbook_id"]);
                cmd.Parameters.AddWithValue("@date_created", row["date_created"]);
                cmd.Parameters.AddWithValue("@date_update", row["date_update"]);
                cmd.Parameters.AddWithValue("@pH", row["pH"]);
                cmd.Parameters.AddWithValue("@vis_type", row["vis_type"]);
                cmd.Parameters.AddWithValue("@brook_1", row["brook_1"]);
                cmd.Parameters.AddWithValue("@brook_5", row["brook_5"]);
                cmd.Parameters.AddWithValue("@brook_10", row["brook_10"]);
                cmd.Parameters.AddWithValue("@brook_20", row["brook_20"]);
                cmd.Parameters.AddWithValue("@brook_30", row["brook_30"]);
                cmd.Parameters.AddWithValue("@brook_40", row["brook_40"]);
                cmd.Parameters.AddWithValue("@brook_50", row["brook_50"]);
                cmd.Parameters.AddWithValue("@brook_60", row["brook_60"]);
                cmd.Parameters.AddWithValue("@brook_70", row["brook_70"]);
                cmd.Parameters.AddWithValue("@brook_80", row["brook_80"]);
                cmd.Parameters.AddWithValue("@brook_90", row["brook_90"]);
                cmd.Parameters.AddWithValue("@brook_100", row["brook_100"]);
                cmd.Parameters.AddWithValue("@brook_comment", row["brook_comment"]);
                cmd.Parameters.AddWithValue("@brook_disc", row["brook_disc"]);
                cmd.Parameters.AddWithValue("@brook_x_vis", row["brook_x_vis"]);
                cmd.Parameters.AddWithValue("@brook_x_rpm", row["brook_x_rpm"]);
                cmd.Parameters.AddWithValue("@brook_x_disc", row["brook_x_disc"]);
                cmd.Parameters.AddWithValue("@krebs", row["krebs"]);
                cmd.Parameters.AddWithValue("@krebs_comment", row["krebs_comment"]);
                cmd.Parameters.AddWithValue("@ici", row["ici"]);
                cmd.Parameters.AddWithValue("@ici_disc", row["ici_disc"]);
                cmd.Parameters.AddWithValue("@ici_comment", row["ici_comment"]);
                cmd.Parameters.AddWithValue("@temp", row["temp"]);
                cmd.Parameters.AddWithValue("@id", row["id"]);
                _user.Connection.Open();
                cmd.ExecuteNonQuery();
                _user.Connection.Close();
            }
            catch (SqlException ex)
            {
                error = ExceptionCode.SqlError;
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                    "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                error = ExceptionCode.SqlConnectionError;
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                    "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _user.Connection.Close();
            }

            return error;
        }

        public ExperimentalVisDto Update(ExperimentalVisDto data)
        {
            throw new NotImplementedException();
        }
    }
}
