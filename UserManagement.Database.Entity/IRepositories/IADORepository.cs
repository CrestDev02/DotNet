using System.Data;

namespace UserManagement.Database.Entity.IRepositories
{
    public interface IADORepository
    {
        Task<T> ExecuteScalarAsnyc<T>(string procedureName, Array parameters) where T : new();
        Task<int> ExecuteNonQueryAsnyc(string sqlQry);
        Task<int> ExecuteSQLAsnyc(string procedureName, Array parameters);
        Task<DataTable> ExecuteAsnyc(string query, string tableName = "");
        Task<DataSet> ExecuteDataSetAsnyc(string procedureName, Array parameters);
        Task<DataTable> ExecuteDataTableAsnyc(string procedureName, Array parameters);
        Task<T> ExecuteAsnycWithParams<T>(string StoredProcedure, Array Parameters) where T : new();
        Task<IEnumerable<T>> FetchAsnycWithParams<T>(string StoredProcedure, Array Parameters) where T : new();
    }
}
