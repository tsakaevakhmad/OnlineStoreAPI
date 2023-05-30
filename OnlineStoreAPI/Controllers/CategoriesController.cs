using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category> _repository;

        public CategoriesController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return await _repository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            
            return await _repository.UpdateAsync(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            return await _repository.CreateAsync(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
