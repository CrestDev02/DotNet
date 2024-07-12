using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using UserManagement.Database.Entity.DataAccess.DTOs;
using UserManagement.Database.Entity.IRepositories;

namespace UserManagement.Database.Entity.Common
{
    public static class Extensions
    {
        public static T RowToObject<T>(this DataRow row) where T : new()
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();

            T result = CreateItemFromRow<T>(row, properties);

            return result;
        }

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(DayOfWeek))
                {
                    DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString()!);
                    property.SetValue(item, day, null);
                }
                else
                {
                    if (row.Table.Columns.Contains(property.Name))
                    {
                        if (row[property.Name] == DBNull.Value)
                            property.SetValue(item, null, null);
                        else
                            ConvertType(property, item, row, property.Name);
                    }
                }
            }
            return item;
        }

        private static void ConvertType<T>(PropertyInfo info, T item, DataRow row, string MemberName)
        {
            var value1 = row[MemberName];

            Type? Generic = (typeof(T).GetProperties().Where(x => x.Name.ToLower() == MemberName.ToLower())
                ?.FirstOrDefault()?.PropertyType?.GetGenericArguments()?.FirstOrDefault())
                ?? (typeof(T).GetProperties().Where(x => x.Name.ToLower() == MemberName.ToLower())
                ?.FirstOrDefault()?.PropertyType);

            Type type = value1.GetType();

            if (type == Generic)
                info.SetValue(item, value1, null);
            else
                info.SetValue(item, Convert.ChangeType(value1, Generic!), null);
        }

        public static async Task Add(this Exception ex, IExceptionRepository applicationExceptionRepository)
        {
            ApplicationExceptionModel exception = new()
            {
                Message = ex.Message,
                CreatedDate = DateTime.Now,
                StackTrace = ex.StackTrace,
                Method = ex.TargetSite?.Name,
                Source = ex.Source
            };
            await applicationExceptionRepository.AddAsync(exception);
        }

        public static ApplicationExceptionModel ConvertEx(this Exception ex)
        {
            return new ApplicationExceptionModel()
            {
                Message = ex.Message,
                CreatedDate = DateTime.Now,
                StackTrace = ex.StackTrace,
                Method = ex.TargetSite?.Name,
                Source = ex.Source
            };
        }
    }
}
