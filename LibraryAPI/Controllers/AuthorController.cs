using LibraryAPI.Interfaces;
using LibraryAPI.Reposetories;
using LibraryModels.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthorController : Controller
    {
        private readonly IAuthorReposetory _authorReposetory;
        public AuthorController(IAuthorReposetory authorReposetory)
        {
            _authorReposetory = authorReposetory;
        }

        [HttpGet]
        [Route("GetAuthors")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _authorReposetory.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("GetAuthorsById")]
        public async Task<IActionResult> GetAuthorsById(List<int> AuthorsIds)
        {
            try
            {
                var books = await _authorReposetory.GetAuthorsById(AuthorsIds);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCitys")]
        public async Task <IActionResult> GetCitys()
        {
            try
            {
                return Ok(await _authorReposetory.GetAllCitysAsync());  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteAuthor")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _authorReposetory.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Author author)
        {
            try
            {
                await _authorReposetory.CreateAsync(author);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("Update")]
        public async Task<IActionResult> Update(Author author)
        {
            try
            {
                await _authorReposetory.UpdateAsync(author);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
