using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using UserManagement.Database.Entity.Common;
using UserManagement.Database.Entity.DataAccess.DTOs;
using UserManagement.Database.Entity.IRepositories;

namespace UserManagement.Database.Entity.Repositories
{
    public class ADORepository : IADORepository
    {
        private readonly AppSettings _appConfiguration;
        private readonly IExceptionRepository _applicationExceptionRepository;

        public ADORepository(IExceptionRepository applicationExceptionRepository, IOptions<AppSettings> appConfiguration)
        {
            _applicationExceptionRepository = applicationExceptionRepository;
            _appConfiguration = appConfiguration.Value;
        }

        private string? ConnectionString => _appConfiguration?.ConnectionString;

        public async Task<DataTable> ExecuteAsnyc(string sqlQry, string tableName = "")
        {
            DataTable table = new()
            {
                TableName = tableName
            };

            using var connection = new SqlConnection(ConnectionString);

            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = sqlQry;
                command.CommandType = CommandType.StoredProcedure;

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                using IDataReader reader = await command.ExecuteReaderAsync();

                table.Load(reader);
            }
            catch (Exception ex)
            {
                await ex.Add(_applicationExceptionRepository);
            }

            return table;
        }

        public async Task<T> ExecuteAsnycWithParams<T>(string StoredProcedure, Array Parameters) where T : new()
        {
            DataTable table = new();

            T Item = new();

            using var connection = new SqlConnection(ConnectionString);

            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = StoredProcedure;
                command.CommandType = CommandType.StoredProcedure;

                if (Parameters != null)
                    command.Parameters.AddRange(Parameters);

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                using IDataReader reader = await command.ExecuteReaderAsync();

                table.Load(reader);

                if (table.Rows.Count > 0)
                    Item = table.Rows[0].RowToObject<T>();
            }
            catch (Exception ex)
            {
                await ex.Add(_applicationExceptionRepository);
            }

            return Item;
        }

        public async Task<DataSet> ExecuteDataSetAsnyc(string procedureName, Array parameters)
        {
            DataSet dataSet = new();

            using var connection = new SqlConnection(ConnectionString);

            DbProviderFactory? factory = DbProviderFactories.GetFactory(connection);

            DbDataAdapter? dataAdapter = factory?.CreateDataAdapter();

            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                dataAdapter!.SelectCommand = command;

                dataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                await ex.Add(_applicationExceptionRepository);
            }
            finally
            {
                dataAdapter?.Dispose();
            }
            return dataSet;
        }

        public async Task<DataTable> ExecuteDataTableAsnyc(string procedureName, Array parameters)
        {
            DataTable dataTable = new();

            using var connection = new SqlConnection(ConnectionString);

            DbProviderFactory? factory = DbProviderFactories.GetFactory(connection);

            DbDataAdapter? dataAdapter = factory?.CreateDataAdapter();

            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                dataAdapter!.SelectCommand = command;

                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                await ex.Add(_applicationExceptionRepository);
            }
            finally
            {
                dataAdapter?.Dispose();
            }
            return dataTable;
        }

        public async Task<int> ExecuteNonQueryAsnyc(string sqlQry)
        {
            using var connection = new SqlConnection(ConnectionString);

            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = sqlQry;

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                await ex.Add(_applicationExceptionRepository);
            }
            return 0;
        }

        public async Task<T> ExecuteScalarAsnyc<T>(string sqlQry, Array parameters) where T : new()
        {
            T item = new();

            using var connection = new SqlConnection(ConnectionString);

            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = sqlQry;

                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                if (result == null)
                    return item;

                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                await ex.Add(_applicationExceptionRepository);
            }
            return item;
        }

        public async Task<int> ExecuteSQLAsnyc(string procedureName, Array parameters)
        {
            using var connection = new SqlConnection(ConnectionString);

            try
            {
                using var command = connection.CreateCommand();

                command.CommandText = procedureName;

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(parameters);

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                await ex.Add(_applicationExceptionRepository);
            }
            return 0;
        }

        public async Task<IEnumerable<T>> FetchAsnycWithParams<T>(string StoredProcedure, Array Parameters) where T : new()
        {
            DataTable table = new();

            ICollection<T> Items = new List<T>();

            using var connection = new SqlConnection(ConnectionString);

            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = StoredProcedure;
                command.CommandType = CommandType.StoredProcedure;

                if (Parameters != null)
                    command.Parameters.AddRange(Parameters);

                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                using IDataReader reader = await command.ExecuteReaderAsync();

                table.Load(reader);

                if (table.Rows.Count > 0)
                    Items = table.ToList<T>();

            }
            catch (Exception ex)
            {
                await ex.Add(_applicationExceptionRepository);
            }

            return Items.AsEnumerable();
        }
    }
}
