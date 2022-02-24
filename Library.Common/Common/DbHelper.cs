using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Library.Core.Common
{
    public static class DbHelper
    {
        /// <summary>
        /// _context.RawSqlQuery<CommentViewModel>("")
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<T> RawSqlQuery<T>(this DbContext context, string query)
        {
            try
            {
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                context.Database.OpenConnection();

                using DbDataReader reader = command.ExecuteReader();
                return reader.MapToList<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        /// <summary>
        /// var data = _context.LoadStoredProc("StoredProcedureName").WithSqlParam("firstparamname", firstParamValue).WithSqlParam("secondparamname", secondParamValue).ExecureStoredProc<MyType>()
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <returns></returns>
        public static DbCommand LoadStoredProc(this DbContext context, string storedProcName)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException("Call LoadStoredProc before using this method");
            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            cmd.Parameters.Add(param);
            return cmd;
        }

        public static async Task<List<T>> ExecuteStoredProcAsync<T>(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using DbDataReader reader = await command.ExecuteReaderAsync();
                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    return reader.MapToList<T>();
                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }


        public static async Task<DataSet> GetDbToDataSet(this DbCommand command)
        {
            IDataReader reader = null;
            DataSet ds = null;
            try
            {
                ds = await ExecuteStoredProcDataReaderAsync(command);
            }
            catch (Exception ex) { }
            finally
            {
                reader.CloseReader();
            }

            return ds;
        }

        public static async Task<DataSet> ExecuteStoredProcDataReaderAsync(this DbCommand command)
        {
            //IDataReader ireader = null;
            DataSet ds = new DataSet();
            using (command)
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                    do
                    {
                        var fieldCount = reader.FieldCount;
                        var table = new DataTable();
                        for (var i = 0; i < fieldCount; i++)
                        {
                            table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }
                        table.BeginLoadData();
                        var values = new object[fieldCount];
                        while (reader.Read())
                        {
                            reader.GetValues(values);
                            table.LoadDataRow(values, true);
                        }
                        table.EndLoadData();
                        ds.Tables.Add(table);
                    } while (reader.NextResult());
                    reader.Close();
                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    command.Connection.Close();
                }
                return ds;
            }
        }

        public static async Task<DataTable> ExecuteStoredProcDataTableAsync(this DbCommand command)
        {
            DataTable dt = new DataTable();

            using (command)
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using DbDataReader reader = await command.ExecuteReaderAsync();
                    dt.Load(reader);
                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    command.Connection.Close();
                }
                return dt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task ExecuteNonQueryAsync(this DbContext context, string query)
        {
            try
            {
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Database.CloseConnection();
            }


        }

        private static List<T> MapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var dtc = dr.GetColumnSchema();



            var colMapping = dr.GetColumnSchema()
              .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
              .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val =
                          dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }

    }
}
