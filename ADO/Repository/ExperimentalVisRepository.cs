using LabBook.ADO.Common;
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
        private static readonly string _allQueryByLabId = "Select * from labbook.dbo.ExpViscosity Where labbook_id = ";
        private static readonly string _allQuery = "Select * from labbook.dbo.ExpViscosity";
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

        public ExperimentalVisDto Save(ExperimentalVisDto data)
        {
            throw new NotImplementedException();
        }

        public ExperimentalVisDto Update(ExperimentalVisDto data)
        {
            throw new NotImplementedException();
        }
    }
}
