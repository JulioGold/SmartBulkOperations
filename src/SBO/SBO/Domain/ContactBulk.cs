using SBO.Domain.Entities;
using SmartBulkOperations;
using System.Collections.Generic;
using System.Text;

namespace SBO.Domain
{
    public sealed class ContactBulk : IBulkInsert
    {
        public string BulkInsertCmd<T>(List<T> items)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"INSERT INTO {nameof(Contact)}({BulkHelper.CommaSeparatedPropertiesNameFromType(typeof(Contact))})");

            items.ForEach(item =>
            {
                stringBuilder.AppendLine($"SELECT {BulkHelper.CommaSeparatedPropertiesValueFromObject(item)}{BulkHelper.CmdLineDelimiter(items, item)}");
            });

            return stringBuilder.ToString();
        }
    }
}
