using Dapper;
using LibraryModels.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerService.reposetorys
{
   public interface  IBookReposetory
    {
        Task<List<Book>> Get();
        Task<DateTime> GetLastCreateDate();
    }
    public class BookReposetory : IBookReposetory
    {
        private string _connectionString;
        public BookReposetory(IStartupConfig startupConfig)
        {
            _connectionString = startupConfig.ConnectionString;
        }
        public async Task<List<Book>> Get()
        {
            using var connection = new SqlConnection(_connectionString);
            var data = await connection.QueryAsync<Book, Publisher, Author, Book>("Stp_Library_GetBooksWithAuthors", (book, publisher, author) =>
            {
                book.Authors.Add(author);
                book.Publisher = publisher;
                return book;
            });

            var books = data.GroupBy(book => book.Id).Select(group =>
            {
                var book = group.First();
                book.Authors = group.Select(book => book.Authors.Single()).ToList();
                return book;
            });

            return books.ToList();
        }

        public async Task<DateTime> GetLastCreateDate()
        {
            using var connection = new SqlConnection(_connectionString);
            var data = await connection.QueryFirstAsync<DateTime>("Stp_Library_LastBookCreateDate");
            return data;
        }
    }
}
