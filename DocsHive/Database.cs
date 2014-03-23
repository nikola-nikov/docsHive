using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsHive.Service
{
    public class Database
    {
        #region Declarations

        private string activeDatabaseName;

        #endregion

        #region Properties

        /// <summary>
        /// Име на активната база от данни
        /// </summary>
        public string ActiveDatabaseName
        {
            get
            {
                return this.activeDatabaseName;
            }
        }

        /// <summary>
        /// Нов Connection обект
        /// </summary>
        public SqlConnection GetConnection
        {
            get
            {
                return new SqlConnection(Properties.Settings.Default.ConnectionString);
            }
        }

        #endregion

        #region Methods


        #endregion

        /// <summary>
        /// Опитва да осъществи връзка със сървъра
        /// </summary>
        /// <returns>True при успех</returns>
        public bool TestConnection()
        {
            bool result = false;

            try
            {
                using (var connection = GetConnection)
                {
                    connection.Open();
                    result = true;
                    this.activeDatabaseName = string.Format("({0}) {1}", connection.DataSource, connection.Database);
                }

            }
            catch (SqlException sqlEx)
            {
                this.activeDatabaseName = "Unable to connect to database.";
            }

            return result;
        }

        /// <summary>
        /// Изпънява заявка без резултат
        /// </summary>
        /// <param name="query">SQL заяка</param>
        public void ExecuteNonQuery(string query)
        {
            using (var connection = GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Изпълнява заявка с 1 резултат
        /// </summary>
        /// <param name="query">SQL заяка</param>
        /// <returns>Резултат</returns>
        public object ExecuteScalar(string query)
        {
            object result = null;

            using (var connection = GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                result = command.ExecuteScalar();
            }

            return result;
        }

        /// <summary>
        /// Изпънява заявка и връща множество резултати
        /// </summary>
        /// <param name="query">SQL заяка</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(string query)
        {
            DataSet result = null;

            using (var connection = GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(result);
            }

            return result;
        }

        /// <summary>
        /// Изпълнява stored procedure
        /// </summary>
        /// <param name="spName">Иаме на процедурата</param>
        public void ExecuteStoredProcedure(string spName)
        {
            using (var connection = GetConnection)
            {
                //string query = "EXEC " + spName;
                SqlCommand command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }



    }
}
