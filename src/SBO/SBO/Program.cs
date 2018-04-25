using SBO.Domain;
using SBO.Domain.Entities;
using SmartBulkOperations;
using System.Collections.Generic;
using System.Linq;

namespace SBO
{
    class Program
    {
        static void Main(string[] args)
        {
            // Just the fill the list
            List<Person> personList = Enumerable.Range(1, 120).ToList().Select(i => new Person { Id = i, Name = i.ToString() }).ToList();
            List<Contact> contactList = Enumerable.Range(1, 200).ToList().Select(i => new Contact { Id = i, Content = i.ToString() }).ToList();

            int pageSize = 5;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            /* With default BulkInsert implementation in not paged mode */

            BulkInsert defaultBulkInsert = new BulkInsert();
            var defaultBulkInsertSqlCmdPerson = defaultBulkInsert.BulkInsertCmd(personList);
            
            /* With your own implementation of BulkInsert in paged mode */

            PersonBulk personBulk = new PersonBulk();

            PagedBulk<Person> personPagedBulk = new PagedBulk<Person>(personList, personBulk);
            personPagedBulk.PageSize = pageSize;

            List<string> sqlCmdGeneratedPerson = new List<string>();

            while (personPagedBulk.HasNext())
            {
                string sqlCmdPersonP = personPagedBulk.Next();
                sqlCmdGeneratedPerson.Add(sqlCmdPersonP);
            }

            /* With your own implementation of BulkInsert in not paged mode */

            string sqlCmdPerson = personBulk.BulkInsertCmd(personList);

            ////////////////////////////////////////////////////////////////////////

            /* With default BulkInsert implementation in not paged mode */

            var defaultBulkInsertSqlCmdContact = defaultBulkInsert.BulkInsertCmd(contactList);

            /* With your own implementation of BulkInsert in paged mode */

            ContactBulk contactBulk = new ContactBulk();

            PagedBulk<Contact> contactPagedBulk = new PagedBulk<Contact>(contactList, contactBulk);
            contactPagedBulk.PageSize = pageSize;

            List<string> sqlCmdGeneratedContact = new List<string>();

            while (contactPagedBulk.HasNext())
            {
                string sqlCmdContactP = contactPagedBulk.Next();
                sqlCmdGeneratedContact.Add(sqlCmdContactP);
            }

            /* With your own implementation of BulkInsert in not paged mode */

            string sqlCmdContact = contactBulk.BulkInsertCmd(contactList);

            ////////////////////////////////////////////////////////////////////////

            /* With default BulkDelete implementation */

            BulkDelete defaultBulkDelete = new BulkDelete();
            var defaultBulkDeleteSqlCmdPerson = defaultBulkDelete.BulkDeleteCmd(personList, e => e.Id);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}
