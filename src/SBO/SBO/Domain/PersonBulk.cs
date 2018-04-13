using SBO.Domain.Entities;
using SmartBulkOperations;
using System.Collections.Generic;
using System.Text;

namespace SBO.Domain
{
    public sealed class PersonBulk : IBulkInsert
    {
        public string BulkInsertCmd<T>(List<T> items)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"INSERT INTO {nameof(Person)}({BulkHelper.CommaSeparatedPropertiesNameFromType(typeof(Person))})");

            items.ForEach(item =>
            {
                stringBuilder.AppendLine($"SELECT {BulkHelper.CommaSeparatedPropertiesNamedValueFromObject(item)}{BulkHelper.CmdLineDelimiter(items, item)}");
            });

            return stringBuilder.ToString();
        }
    }
}
