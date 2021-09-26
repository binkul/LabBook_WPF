using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class MaterialRepository : RepositoryCommon<MaterialDto>
    {
        public static readonly string AllMaterialQuery = "Select id, name, is_intermediate, is_danger, is_production, is_active, intermediate_nrD, clp_signal_word_id, clp_msds_id, function_id, " +
            "price, currency_id, unit_id, density, solids, ash_450, VOC, remarks, login_id, date_created, date_update From LabBook.dbo.Material Where is_intermediate = 'false' " +
            "Order by name";
        public static readonly string AllSemiProductQuery = "Select id, name, is_intermediate, is_danger, is_production, is_active, intermediate_nrD, clp_signal_word_id, clp_msds_id, function_id, " +
            "price, currency_id, unit_id, density, solids, ash_450, VOC, remarks, login_id, date_created, date_update From LabBook.dbo.Material Where is_intermediate = 'true' " +
            "Order by name";
        public static readonly string GetByNameQuery = "Select id, name, is_intermediate, is_danger, is_production, is_active, " +
            "intermediate_nrD, clp_signal_word_id, clp_msds_id, function_id, price, currency_id, unit_id, density, solids, " +
            "ash_450, VOC, remarks, login_id, date_created, date_update From LabBook.dbo.Material Where name=@name";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.Material(name, is_intermediate, is_danger, is_production, is_active, intermediate_nrD, clp_signal_word_id, " +
            "clp_msds_id, function_id, price, currency_id, unit_id, density, solids, ash_450, VOC, remarks, login_id, date_created, date_update) Values(@name, @is_intermediate, @is_danger, " +
            "@is_production, @is_active, @intermediate_nrD, @clp_signal_word_id, @clp_msds_id, @function_id, @price, @currency_id, @unit_id, @density, @solids, @ash_450, @VOC, @remarks, " +
            "@login_id, @date_created, @date_update)";
        public static readonly string UpdateQuery = "Update LabBook.dbo.Material Set name=@name, is_danger=@is_danger, is_production=@is_production," +
            "is_active=@is_active, clp_signal_word_id=@clp_signal_word_id, clp_msds_id=@clp_msds_id, function_id=@function_id, price=@price, currency_id=@currency_id, unit_id=@unit_id, density=@density, " +
            "solids=@solids, ash_450=@ash_450, VOC=@VOC, remarks=@remarks, date_update=@date_update Where id=@id";
        public static readonly string DeleteQuery = "Delete From LabBook.dbo.Material Where id=";
        public static readonly string ExistByNameQuery = "Select Count(1) From LabBook.dbo.Material m Where m.name=@name";
        public static readonly string ExistByIdQuery = "Select Count(1) From LabBook.dbo.Material m Where m.id=@id";
        public static readonly string ExistByIntDQuery = "Select Count(1) From LabBook.dbo.Material m Where m.intermediate_nrD=@id";
        public static readonly string GetForPrice = "Select c.amount, m.price, r.rate, ISNULL(m.intermediate_nrD, -1) as intermediate_nrD, m.is_intermediate " +
            "from LabBook.dbo.ExpComposition c Left Join(LabBook.dbo.Material m Left Join LabBook.dbo.CmbCurrency r on m.currency_id= r.id) on " +
            "c.component=m.name Where c.labbook_id=";
        public static readonly string GetForVOC = "Select c.amount, m.VOC, m.intermediate_nrD, m.is_intermediate from LabBook.dbo.ExpComposition c " +
            "Left Join LabBook.dbo.Material m on c.component= m.name Where c.labbook_id=";
        private static readonly string _getIdByNameQuery = "Select Max(id) as id From LabBook.dbo.Material Where name=@name";
        private static readonly string _saveMaterialGhs = "Insert Into LabBook.dbo.MaterialGHS(material_id, ghs_id, date_created) Values(@material_id, @ghs_id, @date_created)";
        private static readonly string _saveMaterialClp = "Insert Into LabBook.dbo.MaterialCLP(material_id, clp_id, date_created) Values(@material_id, @clp_id, @date_created)";

        public override ExceptionCode Update(DataRow row, string query)
        {
            ExceptionCode error = ExceptionCode.NoError;

            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@name", row["name"]);
                    cmd.Parameters.AddWithValue("@is_danger", row["is_danger"]);
                    cmd.Parameters.AddWithValue("@is_production", row["is_production"]);
                    cmd.Parameters.AddWithValue("@is_active", row["is_active"]);
                    cmd.Parameters.AddWithValue("@clp_signal_word_id", row["clp_signal_word_id"]);
                    cmd.Parameters.AddWithValue("@clp_msds_id", row["clp_msds_id"]);
                    cmd.Parameters.AddWithValue("@function_id", row["function_id"]);

                    if (string.IsNullOrEmpty(row["price"].ToString())) cmd.Parameters.AddWithValue("@price", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@price", row["price"]);

                    cmd.Parameters.AddWithValue("@currency_id", row["currency_id"]);
                    cmd.Parameters.AddWithValue("@unit_id", row["unit_id"]);

                    if (string.IsNullOrEmpty(row["density"].ToString())) cmd.Parameters.AddWithValue("@density", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@density", row["density"]);

                    if (string.IsNullOrEmpty(row["solids"].ToString())) cmd.Parameters.AddWithValue("@solids", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@solids", row["solids"]);

                    if (string.IsNullOrEmpty(row["ash_450"].ToString())) cmd.Parameters.AddWithValue("@ash_450", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@ash_450", row["ash_450"]);

                    if (string.IsNullOrEmpty(row["VOC"].ToString())) cmd.Parameters.AddWithValue("@VOC", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@VOC", row["VOC"]);

                    if (string.IsNullOrEmpty(row["remarks"].ToString())) cmd.Parameters.AddWithValue("@remarks", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@remarks", row["remarks"]);

                    cmd.Parameters.AddWithValue("@date_update", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", row["id"]);
                    connection.Open();
                    cmd.ExecuteNonQuery();
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
                    connection.Close();
                }
            }

            return error;
        }

        public override MaterialDto Save(MaterialDto data)
        {
            MaterialDto material = null;
            bool error = false;

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SaveQuery;
                    cmd.Parameters.AddWithValue("@name", data.Name);
                    cmd.Parameters.AddWithValue("@is_danger", data.IsDanger);
                    cmd.Parameters.AddWithValue("@is_production", data.IsProduction);
                    cmd.Parameters.AddWithValue("@is_intermediate", data.IsIntermediate);
                    cmd.Parameters.AddWithValue("@is_active", data.IsActive);
                    cmd.Parameters.AddWithValue("@clp_signal_word_id", data.ClpSignalWordId);
                    cmd.Parameters.AddWithValue("@clp_msds_id", data.ClpMsdsId);
                    cmd.Parameters.AddWithValue("@function_id", data.FunctionId);
                    cmd.Parameters.AddWithValue("@login_id", data.LoginId);
                    cmd.Parameters.AddWithValue("@intermediate_nrD", data.IntermediateNrD);

                    if (data.Price == 0) cmd.Parameters.AddWithValue("@price", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@price", data.Price);

                    cmd.Parameters.AddWithValue("@currency_id", data.CurrencyId);
                    cmd.Parameters.AddWithValue("@unit_id", data.UnitId);

                    if (data.Density == 0) cmd.Parameters.AddWithValue("@density", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@density", data.Density);

                    if (data.Solids == 0) cmd.Parameters.AddWithValue("@solids", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@solids", data.Solids);

                    if (data.Ash450 == 0) cmd.Parameters.AddWithValue("@ash_450", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@ash_450", data.Ash450);

                    if (data.VOC == 0) cmd.Parameters.AddWithValue("@VOC", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@VOC", data.VOC);

                    if (string.IsNullOrEmpty(data.Remarks)) cmd.Parameters.AddWithValue("@remarks", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@remarks", data.Remarks);

                    cmd.Parameters.AddWithValue("@date_update", data.DateCreated);
                    cmd.Parameters.AddWithValue("@date_created", data.DateUpdated);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    material = data;
                }
                catch (SqlException ex)
                {
                    error = true;
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    error = true;
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            if (!error)
                material.Id = GetId(data.Name);

            return material;
        }

        public override MaterialDto GetByName(string name)
        {
            MaterialDto material = new MaterialDto(name);

            using (SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand(GetByNameQuery, connection) { CommandType = CommandType.Text };
                    sqlCmd.Parameters.AddWithValue("@name", name);
                    connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        _ = reader.Read();
                        material.Id = reader.GetInt64(0);
                        material.IsIntermediate = reader.GetBoolean(2);
                        material.IsDanger = reader.GetBoolean(3);
                        material.IsProduction = reader.GetBoolean(4);
                        material.IsActive = reader.GetBoolean(5);
                        material.IntermediateNrD = reader.GetInt64(6);
                        material.ClpSignalWordId = reader.GetInt32(7);
                        material.ClpMsdsId = reader.GetInt32(8);
                        material.FunctionId = reader.GetInt32(9);
                        material.Price = !reader.GetValue(10).Equals(DBNull.Value) ? reader.GetDecimal(10) : 0M;
                        material.CurrencyId = reader.GetInt32(11);
                        material.UnitId = reader.GetInt32(12);
                        material.Density = !reader.GetValue(13).Equals(DBNull.Value) ? reader.GetDouble(13) : 0d;
                        material.Solids = !reader.GetValue(14).Equals(DBNull.Value) ? reader.GetDouble(14) : 0d;
                        material.Ash450 = !reader.GetValue(15).Equals(DBNull.Value) ? reader.GetDouble(15) : 0d;
                        material.VOC = !reader.GetValue(16).Equals(DBNull.Value) ? Convert.ToDouble(reader.GetDecimal(16)) : -1d;
                        material.Remarks = !reader.GetValue(17).Equals(DBNull.Value) ? reader.GetString(17) : null;
                        material.LoginId = reader.GetInt32(18);
                        material.DateCreated = reader.GetDateTime(19);
                        material.DateUpdated = reader.GetDateTime(20);
                    }
                }
                catch (SqlException ex)
                {
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            return material;
        }

        public bool SaveGhs(long materialId, int ghsId)
        {
            bool result = true;
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = _saveMaterialGhs;
                    cmd.Parameters.AddWithValue("@material_id", materialId);
                    cmd.Parameters.AddWithValue("@ghs_id", ghsId);
                    cmd.Parameters.AddWithValue("@date_created", DateTime.Now);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    result = false;
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    result = false;
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public bool SaveClp(long materialId, int clpId)
        {
            bool result = true;
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = _saveMaterialClp;
                    cmd.Parameters.AddWithValue("@material_id", materialId);
                    cmd.Parameters.AddWithValue("@clp_id", clpId);
                    cmd.Parameters.AddWithValue("@date_created", DateTime.Now);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    result = false;
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    result = false;
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public void RefreshTable(string query, DataTable dataTable)
        {
            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu RefreshMainTable VisRepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu RefreshMainTable VisRepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private long GetId(string name)
        {
            long id = -1L;

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    var sqlCmd = new SqlCommand(_getIdByNameQuery, connection) { CommandType = CommandType.Text };
                    sqlCmd.Parameters.AddWithValue("@name", name);
                    connection.Open();
                    id = Convert.ToInt64(sqlCmd.ExecuteScalar());
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
                finally
                {
                    connection.Close();
                }
            }

            return id;
        }
    }
}
