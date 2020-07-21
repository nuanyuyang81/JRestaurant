using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JRestaurant.DAL
{
    public class SqlHelper
    {
        /// <summary>
        /// Set up SQLServer Connection to Database
        /// </summary>
        /// <returns></returns>
        public static SqlConnection DbConn()
        {
            string connectionstring = "Server=81.70.37.99;Database=JRestaurant;User ID=sa;Password=Esoteric$666;Application Name=JRestaurant;Connection Lifetime=300";
            //string connectionstring = "Server=localhost;Database=Test;User ID=sa;Password=Esoteric$1;Application Name=JRestaurant;Connection Lifetime=300";

            SqlConnection conn = new SqlConnection(connectionstring);
            try
            {
                conn.Open();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return conn;
        }
        /// <summary>
        /// Execute SQL Query without parameters.
        /// </summary>
        /// <param name="cmdline">SQL command line</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteQuery(string cmdline)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = DbConn())
            {
                SqlDataAdapter da = new SqlDataAdapter(cmdline, conn);
                try
                {
                    da.Fill(ds);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return ds;
        }
        /// <summary>
        /// Execute SQL Query with parameters. 
        /// </summary>
        /// <param name="cmdline">SQL command line.</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>Dataset</returns>
        public static DataSet ExecuteQuery(string cmdline, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    da.Fill(ds);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return ds;
        }

        /// <summary>
        /// Execute SQL NonQuery command. Add, Update or Delete operation.
        /// </summary>
        /// <param name="cmdline">SQL command line.</param>
        /// <param name="parameters">SqlParameters.</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string cmdline, SqlParameter[] parameters)
        {
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);
                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Check Data Exist or not.
        /// </summary>
        /// <param name="cmdline"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool CheckExist(string cmdline, SqlParameter[] parameters)
        {
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return bool.Parse(reader[0].ToString());
                    }
                    
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return false;
            }
        }

        public static int ExecuteScalarIdentity(string cmdline, SqlParameter[] parameters)
        {
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);
                try
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        public static int GetCount(string cmdline, SqlParameter[] parameters)
        {
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["RowCount"]);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return 0;
            }
        }
        public static T GetTargetObj<T>(string cmdline, SqlParameter[] parameters)
        {
            T target;
            using (SqlConnection conn = DbConn())
            {
                SqlCommand cmd = new SqlCommand(cmdline, conn);
                cmd.Parameters.AddRange(parameters);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    JArray resultarray = JArray.FromObject(dt, JsonSerializer.CreateDefault(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                    if (resultarray.Count>0)
                    {
                        JObject targetobj = (JObject)resultarray[0];
                        target = JsonConvert.DeserializeObject<T>(targetobj.ToString());
                        return target;
                    }
                }
            }
            return default(T);
        }
    }

}
