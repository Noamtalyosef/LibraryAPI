using Dapper;
using LibraryAPI.Interfaces;
using LibraryModels.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryAPI.Reposetories
{
   
    public class BookReposetory : IBookReposetory
    {
        private readonly string _libraryConnectionString;
        private IBookFilesHelper _bookFilesHelper;
        public BookReposetory(IStartupConfig startupConfig , IBookFilesHelper bookFilesHelper)
        {
            _libraryConnectionString = startupConfig.ConnectionString;
            _bookFilesHelper = bookFilesHelper; 
        }

        public async Task CreateAsync(NewBook bookDto)
        {
            using var connection = new SqlConnection(_libraryConnectionString);
            var newAuthors = new DataTable();
            var existingAuthorsIds = new DataTable();

            newAuthors.Columns.Add("FirstName", typeof(string));
            newAuthors.Columns.Add("LastName", typeof(string));
            newAuthors.Columns.Add("CityId", typeof(string));
            newAuthors.Columns.Add("YearOfBirth", typeof(int));

            foreach (var author in bookDto.NewAuthors)
            {
               newAuthors.Rows.Add(author.FirstName, author.LastName, author.City.Id, author.YearOfBirth);
            }

            existingAuthorsIds.Columns.Add("Id", typeof(int));

            foreach (var id in bookDto.ExistingAuthorsIds)
            {
                existingAuthorsIds.Rows.Add(id);
            }

            await connection.ExecuteAsync("Stp_Library_CreateBook", new 
            {
                Name = bookDto.Book.Name,
                YearOfPublish = bookDto.Book.YearOfPublish,
                PicturePath = bookDto.Book.PicturePath,
                CopyPath = bookDto.Book.CopyPath,
                PublisherId = bookDto.Book.Publisher.Id,
                ExistingAuthorsIds = existingAuthorsIds,
                NewAuthors = newAuthors
            });
        }

        public async Task<bool> DeleteAsync(Book Book)
        {
            _bookFilesHelper.DeleteCopy(Book.CopyPath);
            _bookFilesHelper.DeletePhoto(Book.PicturePath);

            using var connection = new SqlConnection(_libraryConnectionString);
            var numOfChanges =  await connection.ExecuteAsync("Stp_Library_DeleteBook", new {BookId = Book.Id });
            return numOfChanges > 0;    
        }
      

        public async Task<List<Book>> GetAllAsync()
        {
            using var connection = new SqlConnection(_libraryConnectionString);
            var data = await connection.QueryAsync<Book, Publisher,Author, Book>("Stp_Library_GetBooksWithAuthors",(book,publisher,author) =>
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

        public async Task<List<Publisher>> GetAllPublishersAsync()
        {
            using var connection = new SqlConnection(_libraryConnectionString);
            var publishes = await connection.QueryAsync<Publisher>("Stp_Library_GetPublishers");
            return publishes.ToList();  
        }

        public async Task<List<Book>> GetBooksById(List<int> booksIds)
        {
            var idsDataTable  = new DataTable();    
            idsDataTable.Columns.Add("id", typeof(int));
            
            foreach(int id in booksIds)
            {
                idsDataTable.Rows.Add(id);
            }

            using var connection = new SqlConnection(_libraryConnectionString);
            var data = await connection.QueryAsync<Book, Publisher, Author, Book>("Stp_Library_GetBooksById",(book, publisher, author) =>
            {
                book.Authors.Add(author);
                book.Publisher = publisher;
                return book;
            }, new { BooksIds = idsDataTable });

            var books = data.GroupBy(book => book.Id).Select(group =>
            {
                var book = group.First();
                book.Authors = group.Select(book => book.Authors.Single()).ToList();
                return book;
            });

            return books.ToList();

        }

        public Task<List<Book>> SearchBooksAsync(string searchInput)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Book book)
        {
            using var connection = new SqlConnection(_libraryConnectionString);
            var authorsIds = new DataTable();
            authorsIds.Columns.Add("id", typeof(int));
            foreach (var author in book.Authors)
            {
                authorsIds.Rows.Add(author.Id);
            }
            await connection.ExecuteAsync("Stp_Library_UpdateBook", new
            {
                Id = book.Id,
                Name = book.Name,
                YearOfPublish = book.YearOfPublish,
                PicturePath = book.PicturePath,
                CopyPath = book.CopyPath,
                PublisherId = book.Publisher.Id,
                authorsIds = authorsIds
            });
        }
    }
}
