using LabBook.ADO.Common;
using LabBook.Forms.MainForm.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class ExperimentalCommonRepository : RepositoryCommon<ExpCommon>
    {
        public static readonly string GetByLabbokIdQuery = "Select labbook_id, scrub_ISO11998, scrub_ISO11998_class, scrub_brush, " +
            "drying_ISO9117_1, drying_ISO9117_3, yellowing_ISO7724, schock_ISO6272, person_ISO2409, koenig_ISO2409, " +
            "scratch_ISO6272_1, adhesion_ISO2409, stain_ISO2812_4, water_ISO2812_2, salt_spray_ISO9227, flash_rust, UV_test, " +
            "hardness, flow_limit, runoff, yield, other, date_created, date_update From LabBook.dbo.ExpCommon Where labbook_id = @id";
        private static readonly string _getScrubingClass = "Select * From LabBook.dbo.CmbScrubClass Order By name";
        public static readonly string ExistByLabBookIdQuery = "Select COUNT(1) From LabBook.dbo.ExpCommon Where labbook_id=@id";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.ExpCommon(labbook_id, scrub_ISO11998, scrub_ISO11998_class, scrub_brush, " +
            "drying_ISO9117_1, drying_ISO9117_3, yellowing_ISO7724, schock_ISO6272, person_ISO2409, koenig_ISO2409, " +
            "scratch_ISO6272_1, adhesion_ISO2409, stain_ISO2812_4, water_ISO2812_2, salt_spray_ISO9227, flash_rust, UV_test, " +
            "hardness, flow_limit, runoff, yield, other, date_created, date_update) Values(@labbook_id, @scrub_ISO11998, @scrub_ISO11998_class, " +
            "@scrub_brush, @drying_ISO9117_1, @drying_ISO9117_3, @yellowing_ISO7724, @schock_ISO6272, @person_ISO2409, " +
            "@koenig_ISO2409, @scratch_ISO6272_1, @adhesion_ISO2409, @stain_ISO2812_4, @water_ISO2812_2, @salt_spray_ISO9227, " +
            "@flash_rust, @UV_test, @hardness, @flow_limit, @runoff, @yield, @other, @date_created, @date_update)";
        public static readonly string UpdateQuery = "Update LabBook.dbo.ExpCommon Set scrub_ISO11998=@scrub_ISO11998, " +
            "scrub_ISO11998_class=@scrub_ISO11998_class, scrub_brush=@scrub_brush, drying_ISO9117_1=@drying_ISO9117_1, " +
            "drying_ISO9117_3=@drying_ISO9117_3, yellowing_ISO7724=@yellowing_ISO7724, schock_ISO6272=@schock_ISO6272, " +
            "person_ISO2409=@person_ISO2409, koenig_ISO2409=@koenig_ISO2409, scratch_ISO6272_1=@scratch_ISO6272_1, adhesion_ISO2409=@adhesion_ISO2409, " +
            "stain_ISO2812_4=@stain_ISO2812_4, water_ISO2812_2=@water_ISO2812_2, salt_spray_ISO9227=@salt_spray_ISO9227, " +
            "flash_rust=@flash_rust, UV_test=@UV_test, hardness=@hardness, flow_limit=@flow_limit, runoff=@runoff, yield=@yield, " +
            "other=@other, date_update=@date_update Where labbook_id = @labbook_id";

        public override ExpCommon GetById(long labBookId, string query)
        {
            ExpCommon expCommon = new ExpCommon(labBookId);

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
                        expCommon.ScrubISO11998 = !reader[1].Equals(DBNull.Value) ? Convert.ToString(reader[1]) : null;
                        expCommon.ScrubISO11998Class = Convert.ToInt64(reader[2]);
                        expCommon.ScrubBrush = !reader[3].Equals(DBNull.Value) ? Convert.ToString(reader[3]) : null;
                        expCommon.DryingISO9117_1 = !reader[4].Equals(DBNull.Value) ? Convert.ToString(reader[4]) : null;
                        expCommon.DryingISO9117_3 = !reader[5].Equals(DBNull.Value) ? Convert.ToString(reader[5]) : null;
                        expCommon.YellowingISO7724 = !reader[6].Equals(DBNull.Value) ? Convert.ToString(reader[6]) : null;
                        expCommon.SchockISO6272 = !reader[7].Equals(DBNull.Value) ? Convert.ToString(reader[7]) : null;
                        expCommon.PersozISO2409 = !reader[8].Equals(DBNull.Value) ? Convert.ToString(reader[8]) : null;
                        expCommon.KoenigISO2409 = !reader[9].Equals(DBNull.Value) ? Convert.ToString(reader[9]) : null;
                        expCommon.ScratchISO6272_1 = !reader[10].Equals(DBNull.Value) ? Convert.ToString(reader[10]) : null;
                        expCommon.AdhesionISO2409 = !reader[11].Equals(DBNull.Value) ? Convert.ToString(reader[11]) : null;
                        expCommon.StainISO2812_4 = !reader[12].Equals(DBNull.Value) ? Convert.ToString(reader[12]) : null;
                        expCommon.WaterISO2812_2 = !reader[13].Equals(DBNull.Value) ? Convert.ToString(reader[13]) : null;
                        expCommon.SaltSprayISO9227 = !reader[14].Equals(DBNull.Value) ? Convert.ToString(reader[14]) : null;
                        expCommon.FlashRust = !reader[15].Equals(DBNull.Value) ? Convert.ToString(reader[15]) : null;
                        expCommon.UV = !reader[16].Equals(DBNull.Value) ? Convert.ToString(reader[16]) : null;
                        expCommon.Hardness = !reader[17].Equals(DBNull.Value) ? Convert.ToString(reader[17]) : null;
                        expCommon.FlowLimit = !reader[18].Equals(DBNull.Value) ? Convert.ToString(reader[18]) : null;
                        expCommon.RunOff = !reader[19].Equals(DBNull.Value) ? Convert.ToString(reader[19]) : null;
                        expCommon.Yield = !reader[20].Equals(DBNull.Value) ? Convert.ToString(reader[20]) : null;
                        expCommon.Other = !reader[21].Equals(DBNull.Value) ? Convert.ToString(reader[21]) : null;
                        expCommon.Created = Convert.ToDateTime(reader[22]);
                        expCommon.Updated = Convert.ToDateTime(reader[23]);
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

            return expCommon;
        }

        public override ExpCommon Save(ExpCommon expCommon)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SaveQuery;
                    cmd.Parameters.AddWithValue("@labbook_id", expCommon.LabBookId);
                    cmd.Parameters.AddWithValue("@scrub_ISO11998_class", expCommon.ScrubISO11998Class);
                    cmd.Parameters.AddWithValue("@date_created", expCommon.Created);
                    cmd.Parameters.AddWithValue("@date_update", expCommon.Updated);

                    if (string.IsNullOrEmpty(expCommon.ScrubISO11998)) cmd.Parameters.AddWithValue("@scrub_ISO11998", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@scrub_ISO11998", expCommon.ScrubISO11998);

                    if (string.IsNullOrEmpty(expCommon.ScrubBrush)) cmd.Parameters.AddWithValue("@scrub_brush", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@scrub_brush", expCommon.ScrubBrush);

                    if (string.IsNullOrEmpty(expCommon.DryingISO9117_1)) cmd.Parameters.AddWithValue("@drying_ISO9117_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@drying_ISO9117_1", expCommon.DryingISO9117_1);

                    if (string.IsNullOrEmpty(expCommon.DryingISO9117_3)) cmd.Parameters.AddWithValue("@drying_ISO9117_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@drying_ISO9117_3", expCommon.DryingISO9117_3);

                    if (string.IsNullOrEmpty(expCommon.YellowingISO7724)) cmd.Parameters.AddWithValue("@yellowing_ISO7724", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@yellowing_ISO7724", expCommon.YellowingISO7724);

                    if (string.IsNullOrEmpty(expCommon.SchockISO6272)) cmd.Parameters.AddWithValue("@schock_ISO6272", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@schock_ISO6272", expCommon.SchockISO6272);

                    if (string.IsNullOrEmpty(expCommon.PersozISO2409)) cmd.Parameters.AddWithValue("@person_ISO2409", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@person_ISO2409", expCommon.PersozISO2409);

                    if (string.IsNullOrEmpty(expCommon.KoenigISO2409)) cmd.Parameters.AddWithValue("@koenig_ISO2409", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@koenig_ISO2409", expCommon.KoenigISO2409);

                    if (string.IsNullOrEmpty(expCommon.ScratchISO6272_1)) cmd.Parameters.AddWithValue("@scratch_ISO6272_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@scratch_ISO6272_1", expCommon.ScratchISO6272_1);

                    if (string.IsNullOrEmpty(expCommon.AdhesionISO2409)) cmd.Parameters.AddWithValue("@adhesion_ISO2409", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@adhesion_ISO2409", expCommon.AdhesionISO2409);

                    if (string.IsNullOrEmpty(expCommon.StainISO2812_4)) cmd.Parameters.AddWithValue("@stain_ISO2812_4", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@stain_ISO2812_4", expCommon.StainISO2812_4);

                    if (string.IsNullOrEmpty(expCommon.WaterISO2812_2)) cmd.Parameters.AddWithValue("@water_ISO2812_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@water_ISO2812_2", expCommon.WaterISO2812_2);

                    if (string.IsNullOrEmpty(expCommon.SaltSprayISO9227)) cmd.Parameters.AddWithValue("@salt_spray_ISO9227", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@salt_spray_ISO9227", expCommon.SaltSprayISO9227);

                    if (string.IsNullOrEmpty(expCommon.FlashRust)) cmd.Parameters.AddWithValue("@flash_rust", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@flash_rust", expCommon.FlashRust);

                    if (string.IsNullOrEmpty(expCommon.UV)) cmd.Parameters.AddWithValue("@UV_test", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@UV_test", expCommon.UV);

                    if (string.IsNullOrEmpty(expCommon.Hardness)) cmd.Parameters.AddWithValue("@hardness", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@hardness", expCommon.Hardness);

                    if (string.IsNullOrEmpty(expCommon.FlowLimit)) cmd.Parameters.AddWithValue("@flow_limit", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@flow_limit", expCommon.FlowLimit);

                    if (string.IsNullOrEmpty(expCommon.RunOff)) cmd.Parameters.AddWithValue("@runoff", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@runoff", expCommon.RunOff);

                    if (string.IsNullOrEmpty(expCommon.Yield)) cmd.Parameters.AddWithValue("@yield", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@yield", expCommon.Yield);

                    if (string.IsNullOrEmpty(expCommon.Other)) cmd.Parameters.AddWithValue("@other", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@other", expCommon.Other);

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

            return expCommon;
        }

        public override void Update(ExpCommon data)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = UpdateQuery;
                    cmd.Parameters.AddWithValue("@labbook_id", data.LabBookId);
                    cmd.Parameters.AddWithValue("@scrub_ISO11998_class", data.ScrubISO11998Class);
                    cmd.Parameters.AddWithValue("@date_update", DateTime.Now);

                    if (string.IsNullOrEmpty(data.ScrubISO11998)) cmd.Parameters.AddWithValue("@scrub_ISO11998", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@scrub_ISO11998", data.ScrubISO11998);

                    if (string.IsNullOrEmpty(data.ScrubBrush)) cmd.Parameters.AddWithValue("@scrub_brush", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@scrub_brush", data.ScrubBrush);

                    if (string.IsNullOrEmpty(data.DryingISO9117_1)) cmd.Parameters.AddWithValue("@drying_ISO9117_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@drying_ISO9117_1", data.DryingISO9117_1);

                    if (string.IsNullOrEmpty(data.DryingISO9117_3)) cmd.Parameters.AddWithValue("@drying_ISO9117_3", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@drying_ISO9117_3", data.DryingISO9117_3);

                    if (string.IsNullOrEmpty(data.YellowingISO7724)) cmd.Parameters.AddWithValue("@yellowing_ISO7724", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@yellowing_ISO7724", data.YellowingISO7724);

                    if (string.IsNullOrEmpty(data.SchockISO6272)) cmd.Parameters.AddWithValue("@schock_ISO6272", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@schock_ISO6272", data.SchockISO6272);

                    if (string.IsNullOrEmpty(data.PersozISO2409)) cmd.Parameters.AddWithValue("@person_ISO2409", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@person_ISO2409", data.PersozISO2409);

                    if (string.IsNullOrEmpty(data.KoenigISO2409)) cmd.Parameters.AddWithValue("@koenig_ISO2409", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@koenig_ISO2409", data.KoenigISO2409);

                    if (string.IsNullOrEmpty(data.ScratchISO6272_1)) cmd.Parameters.AddWithValue("@scratch_ISO6272_1", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@scratch_ISO6272_1", data.ScratchISO6272_1);

                    if (string.IsNullOrEmpty(data.AdhesionISO2409)) cmd.Parameters.AddWithValue("@adhesion_ISO2409", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@adhesion_ISO2409", data.AdhesionISO2409);

                    if (string.IsNullOrEmpty(data.StainISO2812_4)) cmd.Parameters.AddWithValue("@stain_ISO2812_4", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@stain_ISO2812_4", data.StainISO2812_4);

                    if (string.IsNullOrEmpty(data.WaterISO2812_2)) cmd.Parameters.AddWithValue("@water_ISO2812_2", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@water_ISO2812_2", data.WaterISO2812_2);

                    if (string.IsNullOrEmpty(data.SaltSprayISO9227)) cmd.Parameters.AddWithValue("@salt_spray_ISO9227", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@salt_spray_ISO9227", data.SaltSprayISO9227);

                    if (string.IsNullOrEmpty(data.FlashRust)) cmd.Parameters.AddWithValue("@flash_rust", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@flash_rust", data.FlashRust);

                    if (string.IsNullOrEmpty(data.UV)) cmd.Parameters.AddWithValue("@UV_test", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@UV_test", data.UV);

                    if (string.IsNullOrEmpty(data.Hardness)) cmd.Parameters.AddWithValue("@hardness", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@hardness", data.Hardness);

                    if (string.IsNullOrEmpty(data.FlowLimit)) cmd.Parameters.AddWithValue("@flow_limit", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@flow_limit", data.FlowLimit);

                    if (string.IsNullOrEmpty(data.RunOff)) cmd.Parameters.AddWithValue("@runoff", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@runoff", data.RunOff);

                    if (string.IsNullOrEmpty(data.Yield)) cmd.Parameters.AddWithValue("@yield", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@yield", data.Yield);

                    if (string.IsNullOrEmpty(data.Other)) cmd.Parameters.AddWithValue("@other", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@other", data.Other);

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

        public DataTable GetScrubingClass()
        {
            DataTable table = new DataTable();

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(_getScrubingClass, connection);
                    adapter.Fill(table);

                    DataColumn[] klucz = new DataColumn[1];
                    DataColumn id = table.Columns["id"];
                    klucz[0] = id;
                    table.PrimaryKey = klucz;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu CreateTable VisRepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu CreateTable VisRepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return table;

        }
    }
}
