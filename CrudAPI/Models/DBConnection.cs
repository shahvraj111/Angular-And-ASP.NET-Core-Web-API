using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace CrudAPI.Models
{
    public class DBConnection
    {
        // string cs = @"server = localhost; port = 5432; user id = postgres; password =vraj; database = postgres";
        string cs = @"User ID=postgres;Password=vraj;server = localhost;Port=5432;Database=postgres;Pooling=true";

        public static string conString = Convert.ToString(ConfigurationManager.ConnectionStrings["cs"]);
        //public static string conString = Convert.ToString(ConfigurationManager.ConnectionStrings["ConnString_Local"]);
        public static string rpt_conString = Convert.ToString(ConfigurationManager.ConnectionStrings["56_ConnString"]);


        public NpgsqlConnection cn = new NpgsqlConnection(conString);
        public NpgsqlConnection rpt_con = new NpgsqlConnection(rpt_conString);
        NpgsqlCommand cmd = new NpgsqlCommand();
        //NpgsqlCommand cmd = null;
        NpgsqlDataAdapter ad = null;

        /* string cs = @"server = localhost; port = 5432; user id = postgres; password =vraj; database = postgres";


         public static string conString = Convert.ToString(ConfigurationManager.ConnectionStrings["cs"]);
         //public static string conString = Convert.ToString(ConfigurationManager.ConnectionStrings["ConnString_Local"]);
         public static string rpt_conString = Convert.ToString(ConfigurationManager.ConnectionStrings["56_ConnString"]);


         public NpgsqlConnection cn = new NpgsqlConnection(conString);
         public NpgsqlConnection rpt_con = new NpgsqlConnection(rpt_conString);
         NpgsqlCommand cmd = null;
         NpgsqlDataAdapter ad = null;
*/
        public static string DBConnectionString
            {
                get
                {
                    return conString;
                }
                set
                {
                    DBConnectionString = value;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="qry"></param>
            /// <param name="ds"></param>
            public void GetData(string qry, DataSet ds)
            {
                try
                {
                    using (cn = new NpgsqlConnection(conString))
                    {
                        //cn.Open();
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = cn;
                        cmd.CommandTimeout = 300;
                        cmd.CommandText = qry;
                        ad = new NpgsqlDataAdapter(cmd);
                        //ad = new SqlDataAdapter(qry, cn);
                        ad.Fill(ds);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //closeConnection(cn, cmd);
                }
            }

            public void GetData(NpgsqlCommand cmd, DataSet ds)
            {
                try
                {
                    using (cn = new NpgsqlConnection(conString))
                    {
                        cmd.Connection = cn;
                        cmd.CommandTimeout = 300;
                        ad = new NpgsqlDataAdapter(cmd);
                        ad.Fill(ds);
                    }
                }
                catch (Exception ex)
                {
                    throw ex; ;
                }
                finally
                {
                    //closeConnection(cn, cmd);
                }
            }

            public void GetData(NpgsqlCommand cmd, ref DataSet ds)
            {
                try
                {
                    using (cn = new NpgsqlConnection(conString))
                    {
                        cmd.Connection = cn;
                        ad = new NpgsqlDataAdapter(cmd);
                        ad.Fill(ds);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //closeConnection(cn, cmd);
                }
            }

            public void GetData(NpgsqlCommand cmd, DataTable ds)
            {
                try
                {
                    using (cn = new NpgsqlConnection(conString))
                    {
                        cmd.Connection = cn;
                        ad = new NpgsqlDataAdapter(cmd);
                        ad.Fill(ds);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    //closeConnection(cn, cmd);
                }
            }

            public DataSet getDataBy_SqlCommand_CB(NpgsqlCommand cmd)
            {
                NpgsqlConnection cn = new NpgsqlConnection(cs);
                DataSet dt = new DataSet();
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }
                    cmd.CommandTimeout = 100;
                    cmd.Connection = cn;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;

                }
                catch (Exception)
                {
                    return dt;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
            }

            public DataSet getDataBy_SqlCommand_BusCrew(NpgsqlCommand cmd)
            {
                NpgsqlConnection cn = new NpgsqlConnection(conString);
                DataSet dt = new DataSet();
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }
                    cmd.CommandTimeout = 500;
                    cmd.Connection = cn;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;

                }
                catch (Exception)
                {
                    return dt;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
            }

            public DataSet rpt_getDataByCommand(NpgsqlCommand cmd)
            {
                NpgsqlConnection cn = new NpgsqlConnection(rpt_conString);
                DataSet dt = new DataSet();
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }
                    cmd.CommandTimeout = 100;
                    cmd.Connection = cn;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;

                }
                catch (Exception)
                {
                    return dt;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
            }

            public DataSet getDataBy_SqlCommand_CB_PG_query(NpgsqlCommand cmd)
            {
                DataSet dt = new DataSet();
                try
                {
                    NpgsqlConnection cn = new NpgsqlConnection(conString);
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }
                    NpgsqlTransaction tr = (NpgsqlTransaction)cn.BeginTransaction();
                    string CursorName = "crb";
                    NpgsqlCommand cursCmd = new NpgsqlCommand("select dashboard('crb',94);fetch all in " + "\"" + CursorName + "\"", (NpgsqlConnection)cn);
                    //cursCmd.CommandType = CommandType.StoredProcedure;
                    cursCmd.Connection = cn;
                    cursCmd.Transaction = tr;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cursCmd);
                    da.Fill(dt);

                    //while (dr.Read())
                    //{
                    //    DataTable dtN = new DataTable();
                    //    string CursorName = "crb";
                    //    //cmd = new NpgsqlCommand("FETCH ALL IN " + "\"" + dr[0].ToString() + "\"", cn); //use plpgsql fetch command to get data back
                    //   cmd = new NpgsqlCommand("FETCH ALL IN " + "\"" + CursorName + "\"", cn); //use plpgsql fetch command to get data back
                    //    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    //    da.Fill(dtN);
                    //    //dtRtn.Add(dt); //all the data will save in the List<DataTable> ,no matter the connection is closed or returned multiple refcursors
                    //}
                    //dr.NextResult();
                    //while (dr.Read())
                    //{
                    //    ;
                    //}

                    tr.Commit();

                }
                catch (Exception)
                {

                    throw;
                }
                return dt;
                //NpgsqlConnection cn = new NpgsqlConnection(conString);
                //DataSet dt = new DataSet();
                //NpgsqlTransaction transaction = null;

                //try
                //{
                //    if (cn.State == ConnectionState.Closed)
                //    {
                //        cn.Open();
                //    }
                //    transaction = cn.BeginTransaction();
                //    cmd.Connection = cn;
                //    //NpgsqlTransaction transaction = null;                
                //    cmd.Transaction = transaction;
                //    NpgsqlDataReader dr = cmd.ExecuteReader();
                //    while (dr.Read())
                //    {
                //        DataTable dtN = new DataTable();
                //        string CursorName = "crb";
                //        //cmd = new NpgsqlCommand("FETCH ALL IN " + "\"" + dr[0].ToString() + "\"", cn); //use plpgsql fetch command to get data back
                //        cmd = new NpgsqlCommand("FETCH ALL IN " + "\"" + CursorName + "\"", cn); //use plpgsql fetch command to get data back
                //        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                //        da.Fill(dtN);
                //        //dtRtn.Add(dt); //all the data will save in the List<DataTable> ,no matter the connection is closed or returned multiple refcursors
                //    }
                //}
                //catch (Exception)
                //{
                //    return dt;
                //}

                //try
                //{
                //    if (cn.State == ConnectionState.Closed)
                //    {
                //        cn.Open();
                //    }
                //    cmd.CommandTimeout = 100;
                //    cmd.Connection = cn;
                //    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                //    da.Fill(dt);
                //    return dt;

                //}
                //catch (Exception)
                //{
                //    return dt;
                //}
                //finally
                //{
                //    if (cn.State == ConnectionState.Open)
                //    {
                //        cn.Close();
                //    }
                //    cmd.Dispose();
                //}

            }

            public DataSet getDataBy_SqlCommand_CB_PG(NpgsqlCommand cmd)
            {
                DataSet dt = new DataSet();
                try
                {
                    NpgsqlConnection cn = new NpgsqlConnection(conString);
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }
                    NpgsqlTransaction tr = (NpgsqlTransaction)cn.BeginTransaction();
                    NpgsqlCommand cursCmd = new NpgsqlCommand("dashboard", (NpgsqlConnection)cn);
                    cursCmd.CommandType = CommandType.StoredProcedure;
                    cursCmd.Connection = cn;
                    cursCmd.Transaction = tr;
                    NpgsqlParameter rf = new NpgsqlParameter("ref1", NpgsqlTypes.NpgsqlDbType.Refcursor);
                    rf.Direction = ParameterDirection.InputOutput;
                    rf.Value = "crb";
                    cursCmd.Parameters.Add(rf);

                    cursCmd.Parameters.AddWithValue("p_userid", 94);
                    NpgsqlDataReader dr = cursCmd.ExecuteReader();

                    while (dr.Read())
                    {
                        DataTable dtN = new DataTable();
                        string CursorName = "crb";
                        //cmd = new NpgsqlCommand("FETCH ALL IN " + "\"" + dr[0].ToString() + "\"", cn); //use plpgsql fetch command to get data back
                        cmd = new NpgsqlCommand("FETCH ALL IN " + "\"" + CursorName + "\"", cn); //use plpgsql fetch command to get data back
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        da.Fill(dtN);
                        //dtRtn.Add(dt); //all the data will save in the List<DataTable> ,no matter the connection is closed or returned multiple refcursors
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        ;
                    }

                    tr.Commit();

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return dt;
                //NpgsqlConnection cn = new NpgsqlConnection(conString);
                //DataSet dt = new DataSet();
                //NpgsqlTransaction transaction = null;

                //try
                //{
                //    if (cn.State == ConnectionState.Closed)
                //    {
                //        cn.Open();
                //    }
                //    transaction = cn.BeginTransaction();
                //    cmd.Connection = cn;
                //    //NpgsqlTransaction transaction = null;                
                //    cmd.Transaction = transaction;
                //    NpgsqlDataReader dr = cmd.ExecuteReader();
                //    while (dr.Read())
                //    {
                //        DataTable dtN = new DataTable();
                //        string CursorName = "crb";
                //        //cmd = new NpgsqlCommand("FETCH ALL IN " + "\"" + dr[0].ToString() + "\"", cn); //use plpgsql fetch command to get data back
                //        cmd = new NpgsqlCommand("FETCH ALL IN " + "\"" + CursorName + "\"", cn); //use plpgsql fetch command to get data back
                //        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                //        da.Fill(dtN);
                //        //dtRtn.Add(dt); //all the data will save in the List<DataTable> ,no matter the connection is closed or returned multiple refcursors
                //    }
                //}
                //catch (Exception)
                //{
                //    return dt;
                //}

                //try
                //{
                //    if (cn.State == ConnectionState.Closed)
                //    {
                //        cn.Open();
                //    }
                //    cmd.CommandTimeout = 100;
                //    cmd.Connection = cn;
                //    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                //    da.Fill(dt);
                //    return dt;

                //}
                //catch (Exception)
                //{
                //    return dt;
                //}
                //finally
                //{
                //  closeConnection(cn, cmd);
                //}
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="qry"></param>
            /// <param name="reader"></param>
            /// <returns></returns>
            public NpgsqlDataReader GetDataByDataReader(string qry, ref NpgsqlDataReader reader)
            {
                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    cmd = new NpgsqlCommand(qry, cn);
                    cn.Open();
                    reader = cmd.ExecuteReader();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return reader;
            }

            public NpgsqlDataReader GetDataByDataReader(NpgsqlCommand cmd)
            {
                NpgsqlDataReader reader = null;

                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                    cn.Open();
                    cmd.Connection = cn;
                    reader = cmd.ExecuteReader();
                }
                catch
                {
                }
                return reader;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="qry"></param>
            /// <param name="ds"></param>
            public DataTable GetDataTable(NpgsqlCommand cmd)
            {
                DataTable dt = new DataTable();
                try
                {
                    using (cn = new NpgsqlConnection(conString))
                    {
                        cmd.Connection = cn;
                        cmd.CommandTimeout = 300;
                        ad = new NpgsqlDataAdapter(cmd);
                        //ad = new SqlDataAdapter(qry, cn);
                        ad.Fill(dt);
                    }

                    return dt;
                }
                catch (Exception)
                {
                    return null;
                }
            }


            //public object GetSingleValueQuery(string qry)//
            //{
            //    try
            //    {
            //        using (cn = new NpgsqlConnection(conString))
            //        {
            //            cn.Open();
            //            cmd.Connection = cn;
            //            cmd.CommandTimeout = 300;

            //            return cmd.ExecuteScalar();
            //        }
            //    }
            //    catch
            //    {
            //        return null;
            //    }
            //}

            public object GetSingleValue(NpgsqlCommand cmd)
            {
                try
                {
                    cn = new NpgsqlConnection(conString);

                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandTimeout = 300;

                    return cmd.ExecuteScalar();
                }
                catch
                {
                    return null;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
            }
            public bool Delete(NpgsqlCommand cmd)
            {
                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    using (cn = new NpgsqlConnection(cs))
                    {
                        cmd.Connection = cn;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return true;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="qry"></param>
            /// <returns></returns>
            public bool InsertData(string qry)
            {
                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    cmd = new NpgsqlCommand(qry, cn);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return true;
            }

            public bool InsertData(NpgsqlCommand cmd)
            {
                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    using (cn = new NpgsqlConnection(cs))
                    {
                        cmd.Connection = cn;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return true;
            }
            public bool Update(NpgsqlCommand cmd)
            {
                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    using (cn = new NpgsqlConnection(cs))
                    {
                        cmd.Connection = cn;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return true;
            }




            public int InsertDataHoliday(NpgsqlCommand cmd)
            {
                int a = 0;
                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    using (cn = new NpgsqlConnection(conString))
                    {
                        cmd.Connection = cn;
                        cn.Open();
                        a = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    return 100;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return a;
            }



            public int InsertDataRoutePath(NpgsqlCommand cmd)
            {
                int a = 0;
                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    using (cn = new NpgsqlConnection(conString))
                    {
                        cmd.Connection = cn;
                        cn.Open();
                        a = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return a;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="qry"></param>
            /// <param name="rowCount"></param>
            /// <returns></returns>
            public int Count(string qry, ref int rowCount)
            {
                try
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    cmd = new NpgsqlCommand(qry, cn);
                    cn.Open();

                    rowCount = (int)cmd.ExecuteScalar();

                    return rowCount;
                }

                catch (Exception)
                {
                    return 0;
                }

                finally
                {
                    closeConnection(cn, cmd);
                }
            }

            #region CloseConnection
            public static void closeConnection(NpgsqlConnection cn, NpgsqlCommand cmd)
            {
                if (cn != null)
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
                cmd.Dispose();
            }
            #endregion

            public DataSet getdatabyQuery(string strQuery)
            {
                NpgsqlConnection cn = new NpgsqlConnection(conString);
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = strQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                DataSet dt = new DataSet();
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }
                    cmd.CommandTimeout = 100;
                    cmd.Connection = cn;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.FillSchema(dt, SchemaType.Mapped);
                    return dt;

                }
                catch (Exception ex)
                {
                    //   return dt;
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    closeConnection(cn, cmd);
                }
                return dt;
            }






        }
    }

