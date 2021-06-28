using LabBook.ADO.Common;
using LabBook.Forms.MainForm.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class ExperimentalAshBurnRepository : RepositoryCommon<ExpAshBurns>
    {
        public static readonly string GetByLabbokIdQuery = "Select labbook_id, solid, ash_450, ash_900, organic, titanium_dioxide, " +
            "calcium_carbonate, others, voc_id, voc_content, crucible_1, crucible_2, crucible_3, paint_1, paint_2, paint_3, " +
            "crucible_105_1, crucible_105_2, crucible_105_3, crucible_405_1, crucible_405_2, crucible_405_3, crucible_900_1, " +
            "crucible_900_2, crucible_900_3, date_created, date_update from LabBook.dbo.ExpAshBurn Where labbook_id = @id";
        private static readonly string _getVocClass = "Select * From LabBook.dbo.CmbVOC";
        public static readonly string ExistByLabBookIdQuery = "Select COUNT(1) From LabBook.dbo.ExpAshBurn Where labbook_id=@id";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.ExpAshBurn(labbook_id, solid, ash_450, ash_900, organic, " +
            "titanium_dioxide, calcium_carbonate, others, voc_id, voc_content, crucible_1, crucible_2, crucible_3, paint_1, paint_2, paint_3, " +
            "crucible_105_1, crucible_105_2, crucible_105_3, crucible_405_1, crucible_405_2, crucible_405_3, crucible_900_1, " +
            "crucible_900_2, crucible_900_3, date_created, date_update) Values(@labbook_id, @solid, @ash_450, @ash_900, @organic, " +
            "@titanium_dioxide, @calcium_carbonate, @others, @voc_id, @voc_content, @crucible_1, @crucible_2, @crucible_3, " +
            "@paint_1, @paint_2, @paint_3, @crucible_105_1, @crucible_105_2, @crucible_105_3, @crucible_405_1, @crucible_405_2" +
            "@crucible_405_3, @crucible_900_1, @crucible_900_2, @crucible_900_3, @date_created, @date_update)";
        public static readonly string UpdateQuery = "Update LabBook.dbo.ExpAshBurn Set solid=@solid, ash_450=@ash_450, ash_900=@ash_900, " +
            "organic=@organic, titanium_dioxide=@titanium_dioxide, calcium_carbonate=@calcium_carbonate, others=@others, voc_id=@voc_id, " +
            "voc_content=@voc_content, crucible_1=@crucible_1, crucible_2=@crucible_2, crucible_3=@crucible_3, paint_1=@paint_1, paint_2=@paint_2, " +
            "paint_3=@paint_3, crucible_105_1=@crucible_105_1, crucible_105_2=@crucible_105_2, crucible_105_3=@crucible_105_3, " +
            "crucible_405_1=@crucible_405_1, crucible_405_2=@crucible_405_2, crucible_405_3=@crucible_405_3, crucible_900_1=@crucible_900_1, " +
            "crucible_900_2=@crucible_900_2, crucible_900_3=@crucible_900_3, date_update=@date_update Where labbook_id = @labbook_id";

        public override ExpAshBurns GetById(long labBookId, string query)
        {
            ExpAshBurns expAshBurncs = new ExpAshBurns(labBookId);

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    var sqlCmd = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                    _ = sqlCmd.Parameters.AddWithValue("@id", labBookId);
                    connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows)
                    {
                        _ = reader.Read();
                        expAshBurncs.Solid = !reader[1].Equals(DBNull.Value) ? Convert.ToDouble(reader[1]) : -1f;
                        expAshBurncs.Ash450 = !reader[2].Equals(DBNull.Value) ? Convert.ToDouble(reader[2]) : -1f;
                        expAshBurncs.Ash900 = !reader[3].Equals(DBNull.Value) ? Convert.ToDouble(reader[3]) : -1f;
                        expAshBurncs.Organic = !reader[4].Equals(DBNull.Value) ? Convert.ToDouble(reader[4]) : -1f;
                        expAshBurncs.Titanium = !reader[5].Equals(DBNull.Value) ? Convert.ToDouble(reader[5]) : -1f;
                        expAshBurncs.Chalk = !reader[6].Equals(DBNull.Value) ? Convert.ToDouble(reader[6]) : -1f;
                        expAshBurncs.Others = !reader[7].Equals(DBNull.Value) ? Convert.ToDouble(reader[7]) : -1f;
                        expAshBurncs.VocCatId = Convert.ToInt32(reader[8]);
                        expAshBurncs.VocAmount = !reader[9].Equals(DBNull.Value) ? Convert.ToString(reader[9]) : null;
                        expAshBurncs.Crucible1 = !reader[10].Equals(DBNull.Value) ? Convert.ToDouble(reader[10]) : -1f;
                        expAshBurncs.Crucible2 = !reader[11].Equals(DBNull.Value) ? Convert.ToDouble(reader[11]) : -1f;
                        expAshBurncs.Crucible3 = !reader[12].Equals(DBNull.Value) ? Convert.ToDouble(reader[12]) : -1f;
                        expAshBurncs.Paint1 = !reader[13].Equals(DBNull.Value) ? Convert.ToDouble(reader[13]) : -1f;
                        expAshBurncs.Paint2 = !reader[14].Equals(DBNull.Value) ? Convert.ToDouble(reader[14]) : -1f;
                        expAshBurncs.Paint3 = !reader[15].Equals(DBNull.Value) ? Convert.ToDouble(reader[15]) : -1f;
                        expAshBurncs.Crucible105_1 = !reader[16].Equals(DBNull.Value) ? Convert.ToDouble(reader[16]) : -1f;
                        expAshBurncs.Crucible105_2 = !reader[17].Equals(DBNull.Value) ? Convert.ToDouble(reader[17]) : -1f;
                        expAshBurncs.Crucible105_3 = !reader[18].Equals(DBNull.Value) ? Convert.ToDouble(reader[18]) : -1f;
                        expAshBurncs.Crucible405_1 = !reader[19].Equals(DBNull.Value) ? Convert.ToDouble(reader[19]) : -1f;
                        expAshBurncs.Crucible405_2 = !reader[20].Equals(DBNull.Value) ? Convert.ToDouble(reader[20]) : -1f;
                        expAshBurncs.Crucible405_3 = !reader[21].Equals(DBNull.Value) ? Convert.ToDouble(reader[21]) : -1f;
                        expAshBurncs.Crucible900_1 = !reader[22].Equals(DBNull.Value) ? Convert.ToDouble(reader[22]) : -1f;
                        expAshBurncs.Crucible900_2 = !reader[23].Equals(DBNull.Value) ? Convert.ToDouble(reader[23]) : -1f;
                        expAshBurncs.Crucible900_3 = !reader[24].Equals(DBNull.Value) ? Convert.ToDouble(reader[24]) : -1f;
                        expAshBurncs.Created = Convert.ToDateTime(reader[25]);
                        expAshBurncs.Updated = Convert.ToDateTime(reader[26]);
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

            return expAshBurncs;
        }

        public override ExpAshBurns Save(ExpAshBurns expAshBurns)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SaveQuery;
                    cmd.Parameters.AddWithValue("@labbook_id", expAshBurns.LabBookId);
                    cmd.Parameters.AddWithValue("@voc_id", expAshBurns.VocCatId);
                    cmd.Parameters.AddWithValue("@date_created", expAshBurns.Created);
                    cmd.Parameters.AddWithValue("@date_update", expAshBurns.Updated);

                    if (expAshBurns.Solid == -1) cmd.Parameters.AddWithValue("@solid", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@solid", expAshBurns.Solid);

                    if (expAshBurns.Ash450 == -1) cmd.Parameters.AddWithValue("@ash_450", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@ash_450", expAshBurns.Ash450);

                    if (expAshBurns.Ash900 == -1) cmd.Parameters.AddWithValue("@ash_900", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@ash_900", expAshBurns.Ash900);

                    if (expAshBurns.Organic == -1) cmd.Parameters.AddWithValue("@organic", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@organic", expAshBurns.Organic);

                    if (expAshBurns.Titanium == -1) cmd.Parameters.AddWithValue("@titanium_dioxide", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@titanium_dioxide", expAshBurns.Titanium);

                    if (expAshBurns.Chalk == -1) cmd.Parameters.AddWithValue("@calcium_carbonate", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@calcium_carbonate", expAshBurns.Chalk);

                    if (expAshBurns.Others == -1) cmd.Parameters.AddWithValue("@others", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@others", expAshBurns.Others);

                    if (string.IsNullOrEmpty(expAshBurns.VocAmount)) cmd.Parameters.AddWithValue("@voc_content", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@voc_content", expAshBurns.VocAmount);

                    if (expAshBurns.Crucible1 == -1) cmd.Parameters.AddWithValue("@crucible_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_1", expAshBurns.Crucible1);

                    if (expAshBurns.Crucible2 == -1) cmd.Parameters.AddWithValue("@crucible_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_2", expAshBurns.Crucible2);

                    if (expAshBurns.Crucible3 == -1) cmd.Parameters.AddWithValue("@crucible_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_3", expAshBurns.Crucible3);

                    if (expAshBurns.Paint1 == -1) cmd.Parameters.AddWithValue("@paint_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@paint_1", expAshBurns.Paint1);

                    if (expAshBurns.Paint2 == -1) cmd.Parameters.AddWithValue("@paint_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@paint_2", expAshBurns.Paint2);

                    if (expAshBurns.Paint3 == -1) cmd.Parameters.AddWithValue("@paint_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@paint_3", expAshBurns.Paint3);

                    if (expAshBurns.Crucible105_1 == -1) cmd.Parameters.AddWithValue("@crucible_105_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_105_1", expAshBurns.Crucible105_1);

                    if (expAshBurns.Crucible105_2 == -1) cmd.Parameters.AddWithValue("@crucible_105_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_105_2", expAshBurns.Crucible105_2);

                    if (expAshBurns.Crucible105_3 == -1) cmd.Parameters.AddWithValue("@crucible_105_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_105_3", expAshBurns.Crucible105_3);

                    if (expAshBurns.Crucible405_1 == -1) cmd.Parameters.AddWithValue("@crucible_405_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_405_1", expAshBurns.Crucible405_1);

                    if (expAshBurns.Crucible405_2 == -1) cmd.Parameters.AddWithValue("@crucible_405_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_405_2", expAshBurns.Crucible405_2);

                    if (expAshBurns.Crucible405_3 == -1) cmd.Parameters.AddWithValue("@crucible_405_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_405_3", expAshBurns.Crucible405_3);

                    if (expAshBurns.Crucible900_1 == -1) cmd.Parameters.AddWithValue("@crucible_900_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_900_1", expAshBurns.Crucible900_1);

                    if (expAshBurns.Crucible900_2 == -1) cmd.Parameters.AddWithValue("@crucible_900_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_900_2", expAshBurns.Crucible900_2);

                    if (expAshBurns.Crucible900_3 == -1) cmd.Parameters.AddWithValue("@crucible_900_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_900_3", expAshBurns.Crucible900_3);

                    connection.Open();
                    cmd.ExecuteNonQuery();
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

            return expAshBurns;
        }

        public override void Update(ExpAshBurns expAshBurns)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = UpdateQuery;
                    cmd.Parameters.AddWithValue("@labbook_id", expAshBurns.LabBookId);
                    cmd.Parameters.AddWithValue("@voc_id", expAshBurns.VocCatId);
                    cmd.Parameters.AddWithValue("@date_update", expAshBurns.Updated);

                    if (expAshBurns.Solid == -1) cmd.Parameters.AddWithValue("@solid", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@solid", expAshBurns.Solid);

                    if (expAshBurns.Ash450 == -1) cmd.Parameters.AddWithValue("@ash_450", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@ash_450", expAshBurns.Ash450);

                    if (expAshBurns.Ash900 == -1) cmd.Parameters.AddWithValue("@ash_900", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@ash_900", expAshBurns.Ash900);

                    if (expAshBurns.Organic == -1) cmd.Parameters.AddWithValue("@organic", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@organic", expAshBurns.Organic);

                    if (expAshBurns.Titanium == -1) cmd.Parameters.AddWithValue("@titanium_dioxide", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@titanium_dioxide", expAshBurns.Titanium);

                    if (expAshBurns.Chalk == -1) cmd.Parameters.AddWithValue("@calcium_carbonate", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@calcium_carbonate", expAshBurns.Chalk);

                    if (expAshBurns.Others == -1) cmd.Parameters.AddWithValue("@others", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@others", expAshBurns.Others);

                    if (string.IsNullOrEmpty(expAshBurns.VocAmount)) cmd.Parameters.AddWithValue("@voc_content", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@voc_content", expAshBurns.VocAmount);

                    if (expAshBurns.Crucible1 == -1) cmd.Parameters.AddWithValue("@crucible_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_1", expAshBurns.Crucible1);

                    if (expAshBurns.Crucible2 == -1) cmd.Parameters.AddWithValue("@crucible_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_2", expAshBurns.Crucible2);

                    if (expAshBurns.Crucible3 == -1) cmd.Parameters.AddWithValue("@crucible_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_3", expAshBurns.Crucible3);

                    if (expAshBurns.Paint1 == -1) cmd.Parameters.AddWithValue("@paint_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@paint_1", expAshBurns.Paint1);

                    if (expAshBurns.Paint2 == -1) cmd.Parameters.AddWithValue("@paint_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@paint_2", expAshBurns.Paint2);

                    if (expAshBurns.Paint3 == -1) cmd.Parameters.AddWithValue("@paint_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@paint_3", expAshBurns.Paint3);

                    if (expAshBurns.Crucible105_1 == -1) cmd.Parameters.AddWithValue("@crucible_105_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_105_1", expAshBurns.Crucible105_1);

                    if (expAshBurns.Crucible105_2 == -1) cmd.Parameters.AddWithValue("@crucible_105_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_105_2", expAshBurns.Crucible105_2);

                    if (expAshBurns.Crucible105_3 == -1) cmd.Parameters.AddWithValue("@crucible_105_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_105_3", expAshBurns.Crucible105_3);

                    if (expAshBurns.Crucible405_1 == -1) cmd.Parameters.AddWithValue("@crucible_405_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_405_1", expAshBurns.Crucible405_1);

                    if (expAshBurns.Crucible405_2 == -1) cmd.Parameters.AddWithValue("@crucible_405_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_405_2", expAshBurns.Crucible405_2);

                    if (expAshBurns.Crucible405_3 == -1) cmd.Parameters.AddWithValue("@crucible_405_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_405_3", expAshBurns.Crucible405_3);

                    if (expAshBurns.Crucible900_1 == -1) cmd.Parameters.AddWithValue("@crucible_900_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_900_1", expAshBurns.Crucible900_1);

                    if (expAshBurns.Crucible900_2 == -1) cmd.Parameters.AddWithValue("@crucible_900_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_900_2", expAshBurns.Crucible900_2);

                    if (expAshBurns.Crucible900_3 == -1) cmd.Parameters.AddWithValue("@crucible_900_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@crucible_900_3", expAshBurns.Crucible900_3);

                    connection.Open();
                    cmd.ExecuteNonQuery();
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
        }

        public DataTable GetVOCClass()
        {
            DataTable table = new DataTable();

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(_getVocClass, connection);
                    _ = adapter.Fill(table);

                    DataColumn[] klucz = new DataColumn[1];
                    DataColumn id = table.Columns["id"];
                    klucz[0] = id;
                    table.PrimaryKey = klucz;
                }
                catch (SqlException ex)
                {
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu CreateTable VisRepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu CreateTable VisRepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return table;
        }

    }
}
