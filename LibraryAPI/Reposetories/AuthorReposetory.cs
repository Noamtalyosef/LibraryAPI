using Dapper;
using LibraryAPI.Interfaces;
using LibraryModels.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryAPI.Reposetories
{
    
    public class AuthorReposetory : IAuthorReposetory
    {
        private readonly string _libraryConnectionString;
        public AuthorReposetory(IStartupConfig startupConfig)
        {
            _libraryConnectionString = startupConfig.ConnectionString;
        }
        public async Task CreateAsync(Author author)
        {
            using var connection =  new SqlConnection(_libraryConnectionString);

            var newBooks = new DataTable();
            newBooks.Columns.Add("Name", typeof(string));
            newBooks.Columns.Add("YearOfPublish", typeof(int));
            newBooks.Columns.Add("PicturePath", typeof(string));
            newBooks.Columns.Add("CopyPath", typeof(string));
            newBooks.Columns.Add("PublisherId",typeof(int));    

            foreach (var book in author.Books)
            {
                newBooks.Rows.Add(book.Name,book.YearOfPublish,book.PicturePath,book.CopyPath, book.Publisher.Id);
            }

            await connection.ExecuteAsync("Stp_Library_CreateAuthor", new
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                CityId = author.City.Id,
                YearOfBirth = author.YearOfBirth,
                NewBooks = newBooks
            });
        }

        public async Task<bool> DeleteAsync(int authorId)
        {
            using var connection = new SqlConnection(_libraryConnectionString);
            var numOfChanges = await connection.ExecuteAsync("Stp_Library_DeleteAuthor", new { AuthorId = authorId });
            return numOfChanges > 0;
        }

        public async Task<List<Author>> GetAllAsync()
        {
           using var connection = new SqlConnection(_libraryConnectionString);

            var data = await connection.QueryAsync<Author,City ,Book,Publisher ,Author>("Stp_Library_GetAuthorsWithBooks", (author,City,book,publisher) =>
            {
                author.City = City;
                if(book!=null)
                {
                    book.Publisher = publisher;
                    author.Books.Add(book);
                }
                 
               
                
                return author;
            },splitOn:"Id");

            var authors = data.GroupBy(author => author.Id).Select(group =>
            {
                var author = group.First();
                if(author.Books.Count>0)
                {
                    author.Books = group.Select(author => author.Books.Single()).ToList();
                }
                

                return author;
            });

            return authors.ToList();
        }

        public async Task<List<Author>> GetAuthorsById(List<int> AuthorsIds)
        {
            using var connection = new SqlConnection(_libraryConnectionString);

            var idsDataTable = new DataTable();
            idsDataTable.Columns.Add("id", typeof(int));

            foreach (int id in AuthorsIds)
            {
                idsDataTable.Rows.Add(id);
            }

            var data = await connection.QueryAsync<Author, City, Book, Publisher, Author>("Stp_Library_GetAuthorsById", (author, City, book, publisher) =>
            {
                author.City = City;
                if (book != null)
                {
                    book.Publisher = publisher;
                    author.Books.Add(book);
                }

                return author;
            }, new {AuthorsIds = idsDataTable});

            var authors = data.GroupBy(author => author.Id).Select(group =>
            {
                var author = group.First();
                if (author.Books.Count > 0)
                {
                    author.Books = group.Select(author => author.Books.Single()).ToList();
                }

                return author;
            });

            return authors.ToList();
        }

        public async Task<List<City>> GetAllCitysAsync()
        {
            using var connection = new SqlConnection(_libraryConnectionString);
            var citys = await connection.QueryAsync<City>("Stp_Library_GetCitys");
            return citys.ToList();
        }

        public Task<List<Author>> SearchBooksAsync(string searchInput)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Author author)
        {
           using var connction = new SqlConnection(_libraryConnectionString);
            await connction.ExecuteAsync("Stp_Library_UpdateAuthor", new
            {
                Id = author.Id, 
                FirstName = author.FirstName,
                LastName = author.LastName,
                CityId = author.City.Id,
                YearOfBirth = author.YearOfBirth
            });
        }
    }
}
