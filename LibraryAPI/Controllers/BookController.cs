using LibraryAPI.Interfaces;
using LibraryAPI.Reposetories;
using LibraryModels.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class BookController : ControllerBase
    {
        private readonly IBookReposetory _bookReposetory;
        public BookController(IBookReposetory bookReposetory)
        {
            _bookReposetory = bookReposetory;
        }

        [HttpGet]
        [Route("GetBooks")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var books = await _bookReposetory.GetAllAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("GetBooksById")]
        public async Task<IActionResult> GetBooksById(List<int> booksIds)
        {
            try
            {
                var books = await _bookReposetory.GetBooksById(booksIds);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPublishers")]
        public async Task<IActionResult> GetPublishers()
        {
            try
            {
                var publishers = await _bookReposetory.GetAllPublishersAsync();
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpDelete]
        [Route("DeleteBook")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _bookReposetory.DeleteAsync(id);
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("CreateBook")]
        public async Task<IActionResult> Create(NewBook newBookDto)
        {
            try
            {
                await _bookReposetory.CreateAsync(newBookDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("Update")]
        public async Task<IActionResult> Update(Book book)
        {
            try
            {
                await _bookReposetory.UpdateAsync(book);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("bla")]
        public async Task<IActionResult> bla(string book)
        {
            try
            {
                //await _bookReposetory.UpdateAsync(book);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
