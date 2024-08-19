using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MLMPortal.Models
{
    public class dbHelper
    {
        // private static string consString = "Data Source=DESKTOP-JADR545;Initial Catalog=MLMPortal1db;Integrated Security=True";
         private static string consString = "Data Source=103.131.196.139;Initial Catalog=Sangam2mlmdb;User Id=NewMLMUser;Password=mlmuser@2023##$";

        //private static string consString = "Data Source=DESKTOP-JADR545\\SQLEXPRESS;Initial Catalog=aunmoneyworldmlmdb;Integrated Security=True;";
        //private static string consString = "Data Source=LAPTOP-ETEAB50Q;Initial Catalog=aunmoneyworldmlmdb;Integrated Security=True;";

       

        SqlConnection con = new SqlConnection(consString);
        public int ExecuteNonQueryProc(string cmdText, SqlParameter[] prms)
        {
            int r = 0;
            try
            {
                //string str1 = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();
                using (SqlConnection conn = new SqlConnection(consString))
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        if (prms != null)
                        {
                            foreach (SqlParameter p in prms)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        conn.Open();
                        try
                        {
                            r = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            r = 0;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return r;
        }
        public DataTable FetchData1Proc(string qry, SqlParameter[] Para)
        {
            SqlConnection dbconn = new SqlConnection(consString);

            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(qry, dbconn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < Para.Length; i++)
                {
                    cmd.Parameters.Add(Para[i]);
                }
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dt);
            }
            catch (Exception ex)
            {
                string msg = "Some Fetching Error Occur";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                dbconn.Close();
                dbconn.Dispose();
            }
            return dt;
        }
        public DataTable ExecProcDataTable(string ProName, SqlParameter[] Param)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(ProName, con);
                cmd.CommandTimeout = 200000;
                cmd.CommandType = CommandType.StoredProcedure;
                if (Param!=null)
                {
                    foreach (SqlParameter prm in Param)
                    {
                        cmd.Parameters.Add(prm);
                    }
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                
                adp.Fill(dt);
            }
           catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataSet ExecProcDataSet(string ProName, SqlParameter[] Param)
        {
            DataSet dt = new DataSet();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(ProName, con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter prm in Param)
                {
                    cmd.Parameters.Add(prm);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            catch (Exception ex)
            { 


            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        public DataTable ExecAdaptorDataTable(string Query)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }


        public int ExecuteNonQuery(string Query)
        {
            int r = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(consString))
                {
                    using (SqlCommand cmd = new SqlCommand(Query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        try
                        {
                            r = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            r = 0;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return r;
        }
        public DataTable ExecProcDataTable(string ProName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(ProName, con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}