using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;
using VideoAssetManager.CommonUtils.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace VideoAssetManager.Business
{
    /// <summary>
    /// DataAccess class provides the data access abstraction layer.
    /// All the classes providing data access functiobality should be derived from DataAccess class.
    /// </summary>
    public partial class DataAccess
    {
        /// <summary>
        /// Holds DB provider object
        /// </summary>
        private readonly DbProviderFactory _mobjDbProvider;

        private const string Message = "Successfully Executed";

        private static readonly int DbCommandTimeOut = 600;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// 
        private readonly IConfiguration configuration;
        public DataAccess()
        {

            var builder = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory) // Set base path to the API project directory
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();

            _mobjDbProvider = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");

        }

        //  public DataAccess() => _mobjDbProvider = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");

        /// <summary>
        /// This method is used to retrieve new provider specific connection.
        /// </summary>
        /// <returns>DbConnection object.</returns>
        private DbConnection NewConnection(bool isReadonly = false)
        {
            var objConnection = _mobjDbProvider.CreateConnection();
            objConnection.ConnectionString = _configuration["ConnectionStrings:DefaultConnection"];
            return objConnection;
        }


        /// <summary>
        /// Creates a new DbParameter object
        /// </summary>
        /// <returns>DbParameter</returns>
        protected DbParameter NewParameter()
        {
            var objParameter = _mobjDbProvider.CreateParameter();
            return objParameter;
        }

        /// <summary>
        /// Added new int DBParameter with overloaded method Newparamter() that will take paratername, datatype, value as parameter and return object and that can be added to objParameters collection
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="parameterDirection"></param>
        /// <returns></returns>
        protected DbParameter NewParameter(string parameterName, object value, DbType dbType = DbType.Int32, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            DbParameter objParameter = _mobjDbProvider.CreateParameter();
            objParameter.ParameterName = parameterName;
            objParameter.Value = value;
            objParameter.DbType = dbType;
            objParameter.Direction = parameterDirection;

            if (dbType == DbType.String || dbType == DbType.AnsiString)
            {
                if (parameterDirection == ParameterDirection.Input)
                    objParameter.Size = value.ToString().Length;

                else
                    objParameter.Size = 250;
            }

            return objParameter;
        }

        /// <summary>
        /// Added new int DBParameter with overloaded method Newparamter() that will take paratername, datatype, value as parameter and return object and that can be added to objParameters collection
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="parameterDirection"></param>
        /// <returns></returns>
        protected DbParameter CreateParameter(string parameterName, object value, DbType dbType = DbType.Int32, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            value = value ?? DBNull.Value;

            DbParameter objParameter = _mobjDbProvider.CreateParameter();
            objParameter.ParameterName = parameterName;
            objParameter.Value = value;
            objParameter.DbType = dbType;
            objParameter.Direction = parameterDirection;

            switch (dbType)
            {
                case DbType.String:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    {
                        if (parameterDirection == ParameterDirection.Input)
                            objParameter.Size = value.ToString().Length;

                        else
                            objParameter.Size = 250;
                        break;
                    }
                case DbType.Int32 when parameterDirection == ParameterDirection.Output:
                    objParameter.Size = int.MaxValue;
                    break;
            }

            return objParameter;
        }


        /// <summary>
        /// This method is used to execute Action based SPs in database.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure.</param>
        /// <param name="storedProcParameters">DbParameter array storing all the parameters of the SP.</param>
        protected void ExecuteAction(string storedProcName, DbParameter[] storedProcParameters)
        {

            #region "Variables Declaration"
            DateTime dtStartTime = DateTime.UtcNow;
            string mstrStatus = null;

            #endregion

            var connection = NewConnection();
            var command = connection.CreateCommand();

            //if (storedProcName == "AN_SP_ANNNCMNT_RD_AVL_CNT_API")
            //    command.CommandTimeout = 1;

            command.CommandTimeout = DbCommandTimeOut;
            command.CommandText = storedProcName;
            command.CommandType = CommandType.StoredProcedure;

            if (storedProcParameters != null)
            {
                var nLoopCounter = storedProcParameters.Length;
                for (int i = 0; i < nLoopCounter; i++)
                {
                    command.Parameters.Add(storedProcParameters[i]);
                }
            }

            try
            {
                connection.Open();
                mstrStatus = CommandAsSql((SqlCommand)command);
                dtStartTime = DateTime.UtcNow;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                connection.Close();
                mstrStatus = $"Exception: {ex.Message} | Query: {mstrStatus}";
            }



            connection.Close();
            command = null;
            connection = null;

        }

        /// <summary>
        /// This method is used to retrieve data from database using a SP.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure to be executed to retrieve data.</param>
        /// <param name="storedProcParameters">DbParameter array storing all the parameters of the SP.</param>
        /// <returns>DataSet holding data as returned by database.</returns>
        protected DataSet ExecuteQuery(string storedProcName, DbParameter[] storedProcParameters)
        {
            #region "Variables Declaration"
            DateTime dtStartTime = DateTime.UtcNow;
            string mstrStatus = null;

            #endregion

            var connection = NewConnection();
            var command = connection.CreateCommand();
            command.CommandTimeout = DbCommandTimeOut;
            command.CommandText = storedProcName;
            command.CommandType = CommandType.StoredProcedure;

            if (storedProcParameters != null)
            {
                var nLoopCounter = storedProcParameters.Length;

                for (int i = 0; i < nLoopCounter; i++)
                {
                    command.Parameters.Add(storedProcParameters[i]);
                }
            }

            var dataAdapter = _mobjDbProvider.CreateDataAdapter();
            dataAdapter.SelectCommand = command;
            var dataSet = new DataSet();

            try
            {
                mstrStatus = CommandAsSql((SqlCommand)command);
                dtStartTime = DateTime.UtcNow;
                dataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                connection.Close();
                mstrStatus = $"Exception: {ex.Message} | Query: {mstrStatus}";
            }



            connection.Close();
            dataAdapter = null;
            connection = null;
            command = null;

            return dataSet;
        }

        /// <summary>
        /// This method id used to retrieve single value from the database using a SP.
        /// This method is parallel to ExecuteScalar method of Command.
        /// </summary>
        /// <param name="storedProcName">Name of the stored procedure to be executed to retrieve data.</param>
        /// <param name="storedProcParameters">DbParameter array storing all the parameters of the SP.</param>
        /// <returns>Object holding a single value - as returned by database.</returns>
        protected object ExecuteSingleValueQuery(string storedProcName, DbParameter[] storedProcParameters)
        {

            #region "Variables Declaration"
            DateTime dtStartTime = DateTime.UtcNow;
            string mstrStatus = null;
            object result = null;
            #endregion

            var connection = NewConnection();
            var command = connection.CreateCommand();
            command.CommandTimeout = DbCommandTimeOut;
            command.CommandText = storedProcName;
            command.CommandType = CommandType.StoredProcedure;

            if (storedProcParameters != null)
            {
                var nLoopCounter = storedProcParameters.Length;

                for (int i = 0; i < nLoopCounter; i++)
                {
                    command.Parameters.Add(storedProcParameters[i]);
                }
            }

            try
            {
                connection.Open();
                mstrStatus = CommandAsSql((SqlCommand)command);
                dtStartTime = DateTime.UtcNow;
                result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                connection.Close();
                mstrStatus = $"Exception: {ex.Message} | Query: {mstrStatus}";
            }

            connection.Close();
            connection = null;
            command = null;

            return result;
        }

        /// <summary>
        /// This method is used to check null for given parameter.
        /// </summary>
        /// <param name="sp">Sql parameter</param>
        /// <returns>Returns string "NULL", in case of null in parameter</returns>
        public string ParameterValueForSQL(SqlParameter sp)
        {
            string retval = "";
            try
            {
                switch (sp.SqlDbType)
                {

                    case SqlDbType.Char:
                    case SqlDbType.NChar:
                    case SqlDbType.NText:
                    case SqlDbType.NVarChar:
                    case SqlDbType.Text:
                    case SqlDbType.Time:
                    case SqlDbType.VarChar:
                    case SqlDbType.Xml:
                    case SqlDbType.Date:
                    case SqlDbType.DateTime:
                    case SqlDbType.DateTime2:
                    case SqlDbType.DateTimeOffset:
                        retval = (sp.Value == null ? "NULL" : ("'" + sp.Value.ToString().Replace("'", "''") + "'"));
                        break;

                    case SqlDbType.Bit:
                        retval = (sp == null ? "NULL" : Convert.ToBoolean(sp.Value) ? "1" : "0");
                        break;

                    case SqlDbType.Int:
                        retval = (sp == null ? "NULL" : sp.Value.ToString());
                        break;

                    case SqlDbType.SmallInt:
                        retval = (sp == null ? "NULL" : sp.Value.ToString());
                        break;

                    case SqlDbType.TinyInt:
                        retval = (sp == null ? "NULL" : sp.Value.ToString());
                        break;

                    case SqlDbType.Binary:
                        retval = (sp == null ? "NULL" : sp.Value.ToString());
                        break;

                    default:
                        retval = (sp == null ? "NULL" : sp.Value.ToString().Replace("'", "''"));
                        break;
                }

                return retval;
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }
        }

        /// <summary>
        /// This method is used to returns a dynamic sql query from given SQL command
        /// </summary>
        /// <param name="sc">Sql Command</param>
        /// <returns> returns a dynamic sql query</returns>
        public string CommandAsSql(DbCommand sc)
        {
            #region "Variables Declarations"
            StringBuilder sql = new StringBuilder();
            bool FirstParam = true;
            #endregion

            try
            {
                sql.AppendLine($"use {sc.Connection.Database};");
                switch (sc.CommandType)
                {
                    case CommandType.StoredProcedure:
                        sql.AppendLine("declare @return_value int;");

                        foreach (SqlParameter sp in sc.Parameters)
                        {
                            if ((sp.Direction == ParameterDirection.InputOutput) || (sp.Direction == ParameterDirection.Output))
                            {
                                sql.Append($"declare {sp.ParameterName}\t{sp.SqlDbType}\t= ");

                                sql.AppendLine(
                                    $"{((sp.Direction == ParameterDirection.Output) ? "null" : ParameterValueForSQL(sp))};");

                            }
                        }

                        sql.AppendLine($"exec [{sc.CommandText}]");

                        foreach (SqlParameter sp in sc.Parameters)
                        {
                            if (sp.Direction != ParameterDirection.ReturnValue)
                            {
                                sql.Append((FirstParam) ? "\t" : "\t, ");

                                if (FirstParam) FirstParam = false;

                                if (sp.Direction == ParameterDirection.Input)
                                    sql.AppendLine($"{sp.ParameterName} = {ParameterValueForSQL(sp)}");
                                else

                                    sql.AppendLine($"{sp.ParameterName} = {sp.ParameterName} output");
                            }
                        }
                        sql.AppendLine(";");

                        sql.AppendLine("select 'Return Value' = convert(varchar, @return_value);");

                        foreach (SqlParameter sp in sc.Parameters)
                        {
                            if ((sp.Direction == ParameterDirection.InputOutput) || (sp.Direction == ParameterDirection.Output))
                            {
                                sql.AppendLine($"select '{sp.ParameterName}' = convert(varchar, {sp.ParameterName});");
                            }
                        }
                        break;
                    case CommandType.Text:
                        sql.AppendLine(sc.CommandText);
                        break;
                }

                return sql.ToString();
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }
        }

        //New Code
        public async Task<object> ExecuteActionAsync(string storedProcName, DbParameter[] storedProcParameters)
        {
            #region "Variables Declaration"
            DateTime dtStartTime = DateTime.UtcNow;
            string mstrStatus = null;
            object result = null;
            #endregion

            var connection = NewConnection();
            var command = connection.CreateCommand();
            command.CommandTimeout = DbCommandTimeOut;
            command.CommandText = storedProcName;
            command.CommandType = CommandType.StoredProcedure;

            if (storedProcParameters != null)
            {
                foreach (var parameter in storedProcParameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            try
            {
                await connection.OpenAsync();  // Open connection asynchronously
                mstrStatus = CommandAsSql((SqlCommand)command);
                dtStartTime = DateTime.UtcNow;

                result = await command.ExecuteScalarAsync();  // Execute the query asynchronously
            }
            catch (Exception ex)
            {
                connection.Close();
                mstrStatus = $"Exception: {ex.Message} | Query: {mstrStatus}";
            }

            connection.Close();
            connection = null;
            command = null;

            return result;
        }


    }
}