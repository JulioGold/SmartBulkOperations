using System.Collections.Generic;
using System.Text;

namespace SmartBulkOperations
{
    public sealed class BulkInsert : IBulkInsert
    {
        public string BulkInsertCmd<T>(List<T> items)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"INSERT INTO {typeof(T).Name}({BulkHelper.CommaSeparatedPropertiesNameFromType(typeof(T))})");

            items.ForEach(item =>
            {
                stringBuilder.AppendLine($"SELECT {BulkHelper.CommaSeparatedPropertiesValueFromObject(item)}{BulkHelper.CmdLineDelimiter(items, item)}");
            });

            return stringBuilder.ToString();
        }
    }
}
