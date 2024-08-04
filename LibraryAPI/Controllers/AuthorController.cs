using LibraryAPI.Interfaces;
using LibraryAPI.Reposetories;
using LibraryModels.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthorController : Controller
    {
        private readonly IAuthorReposetory _authorReposetory;
        private HttpClient _httpClient;
        public AuthorController(IAuthorReposetory authorReposetory, IHttpClientFactory httpClientFactory)
        {
            _authorReposetory = authorReposetory;
            _httpClient = httpClientFactory.CreateClient();
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
                var isDeleted = await _authorReposetory.DeleteAsync(id);
                using StringContent idAsJson = new(id.ToString(), Encoding.UTF8, "application/json");
                var res = await _httpClient.PostAsync($"http://localhost:5158/api/LibrarySignalR/AuthorDeleted", idAsJson);
                return Ok(isDeleted);
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
