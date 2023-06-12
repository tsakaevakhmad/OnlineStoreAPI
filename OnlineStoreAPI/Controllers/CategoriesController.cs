using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryShortDTO>>> GetCategory()
        {
            ResponseDTO<IEnumerable<CategoryShortDTO>> result = new ResponseDTO<IEnumerable<CategoryShortDTO>>(null);
            try
            {
                result = await _categoryServices.GetAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryShortDTO>> GetCategory(int id)
        {
            ResponseDTO<CategoryShortDTO> result = new ResponseDTO<CategoryShortDTO>(null);
            try
            {
                result = await _categoryServices.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryShortDTO>> PutCategory(int id, CategoryShortDTO category)
        {
            if (id != category.Id)
            {
                return BadRequest("Incorrect id");
            }

            ResponseDTO<CategoryShortDTO> result = new ResponseDTO<CategoryShortDTO>(null);
            try
            {
                result = await _categoryServices.UpdateAsync(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryShortDTO>> PostCategory(CategoryShortDTO category)
        {
            ResponseDTO<CategoryShortDTO> result = new ResponseDTO<CategoryShortDTO>(null);
            try
            {
                result = await _categoryServices.CreateAsync(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryShortDTO>> DeleteCategory(int id)
        {
            ResponseDTO<CategoryShortDTO> result = new ResponseDTO<CategoryShortDTO>(null);
            try
            {
                result = await _categoryServices.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }
    }
}
