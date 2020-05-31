using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace DAL
{
    public class SqlHelper
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["dbConn"].ToString();

        #region 无参sql

        public static int ExecuteNonQuery(string sql)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExecuteTransaction(string[] sql)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    Transaction = tran
                };
                try
                {
                    foreach (string item in sql)
                    {
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object ExecuteScalar(string sql)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static MySqlDataReader ExecuteReader(string sql)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return null;
            }
        }

        #endregion

        #region 带参sql

        public static int ExecuteNonQuery(string sql, MySqlParameter[] param)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddRange(param);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExecuteTransaction(string sql, MySqlParameter[][] param)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = conn,
                    Transaction = tran
                };
                try
                {
                    foreach (MySqlParameter[] item in param)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(item);
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExecuteTransaction(string[] sql, MySqlParameter[] param)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    Transaction = tran
                };
                cmd.Parameters.AddRange(param);
                try
                {
                    foreach (string item in sql)
                    {
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object ExecuteScalar(string sql, MySqlParameter[] param)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddRange(param);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static MySqlDataReader ExecuteReader(string sql, MySqlParameter[] param)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddRange(param);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return null;
            }
        }

        #endregion

        #region 带参存储过程

        public static int ExecuteNonQuery(string sql, MySqlParameter[] param, bool IsProcedure)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddRange(param);
            if (IsProcedure)
                cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExecuteTransaction(string sql, MySqlParameter[][] param, bool IsProcedure)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand
                {
                    CommandText = sql,
                    Connection = conn,
                    Transaction = tran
                };
                if (IsProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    foreach (MySqlParameter[] item in param)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(item);
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExecuteTransaction(string[] sql, MySqlParameter[] param, bool IsProcedure)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    Transaction = tran
                };
                cmd.Parameters.AddRange(param);
                if (IsProcedure)
                    cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    foreach (string item in sql)
                    {
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static object ExecuteScalar(string sql, MySqlParameter[] param, bool IsProcedure)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddRange(param);
            if (IsProcedure)
                cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static MySqlDataReader ExecuteReader(string sql, MySqlParameter[] param, bool IsProcedure)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddRange(param);
            if (IsProcedure)
                cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
